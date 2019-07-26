using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class JobInventory : MonoBehaviour
{
    [SerializeField]
    private StringList jobNames;

    [SerializeField]
    private GameObject jobViewPrefab;

    private List<Job> jobs = new List<Job>();
    public List<Job> Jobs => jobs;

    private List<Job> toDestroy = new List<Job>();
    
    private Job stealthTraining;
    public Job StealthTraining => stealthTraining;

    private Job assassinationTraining;
    public Job AssassinationTraining => assassinationTraining;

    private Job hackTraining;
    public Job HackTraining => hackTraining;
    
    private Dictionary<Job, JobView> jobToView = new Dictionary<Job, JobView>();

    public Action<int> MoneyUpdated;

    public void AddJob(int difficulty)
    {
        Job j = new Job(jobNames.Strings[Random.Range(0, jobNames.Strings.Count)], difficulty);
        j.OnJobCollected += OnJobCompleted;
        jobs.Add(j);
        var spawnedJob = Instantiate(jobViewPrefab, transform);
        spawnedJob.SetActive(true);
        JobView jView = spawnedJob.GetComponent<JobView>();
        jView.Initialize(j);

        jobToView[ j ] = jView;
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

        foreach ( Job j in toDestroy )
        {
            jobs.Remove( j );
        }
        
        stealthTraining.Tick();
        assassinationTraining.Tick();
        hackTraining.Tick();
    }
 
    public void NewDay(int difficulty)
    {
        foreach ( Job j in jobs )
        {
            j.OnJobCollected -= OnJobCompleted;
            j.ResetJob();
        }

        if ( transform.childCount > 0 )
        {
            foreach (Transform child in transform) {
                Destroy(child.gameObject);
            }
        }
        
        jobs.Clear();
        jobToView.Clear();
        
        // Always start the day with 2 jobs
        AddJob(difficulty);
        AddJob(difficulty);

        AddTrainingJobs(difficulty);
    }

    public void OnJobCompleted( Job j )
    {
        j.OnJobCollected -= OnJobCompleted;
        int jobEarn = j.CurrentState == Job.JobState.complete ? j.GoldReward : ( -1 * j.Penalty );

        MoneyUpdated?.Invoke( jobEarn );
        toDestroy.Add( j );
        Destroy( jobToView[ j ].gameObject );
        jobToView.Remove( j );
    }
    
}
