using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmployeeSlot : MonoBehaviour
{
  [SerializeField]
  private ImageList portraitList;

  [SerializeField]
  private Image portrait;

  [SerializeField]
  private Sprite defaultSprite = null;

  public Employee currentEmployee = null;

  public bool occupied { get; private set; } = false;

  public Func<Employee, bool> employeeRemoved;

  private void Awake()
  {
    portrait.sprite = defaultSprite;
  }

  public void SetEmployee( Employee newEmployee )
  {
    currentEmployee = newEmployee;
    portrait.sprite = portraitList.Images[ currentEmployee.PortraitIndex ];
    occupied = true;
  }

  public void RemoveEmployee()
  {
    if ( employeeRemoved != null && employeeRemoved.Invoke( currentEmployee ) )
    {
      currentEmployee = null;
      portrait.sprite = defaultSprite;
      occupied = false;
    }
  }
}
