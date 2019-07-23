using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    StartScreen = 0,
    Planning = 1,
    Playing = 2,
    Win = 3,
    Lose = 4,
}

public class GameStateMachine : MonoBehaviour
{
    public GameStateChangeEvent StateChangeEvent;

    // Start is called before the first frame update
    void Start()
    {
        if ( StateChangeEvent == null )
        {
            StateChangeEvent = new GameStateChangeEvent();
        }    
        StateChangeEvent.AddListener( Listener );
        StateChangeEvent.Invoke( GameState.StartScreen, GameState.Lose );
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Listener( GameState oldState, GameState newState )
    {
        Debug.Log( oldState.ToString() + " " + newState.ToString() );
    }
}
