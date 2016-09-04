using UnityEngine;
using System.Collections;

public class SettingButton : MonoBehaviour {
	public Setting setting;

	// Use this for initialization
	void Start () {
	
	}


	public void OnSoundClick() {
		setting.OnSoundClick ();
	}

	public void OnVibrateClick() {
		setting.OnVibrateClick ();
	}
}
