using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class GameStateChangeEvent : UnityEvent<GameState,GameState>
{
    // public GameState OldState { get; private set; }
    // public GameState NewState { get; private set; }
    
    // public GameStateChangeEvent( GameState oldState, GameState newState )
    // {
    //     OldState = oldState;
    //     NewState = newState;
    // }
}
