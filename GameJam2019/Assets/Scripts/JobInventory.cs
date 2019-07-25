using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobInventory
{
	private List<Job> jobs = new List<Job>();
	public List<Job> Jobs => jobs;

	private Job stealthTraining;
	public Job StealthTraining => stealthTraining;
	
	private Job assassinationTraining;
	public Job AssassinationTraining => assassinationTraining;
	
	private Job hackTraining;
	public Job HackTraining => hackTraining;

	public void AddJob()
	{
		
	}

	public void AddTraining()
	{
		// TODO: this
	}

	public void Tick()
	{
		Debug.Log( "job tick" );
		
//		foreach ( Job j in jobs )
//		{
//			j.Tick();
//		}
//		
//		stealthTraining.Tick();
//		assassinationTraining.Tick();
//		hackTraining.Tick();
	}
}
