using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( menuName = "Job" )]
public class Job : ScriptableObject
{
  public enum JobState
  {
    planning = 0,
    inProgress = 1,
    complete = 2,
    failed = 3,
    expired = 4
  }

  [SerializeField]
  private int requiredHacking;
  public int RequiredHacking => requiredHacking;

  [SerializeField]
  private int requiredStealth;
  public int RequiredStealth => requiredStealth;

  [SerializeField]
  private int requiredAssassination;
  public int RequiredAssasination => requiredAssassination;

  [SerializeField]
  private int difficulty;
  public int Difficulty => difficulty;

  [SerializeField]
  private int goldCost;
  public int GoldCost => goldCost;

  [SerializeField]
  private int goldReward;
  public int GoldReward => goldReward;

  public int PersonnelLimit { get; set; } = 3;

  public int Penalty { get; set; } = 10;

  [SerializeField]
  private string jobName;
  public string Name => jobName;

  [SerializeField]
  private int duration = 10;
  public int Duration => duration;

  [SerializeField]
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
    }
  }

  public DateTime startTime { get; private set; }
  public DateTime endTime { get; private set; }

  public void StartJob()
  {
    if ( CanStart() )
    {
      CurrentState = JobState.inProgress;
    }
    startTime = DateTime.UtcNow;
    endTime = DateTime.UtcNow + new TimeSpan( 0, 0, Duration );
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
      return ( Math.Max( 0.0, remainingAssassination ) + Math.Max( 0.0, remainingHacking ) + Math.Max( 0.0, remainingStealth ) ) / ( RequiredAssasination + RequiredHacking + RequiredStealth );
    }
  }

  public void ExpireJob()
  {
    CurrentState = JobState.expired;
  }

  public void EndJob()
  {
    var random = new System.Random(DateTime.UtcNow.Millisecond);
    int roll = random.Next( 0, 10 );
    if (roll > CompletionChance * 10)
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
}
