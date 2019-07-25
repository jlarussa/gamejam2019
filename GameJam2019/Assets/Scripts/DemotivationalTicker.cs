using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemotivationalTicker : MonoBehaviour {

	public Text Text;
	public StringList Messages;
	public RectTransform scrollyBit;

	public float ScrollSpeed = 5;
	public float BaseSecondsBetweenMessages = 15;
	public float OffsetSecondsBetweenMessages = 5;

	private float showNextMessageCountdown = 0;

	// Don't repeat messages twice in a row
	private int lastMessageIndex = -1;

	// Use this for initialization
	void Start () {
		//AddTime();
	}

	private void AddTime()
	{
		showNextMessageCountdown = BaseSecondsBetweenMessages;
		showNextMessageCountdown += Random.value * OffsetSecondsBetweenMessages;
	}
	
	// Update is called once per frame
	void Update () {
		showNextMessageCountdown -= Time.deltaTime;
		float xOffset = ScrollSpeed * Time.deltaTime;
		scrollyBit.anchoredPosition = new Vector2( scrollyBit.anchoredPosition.x - xOffset, 0 );

		if ( showNextMessageCountdown < 0 )
		{
			// This is dangerous... but danger is my middle name.
			int messageIndex = lastMessageIndex;
			while ( messageIndex == lastMessageIndex )
			{
				messageIndex = Random.Range( 0, Messages.Strings.Count );
			}
			
			Text.text = Messages.Strings[ messageIndex ];
			scrollyBit.anchoredPosition = Vector2.zero;
			AddTime();
		}
	}


}
