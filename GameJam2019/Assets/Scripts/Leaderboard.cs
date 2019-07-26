using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

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

	[Serializable]
	public class PostResponse
	{
		public int statusCode;
		public Entries body;
	}

	[SerializeField]
	private Text leaderboardText;

	private Entries rows;
	public Entries Rows
	{
		get { return rows; }
		private set
		{
			rows = value;
			leaderboardText.text = formatEntries();
		}
	}
	
	// Use this for initialization
	void OnEnable ()
	{
		StartCoroutine( GetLeaderboard( URL ) );
		//StartCoroutine(PostScore( URL, "Theo", 9003 ) );
	}
	
	IEnumerator GetLeaderboard(string url)
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
				Rows = JsonUtility.FromJson<Entries>( webRequest.downloadHandler.text );
				// Debug.Log( entries.scores[0].name + ": " + entries.scores[0].score );
			}
		}
	}

	IEnumerator PostScore( string url, string playerName, int playerScore )
	{
		string urlWithScore = url + String.Format( "?name={0}&score={1}", playerName, playerScore );

		using ( UnityWebRequest webRequest = UnityWebRequest.Post( urlWithScore, "" ) )
		{
			webRequest.SetRequestHeader( "Content-Type", "application/json" );
			yield return webRequest.SendWebRequest();
			
			if (webRequest.isNetworkError)
			{
				Debug.Log("Error: " + webRequest.error);
			}
			else
			{
				PostResponse resp = JsonUtility.FromJson<PostResponse>( webRequest.downloadHandler.text );
				// Debug.Log( resp.body.scores[0].name + ": " + resp.body.scores[0].score );
			}
		}
	}

	private string formatEntries()
	{
		string result = "";
		foreach ( Score s in Rows.scores )
		{
			result = result + String.Format( "{0}\t-\t{1}\n", s.name, s.score.ToString() );
		}
		
		return result;
	}
}