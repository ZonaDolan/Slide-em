using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Setting : MonoBehaviour {

    [Header("Sound Setting")]
    public Image soundIcon;
	public Image vibrateIcon;

	public Sprite[] soundStates;
	public Sprite[] vibrateStates;

    private bool isSoundActive;
	private bool isVibrateActive;

	void Awake() {
		isSoundActive = PlayerPrefs.GetInt ("SOUND", 1) == 1;
		isVibrateActive = PlayerPrefs.GetInt ("VIBRATE", 1) == 1;
	}

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (isSoundActive)
			soundIcon.sprite = soundStates [1];
        else 
			soundIcon.sprite = soundStates [0];

		if (isVibrateActive)
			vibrateIcon.sprite = vibrateStates [1];
		else
			vibrateIcon.sprite = vibrateStates [0];
	}

    public void OnSoundClick() {
        isSoundActive ^= true;

		int state = (isSoundActive) ? 1 : 0;
		PlayerPrefs.SetInt ("SOUND", state);
    }

	public void OnVibrateClick() {
		isVibrateActive ^= true;

		int state = (isVibrateActive) ? 1 : 0;
		PlayerPrefs.SetInt ("VIBRATE", state);
	}

    public bool SoundActive() { return isSoundActive; }
	public bool VibrateActive() { return isVibrateActive; }
}
