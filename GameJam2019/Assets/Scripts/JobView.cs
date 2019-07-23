using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JobView : MonoBehaviour
{
  [SerializeField]
  private Job sourceJob;
  public Job SourceJob
  {
    get { return sourceJob; }
    private set
    {
      sourceJob = value;
      titleText.text = sourceJob.Name;
      sourceJob.currentStateUpdated += OnJobStateUpdated;
    }
  }

  [SerializeField]
  private Text stateText;

  [SerializeField]
  private Text titleText;

  [SerializeField]
  private Slider TimeSlider;

  public void StartJob()
  {
    SourceJob.StartJob();
  }

  public void ResetJob()
  {
    SourceJob.ResetJob();
  }

  public void AddEmployee( Employee newStaff )
  {
    SourceJob.AddEmployee( newStaff );
  }

  private void Awake()
  {
    SourceJob.ResetJob();
    stateText.text = SourceJob.CurrentState.ToString();
    titleText.text = SourceJob.Name;
    SourceJob.currentStateUpdated += OnJobStateUpdated;
  }

  private void OnJobStateUpdated()
  {
    stateText.text = SourceJob.CurrentState.ToString();
  }

  void FixedUpdate()
  {
    SourceJob.Tick();
    TimeSlider.value = (float)((SourceJob.endTime - DateTime.UtcNow).TotalSeconds / (SourceJob.endTime - SourceJob.startTime).TotalSeconds) ;
  }
}
