using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventWithObject : GameEvent {

	public void Raise( GameObject go )
    {
        Debug.Log( this.name + " was raised.");
        for ( int i = listeners.Count - 1; i >= 0; i-- )
        {
            listeners[ i ]?.OnEventRaised( go );
        }
    }
}
