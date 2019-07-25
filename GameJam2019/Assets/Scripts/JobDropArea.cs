using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobDropArea : MonoBehaviour 
{
    [SerializeField]
    private BoxCollider2D jobCollider = null;

    [SerializeField]
    private JobView targetJob = null;

    private Collider2D pendingCollider = null;

    private void OnTriggerEnter2D( Collider2D other )
    {
        pendingCollider = other;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        pendingCollider = null;
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (pendingCollider != null)
            {
                var Employee = pendingCollider?.gameObject?.GetComponent<EmployeeView>()?.EmployeeData;
                targetJob.AddEmployee(Employee);
            }
        }
    }

}
