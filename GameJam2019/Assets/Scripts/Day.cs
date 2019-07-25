using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day
{
  public int BaseMoneyRequired = 1000;
  public TimeSpan BaseDayLength = TimeSpan.FromMinutes( 4 );

  private bool inProgress = false;
  
  private int earned = 0;
  public int Earned => earned;

  private int requiredEarning;
  public int RequiredEarning => requiredEarning;
  
  private DateTime endTime;
  public DateTime Endtime => endTime;

  private JobInventory jobs;
  public JobInventory Jobs => jobs;

  public Action EndDay;

  public Day( int difficulty )
  {
    requiredEarning = ( int ) Math.Pow( 1.25, difficulty ) * BaseMoneyRequired;
    jobs = new JobInventory();
  }

  public void Tick()
  {
    if ( Endtime.Subtract( DateTime.Now ).Milliseconds <= 0 )
    {
      End();
    }

    if ( inProgress )
    {
      jobs.Tick();
    }
  }

  public void Start()
  {
    inProgress = true;
    endTime = DateTime.Now.Add( BaseDayLength );
  }

  public void End()
  {
    inProgress = false;
    EndDay?.Invoke();
  }
}
