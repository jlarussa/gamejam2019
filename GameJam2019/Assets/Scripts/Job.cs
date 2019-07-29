using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Job
{
  public enum JobState
  {
    planning = 0,
    inProgress = 1,
    complete = 2,
    failed = 3,
    expired = 4
  }

  public int BaseTrainCost = 250;
  
  private int requiredHacking;
  public int RequiredHacking => requiredHacking;

  private int requiredStealth;
  public int RequiredStealth => requiredStealth;

  private int requiredAssassination;
  public int RequiredAssasination => requiredAssassination;

  private bool trainHacking = false;
  public bool TrainHacking => trainHacking;
  
  private bool trainStealth = false;
  public bool TrainStealth => trainStealth;
  
  private bool trainAssassination = false;
  public bool TrainAssassination => trainAssassination;
  
  private int difficulty;
  public int Difficulty => difficulty;

  private int goldCost = 0;
  public int GoldCost => goldCost;

  private int goldReward = 100;
  public int GoldReward => goldReward;

  public int PersonnelLimit { get; set; } = 3;

  public int Penalty { get; set; } = 10;

  private string jobName;
  public string Name => jobName;

  private int duration = ExpirationConst;
  public int Duration => duration;

  private int expiration = DurationConst;
  public int Expiration => expiration;

  public static readonly int DurationConst = 25;
  public static readonly int ExpirationConst = 15;

  public List<Employee> Staff { get; set; } = new List<Employee>();

  private JobState currentState = JobState.planning;
  public JobState CurrentState
  {
    get { return currentState; }
    private set
    {
      currentState = value;
      currentStateUpdated?.Invoke();
    }
  }

  public Action currentStateUpdated;

  public DateTime startTime { get; private set; }
  public DateTime endTime { get; private set; }

  public bool StartJob()
  {
    if ( !CanStart() )
    {
      return false;
    }
    CurrentState = JobState.inProgress;
    startTime = DateTime.UtcNow;
    endTime = DateTime.UtcNow + new TimeSpan( 0, 0, Duration );

    return true;
  }

  public Job( string name, int difficulty, bool training = false )
  {
    this.difficulty = difficulty;
    this.jobName = name;
    currentState = JobState.planning;

    if ( training )
    {
      Penalty = 0;
      goldReward = 0;
      PersonnelLimit = 1;
      expiration = 999;
      duration = 60;
    }
    else
    {
      var random = new System.Random(DateTime.UtcNow.Millisecond);
      Penalty = 25 * difficulty;
      goldReward = 100 + 25 * difficulty;
      requiredHacking = random.Next( 1, difficulty + 2);
      requiredAssassination = random.Next( 1, difficulty + 2);
      requiredStealth = random.Next( 1, difficulty + 2);
    }
  }

  public void Tick()
  {
    if ( CurrentState == JobState.inProgress )
    {
      if ( DateTime.UtcNow > endTime )
      {
        EndJob();
      }
    }
    if ( CurrentState == JobState.planning )
    {
      if ( DateTime.UtcNow > endTime )
      {
        if ( CanStart() )
        {
          StartJob();
        }
        else
        {
          ExpireJob();
        }
      }
    }
  }

  public double CompletionChance
  {
    get
    {
      double remainingAssassination = ( double )RequiredAssasination;
      double remainingStealth = ( double )RequiredStealth;
      double remainingHacking = ( double )RequiredHacking;
      foreach ( Employee person in Staff )
      {
        remainingAssassination -= person.Assassination;
        remainingHacking -= person.Hacking;
        remainingStealth -= person.Stealth;
      }
      return 1.0 - ( Math.Max( 0.0, remainingAssassination ) + Math.Max( 0.0, remainingHacking ) + Math.Max( 0.0, remainingStealth ) ) / ( RequiredAssasination + RequiredHacking + RequiredStealth );
    }
  }

  public void ExpireJob()
  {
    CurrentState = JobState.expired;
    //CollectJob();
  }

  public void EndJob()
  {
    var random = new System.Random(DateTime.UtcNow.Millisecond);
    int roll = random.Next( 0, 100 );
    if (roll > CompletionChance * 100)
    {
      FailJob();
      return;
    }
    CompleteJob();
  }

  public void FailJob()
  {
    CurrentState = JobState.failed;
  }

    public Action<Job> OnJobCollected;

    public void CollectJob()
    {
        if (CurrentState == JobState.failed || CurrentState == JobState.complete || CurrentState == JobState.expired)
        {
            foreach (Employee staff in Staff)
            {
                staff.Away = false;
            }
            
            OnJobCollected?.Invoke(this);
        }
    }

    public void CompleteJob()
  {
    CurrentState = JobState.complete;
    
    if ( trainAssassination )
    {
      Staff[ 0 ].Assassination++;
    }

    if ( trainHacking )
    {
      Staff[ 0 ].Hacking++;
    }

    if ( trainStealth )
    {
      Staff[ 0 ].Stealth++;
    }
  }

  public bool CanStart()
  {
    return CurrentState == JobState.planning && Staff.Count > 0;
  }

  public void ResetJob()
  {
    CurrentState = JobState.planning;
    
    foreach (Employee staff in Staff)
    {
      staff.Away = false;
    }
    
    Staff = new List<Employee>();
    startTime = DateTime.UtcNow;
    endTime = DateTime.UtcNow + new TimeSpan( 0, 0, Expiration );
  }

  public bool AddEmployee( Employee newStaff )
  {
    if ( currentState != JobState.planning )
    {
      return false;
    }

    if ( trainAssassination )
    {
      goldCost = ( difficulty * newStaff.Assassination + 1 ) * BaseTrainCost;
    }
    
    if ( trainHacking )
    {
      goldCost = difficulty * newStaff.Hacking + 1 * BaseTrainCost;
    }
    
    if ( trainStealth )
    {
      goldCost = difficulty * newStaff.Stealth + 1 * BaseTrainCost;
    }

    if ( Manager.Current.CanChangeMoney( -1 * goldCost ) )
    {
      if ( Staff.Count < PersonnelLimit)
      {
        Staff.Add( newStaff );
        Manager.Current.MoneyChanged( -1 * goldCost );
        currentStateUpdated?.Invoke();
        return true;
      }
    }

    return false;
  }

  public bool RemoveEmployee( Employee removeStaff )
  {
    if (Staff.Contains(removeStaff))
    {
        Staff.Remove(removeStaff);
        return true;
    }
    
    return false;
  }

  public void SetHackTraining() { trainHacking = true; }
  public void SetAssassinationTraining() { trainAssassination = true; }
  public void SetStealthTraining() { trainStealth = true; }
}
