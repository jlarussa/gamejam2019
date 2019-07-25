using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public GameEvent DayEndEvent;
    
    private Day currentDay;

    private int dayCount = 0;
    public int DayCount => dayCount;

    private int totalMoney = 0;
    public int TotalMoney => totalMoney;

    [SerializeField]
    private EmployeeInventory employees;

    [SerializeField]
    private JobInventory jobs;

    [SerializeField]
    private Text ClockDisplay;
    
    [SerializeField]
    private Text MoneyDisplay;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentDay != null)
        {
            currentDay.Tick();
            var timeRemaining = currentDay.Endtime - DateTime.UtcNow;
            ClockDisplay.text = ((int)timeRemaining.TotalSeconds).ToString();
            MoneyDisplay.text = "$" + jobs.JobEarnings;
        }
    }

    public void OnDayStart()
    {
        Debug.Log("day start");
        dayCount++;

        currentDay = new Day(dayCount, jobs);
        currentDay.EndDay += OnDayEnd;

        currentDay.Begin();
    }

    public void OnDayEnd()
    {
        Debug.Log("day end");

        totalMoney += currentDay.Earned;
        currentDay.EndDay -= OnDayEnd;
        DayEndEvent.Raise();
        // TODO: don't call this
        // currentDay.End();

    }
}
