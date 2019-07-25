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

  private int duration = 10;
  public int Duration => duration;

  private int expiration = 10;
  public int Expiration => expiration;

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

  public void StartJob()
  {
    if ( !CanStart() )
    {
      return;
    }
    CurrentState = JobState.inProgress;
    startTime = DateTime.UtcNow;
    endTime = DateTime.UtcNow + new TimeSpan( 0, 0, Duration );
  }

  // For creating training jobs
  public Job( string name, int difficulty, bool hack, bool stealth, bool assassin )
  {
    this.trainHacking = hack;
    this.trainStealth = stealth;
    this.trainAssassination = assassin;

    jobName = name;
    goldCost = 100 * difficulty;
    goldReward = 0;
    PersonnelLimit = 1;
    expiration = 999;
  }

  public Job( string name, int difficulty )
  {
    this.difficulty = difficulty;
    this.jobName = name;
    currentState = JobState.planning;
    
    var random = new System.Random(DateTime.UtcNow.Millisecond);
    requiredHacking = random.Next( 1, difficulty );
    requiredAssassination = random.Next( 1, difficulty );
    requiredStealth = random.Next( 1, difficulty );
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
        ExpireJob();
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

  private bool CanStart()
  {
    return CurrentState == JobState.planning;
  }

  public void ResetJob()
  {
    CurrentState = JobState.planning;
    Staff = new List<Employee>();
    startTime = DateTime.UtcNow;
    endTime = DateTime.UtcNow + new TimeSpan( 0, 0, Expiration );
  }

  public bool AddEmployee( Employee newStaff )
  {
    if ( Staff.Count < PersonnelLimit)
    {
      Staff.Add( newStaff );
    }
    
    return Staff.Count <= PersonnelLimit;
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
}
