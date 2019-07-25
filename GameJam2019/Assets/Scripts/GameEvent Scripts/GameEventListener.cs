using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
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

    public void OnEventRaised()
    {
        Response.Invoke();
    }

    public void OnEventRaised( GameObject go )
    {
        Response.Invoke();
    }
}
