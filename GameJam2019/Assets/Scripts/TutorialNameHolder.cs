using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialNameHolder : MonoBehaviour {

    [SerializeField]
    private Text NameText;
	// Use this for initialization
	void OnEnable () {
        NameText.text = "<color=red>" + Manager.Current.PlayerName + "</color>\n\nWelcome to Cybercom Solutions, experts in espionage and assassination since 2076.\n\nWe've analyzed your qualifications and found you to be adequate at neither espionage nor assassination, so you've been hired to manage some of our field agents as they perform their day to day tasks.\nIn order to help you in succeed in this thrilling career, we've kindly prepared a series of slides for you to look at with your eyes/optical array and store in your brain/memory bank.";

    }
}
