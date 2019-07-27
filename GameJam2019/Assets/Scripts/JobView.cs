using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JobView : MonoBehaviour
{
  private Job sourceJob;
  public Job SourceJob
  {
    get { return sourceJob; }
    private set
    {
      sourceJob = value;
      titleText.text = sourceJob.Name;
      rewardText.text = "Reward: " + sourceJob.GoldReward.ToString();
      sourceJob.currentStateUpdated += OnJobStateUpdated;
      HackingValue = 0;
      StealthValue = 0;
      AssassinationValue = 0;
    }
  }
  [SerializeField]
  private GameObject employeePrefab = null;

  [SerializeField]
  private GameObject employeeListParent = null;

  [SerializeField]
  private Text stateText;

  [SerializeField]
  private Text titleText;

  [SerializeField]
  private Slider TimeSlider;

  [SerializeField]
  private Text rewardText;

  [SerializeField]
  private Button startButton;

  [SerializeField]
  private Button completeButton;

  [SerializeField]
  private Color CompleteColor;

  [SerializeField]
  private Color InProgressColor;

  [SerializeField]
  private Color FailedColor;

  [SerializeField]
  private Color ExpiredColor;

  [SerializeField]
  private Image background;

  [SerializeField]
  private float failedWaitTime = 1.5f;

  [SerializeField]
  private float expiredWaitTime = 1.5f;

  [SerializeField]
  private GameEvent OnSucceededEvent;

  [SerializeField]
  private GameEvent OnExpiredEvent;

  [SerializeField]
  private GameEvent OnFailureEvent;

  private int hackingValue;
  public int HackingValue
  {
    get { return hackingValue; }
    set
    {
      hackingValue = value;
      currentHacking.text = value.ToString() + "/" + SourceJob.RequiredHacking.ToString();
      hackingSlider.value = value / ( float )SourceJob.RequiredHacking;
    }
  }
  private int stealthValue;
  public int StealthValue
  {
    get { return stealthValue; }
    set
    {
      stealthValue = value;
      currentStealth.text = value.ToString() + "/" + SourceJob.RequiredStealth.ToString();
      stealthSlider.value = value / ( float )SourceJob.RequiredStealth;
    }
  }
  private int assassinationValue;
  public int AssassinationValue
  {
    get { return assassinationValue; }
    set
    {
      assassinationValue = value;
      currentAssassination.text = value.ToString() + "/" + SourceJob.RequiredAssasination.ToString();
      assassinationSlider.value = value / ( float )SourceJob.RequiredAssasination;
    }
  }

  [SerializeField]
  private Text currentAssassination;
  [SerializeField]
  private Text currentStealth;
  [SerializeField]
  private Text currentHacking;

  [SerializeField]
  private Slider hackingSlider;
  [SerializeField]
  private Slider assassinationSlider;
  [SerializeField]
  private Slider stealthSlider;

  [SerializeField]
  private Text successChanceText;

  private List<EmployeeSlot> personnelImages = new List<EmployeeSlot>();

  public void StartJob()
  {
    if ( SourceJob.StartJob() )
    {
      startButton.gameObject.SetActive( false );
      completeButton.gameObject.SetActive( true );
    }
  }

  public void CompleteJob()
  {
    SourceJob.CollectJob();
  }

  public bool CanCollectJob()
  {
    return SourceJob.CurrentState == Job.JobState.complete || SourceJob.CurrentState == Job.JobState.failed;
  }

  public void ResetJob()
  {
    SourceJob.ResetJob();
  }

  public void AddEmployee( Employee newStaff )
  {
    if ( newStaff.Away == false && SourceJob.AddEmployee( newStaff ) )
    {
      newStaff.Away = true;
      foreach ( EmployeeSlot slot in personnelImages )
      {
        if ( !slot.occupied )
        {
          slot.SetEmployee( newStaff );
          HackingValue += newStaff.Hacking;
          AssassinationValue += newStaff.Assassination;
          StealthValue += newStaff.Stealth;
          successChanceText.text = string.Format( "Success chance: {0}%", sourceJob.CompletionChance * 100 );
          return;
        }
      }
    }
  }

  public bool RemoveEmployee( Employee oldStaff )
  {
    if ( sourceJob != null && sourceJob.CurrentState == Job.JobState.planning )
    {
      SourceJob.RemoveEmployee( oldStaff );
      oldStaff.Away = false;
      HackingValue -= oldStaff.Hacking;
      AssassinationValue -= oldStaff.Assassination;
      StealthValue -= oldStaff.Stealth;
      successChanceText.text = string.Format( "Success chance: {0}%", sourceJob.CompletionChance * 100);
      return true;
    }
    return false;
  }

  public void Initialize( Job source )
  {
    SourceJob = source;
    SourceJob.ResetJob();
    stateText.text = SourceJob.CurrentState.ToString();
    titleText.text = SourceJob.Name;
    SourceJob.currentStateUpdated += OnJobStateUpdated;
    successChanceText.text = "Success Chance: 0%";
    for ( int i = 0; i < SourceJob.PersonnelLimit; i++ )
    {
      var newEmployee = Instantiate( employeePrefab, employeeListParent.transform );
      var personnelSlot = newEmployee.GetComponent<EmployeeSlot>();
      newEmployee.SetActive( true );
      personnelImages.Add( personnelSlot );
      personnelSlot.employeeRemoved += RemoveEmployee;
    }
  }

  private void OnJobStateUpdated()
  {
    stateText.text = SourceJob.CurrentState.ToString();
    completeButton.interactable = CanCollectJob();
    startButton.interactable = SourceJob.CanStart();
    if (sourceJob.CurrentState == Job.JobState.inProgress )
    {
      completeButton.image.color = InProgressColor;
      background.color = InProgressColor;
      startButton.gameObject.SetActive( false );
      completeButton.gameObject.SetActive( true );
    }
    if (sourceJob.CurrentState == Job.JobState.complete)
    {
      completeButton.image.color = CompleteColor;
      background.color = CompleteColor;
      OnSucceededEvent?.Raise();
    }
    if (sourceJob.CurrentState == Job.JobState.failed)
    {
      StartCoroutine( FailureCoroutine() );
    }
    if ( sourceJob.CurrentState == Job.JobState.expired )
    {
      StartCoroutine( ExpiredCoroutine() );
    }
  }

  private IEnumerator FailureCoroutine()
  {
    // Raise event so sound can play when color transiton happends
    OnFailureEvent?.Raise();
    background.color = FailedColor;
    yield return new WaitForSeconds( failedWaitTime );
    CompleteJob();
  }

  private IEnumerator ExpiredCoroutine()
  {
    OnExpiredEvent?.Raise();
    background.color = ExpiredColor;
    yield return new WaitForSeconds( expiredWaitTime );
    CompleteJob();
  }

  void FixedUpdate()
  {
    SourceJob.Tick();
    TimeSlider.value = ( float )( ( SourceJob.endTime - DateTime.UtcNow ).TotalSeconds / ( SourceJob.endTime - SourceJob.startTime ).TotalSeconds );
  }
}
