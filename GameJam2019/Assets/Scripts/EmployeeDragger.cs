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
			var v3 = new Vector3( Input.mousePosition.x, Input.mousePosition.y, 10 );
      transform.position = Camera.main.ScreenToWorldPoint( v3 );

    }
	}

	public override void OnPointerDown( PointerEventData eventData)
	{
        base.OnPointerDown(eventData);
		dragging = true;
		originalLocation = this.gameObject.transform.position;
	}

	public override void OnPointerUp( PointerEventData eventData )
	{
        base.OnPointerUp (eventData);
        dragging = false;
		transform.position = originalLocation;
	}

}
