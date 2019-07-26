using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {
	
	public PrefabList tutorialList;
	public Transform tutorialScreenParent;

	public Text DoneButtonText;
	public GameEvent TutorialCompleteEvent;

	private int index = -1;
	private GameObject currentTutorial;

	public void PrevTutorial()
	{
		index = Mathf.Max( 0, index - 1);
		LoadTutorial();
	}
	
	public void NextTutorial()
	{
		// Tutorial is done
		if ( index == tutorialList.Prefabs.Count - 1 )
		{
			TutorialCompleteEvent.Raise();
			index = -1;
			GameObject.Destroy( currentTutorial );
			return;
		}

		index = Mathf.Min( tutorialList.Prefabs.Count - 1, index + 1 );
		LoadTutorial();
	}

	private void LoadTutorial()
	{
		if ( index == tutorialList.Prefabs.Count - 1 )
		{
			DoneButtonText.text = "Done!";
		}
		else
		{
			DoneButtonText.text = "Next";
		}

		GameObject.Destroy( currentTutorial );
		currentTutorial = GameObject.Instantiate( tutorialList.Prefabs[ index ], tutorialScreenParent );
	}

}
