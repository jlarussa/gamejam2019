using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EmployeeDragger : EventTrigger {

private bool dragging;

private Vector2 originalLocation;
	
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
		originalLocation = this.gameObject.transform.position;
	}

	public override void OnPointerUp( PointerEventData eventData )
	{
		dragging = false;
		transform.position = originalLocation;
	}

}
