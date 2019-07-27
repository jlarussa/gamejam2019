using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDaddy : MonoBehaviour, IGameEventWithObjectListener {
	
	public GameEventWithObject BeginDragEvent;
	public GameEventWithObject EndDragEvent;
	public GameObject dragPlaceholder;

	void OnEnable()
    {
        BeginDragEvent.RegisterListener( this );
				EndDragEvent.RegisterListener( this );
    }

    void OnDisable()
    {
        BeginDragEvent.UnregisterListener( this );
				EndDragEvent.UnregisterListener( this );
    }

		public void OnEventRaised( GameEventWithObject e, GameObject go )
    {
			if ( e == BeginDragEvent )
			{
				// Take the object away from its current parent and put our placeholder there instead
				int sibIndex = go.transform.GetSiblingIndex();
				dragPlaceholder.transform.SetParent( go.transform.parent, worldPositionStays: false );

        go.transform.SetParent( this.transform, worldPositionStays: false );
				dragPlaceholder.transform.SetSiblingIndex(sibIndex);
			}
			else if ( e == EndDragEvent )
			{
				// Take our placeholder back and put the object back there instead
				int sibIndex = dragPlaceholder.transform.GetSiblingIndex();
				go.transform.SetParent( dragPlaceholder.transform.parent, worldPositionStays: false );

        dragPlaceholder.transform.SetParent( this.transform, worldPositionStays: false );
				go.transform.SetSiblingIndex(sibIndex);
			}
				
    }
	
}
