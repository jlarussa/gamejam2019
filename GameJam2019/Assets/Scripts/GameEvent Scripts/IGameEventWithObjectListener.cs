using UnityEngine;

public interface IGameEventWithObjectListener
{
		void OnEventRaised( GameEventWithObject e, GameObject go );
} 
