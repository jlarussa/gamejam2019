using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmployeeView : MonoBehaviour
{
    public Text NameText;
    public Text DescriptionText;

    // Change these to slider bars?
    public Text HackingText;
    public Text AssassinationText;
    public Text StealthText;

    private Employee employeeData;
    public Employee EmployeeData
    {
        get { return employeeData; }
        set 
        {
            employeeData = value;
            UpdateFlavorViews();
            UpdateStatViews();
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        UpdateFlavorViews();
        UpdateStatViews();
    }

    private void UpdateFlavorViews()
    {
        NameText.text = EmployeeData.Name;
        if ( DescriptionText != null )
        {
            DescriptionText.text = EmployeeData.Description;
        }
        
    }

    private void UpdateStatViews()
    {
        HackingText.text = EmployeeData.Hacking.ToString();
        AssassinationText.text = EmployeeData.Assassination.ToString();
        StealthText.text = EmployeeData.Stealth.ToString();
    }
}
