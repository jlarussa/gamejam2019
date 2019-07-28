using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public static Manager Current
    {
        get;
        private set;
    }

    public GameEvent DayEndEvent;
    
    private Day currentDay;
    
    public int DaySeconds = 60;

    private int dayCount = 0;
    public int DayCount => dayCount;

    private int totalMoney = 900;
    public int TotalMoney => totalMoney;

    private int dailyMoney = 0;
    public int DailyMoney => dailyMoney;
    
    private string playerName = "Employee #999";

    public string PlayerName 
    {
        get { return playerName; }
        set
        {
            playerName = value;
            nameText.text = "Welcome, " + playerName;
        }
    }

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

    [SerializeField]
    private Text lateDisplay;
    private DateTime lateTime;

    [SerializeField]
    private Text planningText;

    [SerializeField]
    private Text nameText;

    [SerializeField]
    private GameObject warningOverlay;

    [SerializeField]
    private float timeWarning = 4.0f;

    [SerializeField]
    private GameEvent TimeWarningEvent;

    [SerializeField]
    private Leaderboard leaderboard;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentDay != null && currentDay.inProgress)
        {
            var timeRemaining = currentDay.Endtime - DateTime.UtcNow;
            ClockDisplay.text = ((int)timeRemaining.TotalSeconds).ToString();

          // IF TIME REMAINING IS NOT ENOUGH TO COMPLETE A JOB, WARN PLAYER. ALSO STOP SPAWNING JOBS
          float timeToStopSpawningJobsAndWarnPlayer = Job.DurationConst + timeWarning;
          if ( timeRemaining < TimeSpan.FromSeconds( timeToStopSpawningJobsAndWarnPlayer ) && !warningOverlay.activeInHierarchy )
          {
            warningOverlay.SetActive( true );
            currentDay.SpawnJobs = false;
            TimeWarningEvent.Raise();
          }
        }
        else
        {
            lateDisplay.text = "It looks like you're already " + ((int)(DateTime.UtcNow - lateTime).TotalSeconds).ToString() + " seconds late.";
        }
        // Tick needs to happen at the end of the update since ticking can cause the day to end and create a new day during the same update!
        currentDay.Tick();
  }

    public void CreateDay()
    {
        dayCount++;
        currentDay = new Day(dayCount, jobs);
        planningText.text = "It's your " + AddOrdinal(DayCount) + " day on the job. For today let's set a simple target. Just try to make " + currentDay.RequiredEarning + " dollars for the company without losing too many employees.";
    }

    public string AddOrdinal(int num)
    {
        if (num <= 0) return num.ToString();

        switch (num % 100)
        {
            case 11:
            case 12:
            case 13:
                return num + "th";
        }

        switch (num % 10)
        {
            case 1:
                return num + "st";
            case 2:
                return num + "nd";
            case 3:
                return num + "rd";
            default:
                return num + "th";
        }

    }

    public void OnDayStart()
    {
        Debug.Log("day start");
        dailyMoney = 0;
        MoneyChanged( 0 );

        currentDay.EndDay += OnDayEnd;

        currentDay.Begin( DaySeconds );
    }

    public void OnDayEnd()
    {
        Debug.Log("day end");
      // Reset time out warning
      warningOverlay.SetActive( false );
      currentDay.EndDay -= OnDayEnd;
      DayEndEvent.Raise();

      if ( currentDay.RequiredEarning > dailyMoney )
      {
          EndGame();
      }
      
      CreateDay();
  }

  public bool CanChangeMoney( int money )
    {
        return totalMoney + money >= 0;
    }

    public void MoneyChanged(int money, bool recruit = false)
    {
        totalMoney += money;
        if (!recruit)
        {
            dailyMoney = Mathf.Max(dailyMoney + money, 0);
        }
        DayMoneyDisplay.text = "$" + dailyMoney + "/" + currentDay.RequiredEarning;
        TotalMoneyDisplay.text = "$" + totalMoney;

    }

    void Start()
  {
    warningOverlay.SetActive( false );
    jobs.MoneyUpdated += MoneyChanged;
        Current = this;
        CreateDay();
        lateTime = DateTime.UtcNow;
        employees.RecruitNewEmployee();
        employees.RecruitNewEmployee();
        employees.RecruitNewEmployee();
    }

    public void SetName( string name )
    {
        PlayerName = name;
    }

    void EndGame()
    {
        // end the game
        leaderboard.gameObject.SetActive( true );
        leaderboard.PostPlayerScore( PlayerName, totalMoney );

        dayCount = 0;
    }
}
