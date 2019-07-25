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
    private Text DayMoneyDisplay;
    
    [SerializeField]
    private Text TotalMoneyDisplay;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentDay != null)
        {
            currentDay.Tick();
            var timeRemaining = currentDay.Endtime - DateTime.UtcNow;
            ClockDisplay.text = ((int)timeRemaining.TotalSeconds).ToString();
            DayMoneyDisplay.text = "$" + jobs.JobEarnings;
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

        totalMoney += jobs.JobEarnings;
        TotalMoneyDisplay.text = "$" + totalMoney;
        
        currentDay.EndDay -= OnDayEnd;
        DayEndEvent.Raise();
    }
}
