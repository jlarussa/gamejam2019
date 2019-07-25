using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{

	private Day currentDay;

	private int dayCount = 0;
	public int DayCount => dayCount;

	private int totalMoney = 0;
	public int TotalMoney => totalMoney;

	[SerializeField]
	private EmployeeInventory employees;

	// Update is called once per frame
	void FixedUpdate () {
		if ( currentDay != null )
		{
			currentDay.Tick();
		}
	}

	public void OnDayStart()
	{
		Debug.Log( "day start" );
		dayCount++;
		
		currentDay = new Day( dayCount );
		currentDay.EndDay += OnDayEnd;
		
		currentDay.Start();
	}
	
	public void OnDayEnd()
	{
		Debug.Log( "day end" );
		
		totalMoney += currentDay.Earned;
		currentDay.EndDay -= OnDayEnd;
		currentDay.End();
		
	}
}
