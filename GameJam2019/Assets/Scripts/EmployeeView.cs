using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmployeeView : MonoBehaviour
{
    public Image PortraitUI;
    public Color PortraitColorHacking;
    public Color PortraitColorAssassination;
    public Color PortraitColorStealth;

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

    [SerializeField]
    private Text employeeDescription;

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
            employeeDescription.text = employeeData.Description;
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

    private float barSliverAmt = 0.1f;
    private void UpdateStatViews()
    {
        HackingText.text = string.Empty;
        if ( EmployeeData.Hacking > 0 )
        {
            HackingText.text = EmployeeData.Hacking.ToString();
        }
        HackingSlider.value = Mathf.Max( barSliverAmt, EmployeeData.Hacking );

        AssassinationText.text = string.Empty;
        if ( EmployeeData.Assassination > 0 )
        {
            AssassinationText.text = EmployeeData.Assassination.ToString();
        }
        AssassinationSlider.value = Mathf.Max( barSliverAmt, EmployeeData.Assassination);

        StealthText.text = string.Empty;
        if ( EmployeeData.Stealth > 0 )
        {
            StealthText.text = EmployeeData.Stealth.ToString();
        }
        StealthSlider.value = Mathf.Max( barSliverAmt, EmployeeData.Stealth );
        TintPortait();
    }

    private void TintPortait()
    {
        // 0 is hacking, 1 is assassination, 2 is stealth
        int highestStat = 0;
        if ( EmployeeData.Assassination > EmployeeData.Hacking )
        {
            highestStat = 1;
        }
        if ( EmployeeData.Stealth > EmployeeData.Hacking && EmployeeData.Stealth > EmployeeData.Assassination )
        {
            highestStat = 2;
        }
        switch ( highestStat )
        {
            case 0:
                PortraitUI.color = PortraitColorHacking;
                break;
            case 1:
                PortraitUI.color = PortraitColorAssassination;
                break;
            case 2:
                PortraitUI.color = PortraitColorStealth;
                break;
        }
    }
}
