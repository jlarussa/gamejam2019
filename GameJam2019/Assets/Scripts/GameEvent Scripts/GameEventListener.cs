using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour, IGameEventListener
{
    public GameEvent Event;
    public UnityEvent Response;

    void OnEnable()
    {
        Event.RegisterListener( this );
    }

    void OnDisable()
    {
        Event.UnregisterListener( this );
    }

    public virtual void OnEventRaised( GameEvent e )
    {
        Response.Invoke();
    }
}
