using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
    protected List<IGameEventListener> listeners = new List<IGameEventListener>();

    public virtual void Raise()
    {
        //Debug.Log( this.name + " was raised.");
        for ( int i = listeners.Count - 1; i >= 0; i-- )
        {
            listeners[ i ]?.OnEventRaised( this );
        }
    }

    public void RegisterListener( IGameEventListener listener )
    {
        listeners.Add( listener );
    }

    public void UnregisterListener( IGameEventListener listener )
    {
        listeners.Add( listener );
    }
}
