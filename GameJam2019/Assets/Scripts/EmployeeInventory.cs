using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeInventory : MonoBehaviour
{
    public int MaxEmployees = 10;
    public int RecruitCost = 300;

    public int DefaultAllotmentPoints = 4;
    public int MaxDefaultPointsPerSkill = 2;

    public List<GameObject> Employees = new List<GameObject>();

    public GameObject EmployeeUIPrefab = null;
    public Transform EmployeeUIParent = null;
    public StringList EmployeeFirstNames = null;
    public StringList EmployeeLastNames = null;
    public StringList EmployeeDescriptions = null;
    public ImageList Portraits = null;

    private List<int> availablePortaitIndexes = new List<int>();

    void Start()
    {
        for ( int i = 0; i < Portraits.Images.Count; i++ )
        {
            availablePortaitIndexes.Add( i );
        }
    }

    public void RecruitNewEmployee()
    {
        if ( Employees.Count < MaxEmployees )
        {
            if ( Manager.Current.CanChangeMoney( -1 * RecruitCost ) )
            {
                Employees.Add( CreateNewEmployee() );
                Manager.Current.MoneyChanged( -1 * RecruitCost );
            }
        }
    }

    private GameObject CreateNewEmployee()
    {
        Employee e = new Employee();
        
        // TODO: randomly pick image
        int x = EmployeeFirstNames.Strings.Count;
        int y = Random.Range( 0, x );
        string firstName = EmployeeFirstNames.Strings[ y ];
        //string firstName = EmployeeFirstNames.Strings[ Random.Range( 0, EmployeeFirstNames.Strings.Count ) ];
        string lastName = EmployeeLastNames.Strings[ Random.Range( 0, EmployeeLastNames.Strings.Count ) ];
        e.FirstName = firstName;
        e.LastName = lastName;

        e.Description = EmployeeDescriptions.Strings[ Random.Range( 0, EmployeeDescriptions.Strings.Count ) ];

        // Randomly assign skill points
        int points = DefaultAllotmentPoints;
        while ( points > 0 )
        {
            int whichSkill = Random.Range( 0, 3);
            switch ( whichSkill )
            {
                case 0:
                {
                    if ( e.Stealth < MaxDefaultPointsPerSkill )
                    {
                        e.Stealth++;
                        points--;
                    }
                    break;
                }
                case 1:
                {
                    if ( e.Hacking < MaxDefaultPointsPerSkill )
                    {
                        e.Hacking++;
                        points--;
                    }
                    break;
                }
                case 2:
                {
                    if ( e.Assassination < MaxDefaultPointsPerSkill )
                    {
                        e.Assassination++;
                        points--;
                    }
                    break;
                }
                default:
                    break;
            }
        }
        var instance = GameObject.Instantiate( EmployeeUIPrefab, EmployeeUIParent );
        instance.SetActive(true);
        
        // This is dumb but I'm being lazy and sloppy for the sake of speed
        // God save the queen
        if ( availablePortaitIndexes.Count > 0 )
        {
            int i = Random.Range( 0, availablePortaitIndexes.Count );
            e.PortraitIndex = availablePortaitIndexes[ i ];
            availablePortaitIndexes.RemoveAt( i );
        }
        else
        {
            e.PortraitIndex = Random.Range( 0, Portraits.Images.Count );
        }
        
        var view = instance.GetComponent<EmployeeView>();
        view.EmployeeData = e;
        view.PortraitUI.sprite = Portraits.Images[ e.PortraitIndex ];

        return instance;
    }
}
