using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEventWithObject : ScriptableObject {

		protected List<IGameEventWithObjectListener> listeners = new List<IGameEventWithObjectListener>();

    public virtual void Raise( GameObject go )
    {
        //Debug.Log( this.name + " was raised.");
        for ( int i = listeners.Count - 1; i >= 0; i-- )
        {
            listeners[ i ]?.OnEventRaised( this, go );
        }
    }

    public void RegisterListener( IGameEventWithObjectListener listener )
    {
        listeners.Add( listener );
    }

    public void UnregisterListener( IGameEventWithObjectListener listener )
    {
        listeners.Add( listener );
    }
}
