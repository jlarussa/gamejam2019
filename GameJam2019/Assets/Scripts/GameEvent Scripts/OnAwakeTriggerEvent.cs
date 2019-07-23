using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAwakeTriggerEvent : MonoBehaviour
{
    public GameEvent Event;

    public void Awake()
    {
        Event.Raise();
    }
}
