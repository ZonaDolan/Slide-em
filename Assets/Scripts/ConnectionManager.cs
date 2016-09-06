using UnityEngine;
using UnityEngine.SocialPlatforms;
using System.Collections;

using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class ConnectionManager : MonoBehaviour {

	public bool gpgActivated;

	// Use this for initialization
	void Start () {
		if(!Social.localUser.authenticated)
			ActivateGPG ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void ActivateGPG() {
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
			//.RequireGooglePlus()
			//.EnableSavedGames()
			.Build();

		PlayGamesPlatform.InitializeInstance(config);
		PlayGamesPlatform.DebugLogEnabled = true;
		PlayGamesPlatform.Activate();

		Social.localUser.Authenticate((bool success) => {
			// Handle success or failure
			if(success) {
				gpgActivated = true;
				Debug.Log("Social Active");
			} else
				gpgActivated = false;
			Debug.Log("Social Failed");
		});

	}

	public void ShowLeaderboard() {
		Social.ShowLeaderboardUI ();
	}
}
