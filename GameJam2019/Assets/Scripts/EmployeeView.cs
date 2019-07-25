using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmployeeView : MonoBehaviour
{
    public Image PortraitUI;
    public Text NameText;
    public Text DescriptionText;

    public Text HackingText;
    public Slider HackingSlider;

    public Text AssassinationText;
    public Slider AssassinationSlider;

    public Text StealthText;
    public Slider StealthSlider;

    [SerializeField]
    private GameObject awayOverlay;

    private Employee employeeData;
    public Employee EmployeeData
    {
        get { return employeeData; }
        set 
        {
            employeeData = value;
            UpdateFlavorViews();
            UpdateStatViews();
            employeeData.awayStateChanged += UpdateAwayOverlay;
        }
    }

    private void UpdateAwayOverlay()
    {
        awayOverlay.SetActive(employeeData.Away);
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateFlavorViews();
        UpdateStatViews();

    }

    private void UpdateFlavorViews()
    {
        NameText.text = EmployeeData.FirstName + "\n" + EmployeeData.LastName;
        if ( DescriptionText != null )
        {
            DescriptionText.text = EmployeeData.Description;
        } 
    }

    private void UpdateStatViews()
    {
        HackingText.text = string.Empty;
        if ( EmployeeData.Hacking > 0 )
        {
            HackingText.text = EmployeeData.Hacking.ToString();
        }
        HackingSlider.value = EmployeeData.Hacking;

        AssassinationText.text = string.Empty;
        if ( EmployeeData.Assassination > 0 )
        {
            AssassinationText.text = EmployeeData.Assassination.ToString();
        }
        AssassinationSlider.value = EmployeeData.Assassination;

        StealthText.text = string.Empty;
        if ( EmployeeData.Stealth > 0 )
        {
            StealthText.text = EmployeeData.Stealth.ToString();
        }
        StealthSlider.value = EmployeeData.Stealth;
    }
}
