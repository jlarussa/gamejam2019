using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public GameEvent DayEndEvent;
    
    private Day currentDay;
    
    public int DaySeconds = 60;

    private int dayCount = 0;
    public int DayCount => dayCount;

    private int totalMoney = 0;
    public int TotalMoney => totalMoney;

    private int dailyMoney = 0;
    public int DailyMoney => dailyMoney;

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
        }
    }

    public void OnDayStart()
    {
        Debug.Log("day start");
        dayCount++;
        dailyMoney = 0;
        MoneyChanged( 0 );

        currentDay = new Day(dayCount, jobs);
        currentDay.EndDay += OnDayEnd;

        currentDay.Begin( DaySeconds );
    }

    public void OnDayEnd()
    {
        Debug.Log("day end");
        
        currentDay.EndDay -= OnDayEnd;
        DayEndEvent.Raise();
    }

    void MoneyChanged( int money )
    {
        totalMoney += money;
        dailyMoney += money;
        DayMoneyDisplay.text = "$" + dailyMoney;
        TotalMoneyDisplay.text = "$" + totalMoney;
    }

    void Start() { jobs.MoneyUpdated += MoneyChanged; }
}
