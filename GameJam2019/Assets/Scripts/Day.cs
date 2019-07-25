using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day
{
    public int BaseMoneyRequired = 1000;
    public TimeSpan BaseDayLength = TimeSpan.FromMinutes(4);

    private bool inProgress = false;
    private int difficulty;

    private int earned = 0;
    public int Earned => earned;

    private int requiredEarning;
    public int RequiredEarning => requiredEarning;

    private DateTime endTime;
    public DateTime Endtime => endTime;

    private JobInventory jobs;
    public JobInventory Jobs => jobs;

    public Action EndDay;

    private int standardJobRate = 15;

    private DateTime nextJobTime;

    public Day(int difficulty, JobInventory ji)
    {
        this.difficulty = difficulty;
        requiredEarning = (int)Math.Pow(1.25, difficulty) * BaseMoneyRequired;
        jobs = ji;
    }

    public void Tick()
    {
        if (DateTime.UtcNow > Endtime)
        {
            End();
        }

        if (DateTime.UtcNow > nextJobTime)
        {
            jobs.AddJob(difficulty);
            nextJobTime = DateTime.UtcNow + new TimeSpan(0, 0, (int)((1.0 / difficulty) * standardJobRate));
        }

        if (inProgress)
        {
            jobs.Tick();
        }
    }

    public void Begin()
    {
        Jobs.NewDay(difficulty);
        inProgress = true;
        endTime = DateTime.UtcNow.Add(BaseDayLength);
        nextJobTime = DateTime.UtcNow + new TimeSpan(0, 0, (int)((1.0 / difficulty) * standardJobRate));
    }

    public void End()
    {
        inProgress = false;
        EndDay?.Invoke();
    }
}
