using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EmployeeDragger : EventTrigger {

private bool dragging;
	
	// Update is called once per frame
	void Update () {
		if ( dragging )
		{
			transform.position = new Vector2( Input.mousePosition.x, Input.mousePosition.y );
		}
	}

	public override void OnPointerDown( PointerEventData eventData)
	{
		dragging = true;
	}

	public override void OnPointerUp( PointerEventData eventData )
	{
		dragging = false;
	}

}
