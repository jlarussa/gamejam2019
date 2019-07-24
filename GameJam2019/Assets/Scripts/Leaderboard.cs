using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class Leaderboard : MonoBehaviour {
	const string URL = "https://qrw62s8r54.execute-api.us-east-1.amazonaws.com/gj-prod/leaderboard";

	[Serializable]
	public class Score
	{
		public int score;
		public string name;
	}

	[Serializable]
	public class Entries
	{
		public Score[] scores;
	}

	public Entries entries;
	
	// Use this for initialization
	void Start () {
		StartCoroutine(GetRequest( URL ));
	}
	
	IEnumerator GetRequest(string url)
	{
		using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
		{
			// Request and wait for the desired page.
			yield return webRequest.SendWebRequest();
			
			if (webRequest.isNetworkError)
			{
				Debug.Log("Error: " + webRequest.error);
			}
			else
			{
				entries = JsonUtility.FromJson<Entries>( webRequest.downloadHandler.text );
			}
		}
	}
}
