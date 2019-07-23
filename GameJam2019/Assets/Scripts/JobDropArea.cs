using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobDropArea : MonoBehaviour 
{
    [SerializeField]
    private BoxCollider2D jobCollider = null;

    [SerializeField]
    private Job targetJob = null;

    private Collider pendingCollider = null;

    private void OnTriggerEnter( Collider other )
    {
        pendingCollider = other;
    }

    private void OnTriggerExit(Collider other)
    {
        pendingCollider = null;
    }

    private void OnMouseUp()
    {
        if (pendingCollider != null)
        {
            var Employee = pendingCollider?.gameObject?.GetComponent<EmployeeView>()?.EmployeeData;
            targetJob.AddEmployee(Employee);
        }
    }

}
