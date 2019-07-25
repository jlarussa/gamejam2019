﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobInventory : MonoBehaviour
{
    [SerializeField]
    private StringList jobNames;

    [SerializeField]
    private GameObject jobViewPrefab;

    private List<Job> jobs = new List<Job>();
    public List<Job> Jobs => jobs;

    private Job stealthTraining;
    public Job StealthTraining => stealthTraining;

    private Job assassinationTraining;
    public Job AssassinationTraining => assassinationTraining;

    private Job hackTraining;
    public Job HackTraining => hackTraining;

    public void AddJob(int difficulty)
    {
        Job j = new Job(jobNames.Strings[Random.Range(0, jobNames.Strings.Count)], difficulty);
        jobs.Add(j);
        var spawnedJob = Instantiate(jobViewPrefab, transform);
        spawnedJob.GetComponent<JobView>().Initialize(j);
    }

    public void AddTrainingJobs(int difficulty)
    {
        hackTraining = new Job("Train Hacking", difficulty );
        stealthTraining = new Job("Train Stealth", difficulty );
        assassinationTraining = new Job("Train Assassination", difficulty );
        
        hackTraining.SetHackTraining();
        stealthTraining.SetStealthTraining();
        assassinationTraining.SetAssassinationTraining();
    }

    public void Tick()
    {
        foreach (Job j in jobs)
        {
            j.Tick();
        }

        stealthTraining.Tick();
        assassinationTraining.Tick();
        hackTraining.Tick();
    }
 
    public void NewDay(int difficulty)
    {
        jobs.Clear();
        
        if ( transform.childCount > 0 )
        {
            for ( int i = 0; i < transform.childCount; i++ )
            {
                Destroy( transform.GetChild( i ).gameObject  );
            }
        }
        
        // Always start the day with 2 jobs
        AddJob(difficulty);
        AddJob(difficulty);

        AddTrainingJobs(difficulty);
    }
}
