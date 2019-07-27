using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySFXOnEvent : PlaySFX, IGameEventListener
{
  [SerializeField]
  private GameEvent gameEvent;

  public void OnEventRaised( GameEvent e )
  {
    playClip();
  }

  private void OnEnable()
  {
    gameEvent.RegisterListener( this );
  }

  private void OnDisable()
  {
    gameEvent.UnregisterListener( this );
  }
}
