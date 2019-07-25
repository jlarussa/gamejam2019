using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmployeeSlot : MonoBehaviour {
    [SerializeField]
    private ImageList portraitList;

    [SerializeField]
    private Image portrait;

    public Employee currentEmployee = null;

    public void SetEmployee(Employee newEmployee)
    {
        currentEmployee = newEmployee;
        portrait.sprite = portraitList.Images[currentEmployee.PortraitIndex];
    }

    public void RemoveEmployee()
    {
        currentEmployee = null;
        portrait.sprite = null;
    }
}
