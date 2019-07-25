using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobInventory : MonoBehaviour
{
	private List<Job> jobs = new List<Job>();
	public List<Job> Jobs => jobs;

	private Job stealthTraining;
	public Job StealthTraining => stealthTraining;
	
	private Job assassinationTraining;
	public Job AssassinationTraining => assassinationTraining;
	
	private Job hackTraining;
	public Job HackTraining => hackTraining;

	public void AddJob( int difficulty )
	{
		Job j = new Job( difficulty);
		jobs.Add( j );
	}

	public void AddTrainingJobs( int difficulty )
	{
		hackTraining = new Job( "Train Hacking", difficulty, true, false, false );
		stealthTraining = new Job( "Train Stealth", difficulty,  false, true, false );
		assassinationTraining = new Job( "Train Assassination", difficulty, false, false, true );
	}

	public void Tick()
	{
		foreach ( Job j in jobs )
		{
			j.Tick();
		}
		
		stealthTraining.Tick();
		assassinationTraining.Tick();
		hackTraining.Tick();
	}

	public void NewDay( int difficulty )
	{
		jobs.Clear();
		
		// Always start the day with 2 jobs
		AddJob( difficulty );
		AddJob( difficulty );

		AddTrainingJobs( difficulty );
	}
}
