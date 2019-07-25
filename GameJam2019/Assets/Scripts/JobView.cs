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

    private List<EmployeeSlot> personnelImages = new List<EmployeeSlot>();

    public void StartJob()
    {
        SourceJob.StartJob();
    }

    public void ResetJob()
    {
        SourceJob.ResetJob();
    }

    public void AddEmployee(Employee newStaff)
    {
        if (newStaff.Away == false && SourceJob.AddEmployee(newStaff))
        {
            newStaff.Away = true;
            foreach (EmployeeSlot slot in personnelImages)
            {
                if (!slot.occupied)
                {
                    slot.SetEmployee(newStaff);
                    return;
                }
            }
        }
    }

    public void RemoveEmployee(Employee oldStaff)
    {
        SourceJob.RemoveEmployee(oldStaff);
        oldStaff.Away = false;
    }

    public void Initialize(Job source)
    {
        SourceJob = source;
        SourceJob.ResetJob();
        stateText.text = SourceJob.CurrentState.ToString();
        titleText.text = SourceJob.Name;
        SourceJob.currentStateUpdated += OnJobStateUpdated;
        for (int i = 0; i < SourceJob.PersonnelLimit; i++)
        {
            var newEmployee = Instantiate(employeePrefab, employeeListParent.transform);
            var personnelSlot = newEmployee.GetComponent<EmployeeSlot>();
            newEmployee.SetActive(true);
            personnelImages.Add(personnelSlot);
            personnelSlot.employeeRemoved += RemoveEmployee;
        }
    }

    private void OnJobStateUpdated()
    {
        stateText.text = SourceJob.CurrentState.ToString();
    }

    void FixedUpdate()
    {
        SourceJob.Tick();
        TimeSlider.value = (float)((SourceJob.endTime - DateTime.UtcNow).TotalSeconds / (SourceJob.endTime - SourceJob.startTime).TotalSeconds);
    }
}
