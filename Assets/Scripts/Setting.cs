using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Setting : MonoBehaviour {

    [Header("Sound Setting")]
    public Image image;
    public Sprite soundActiveSprite;
    public Sprite soundNonActiveSprite;
    private bool isSoundActive;

    // Use this for initialization
    void Start () {
        // TODO Read from file (?)
        isSoundActive = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (isSoundActive) image.sprite = soundActiveSprite;
        else image.sprite = soundNonActiveSprite;
	}

    public void onSoundClick() {
        isSoundActive ^= true;
    }

    public bool soundActive() { return isSoundActive; }
}
