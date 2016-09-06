using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;

public class SceneManager : MonoBehaviour {

	[Header("Slider Control")]
    public Slider mainSlider;
	public Image slideIndicator;
	public Sprite[] sliders;

	[Header("Button")]
	public Image buttonBG;
	public Text buttonText;
	public Sprite[] colors;

	[Header("Text")]
    public Text leftText;
    public Text rightText;
    public Text secondText;
    public Text scoreText;
	public AnimObject scoreTextAnim;

    [Header("Game Over Stuff")]
    public List<Text> removeTexts;
    public List<Image> removeImage;
//    public Text finishedText;
	public PanelCustom gameOverPanel;

    [Header("Sound")]
    public AudioSource coinSound;
    public AudioSource wrongSound;

	[Header("Panel")]
	public PanelManager panelManager;

    private Setting setting;

    private GameplayData gameplayData;
    private bool isGameOver = false;

	private bool isStarted;

	// Use this for initialization
	void Start () {
		setting = GetComponent<Setting> ();

        Random.seed = (int)(System.DateTime.UtcNow.Subtract(new System.DateTime(1970, 1, 1))).TotalSeconds;
        gameplayData = new GameplayData(1, 3);

        mainSlider.minValue = gameplayData.getLowerBound();
        mainSlider.maxValue = gameplayData.getUpperBound();

		gameOverPanel.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		bool backPressed = Input.GetKeyDown (KeyCode.Escape);
		if (backPressed) {
			if (panelManager.panelType == PanelManager.PanelType.MENU) {
				Application.Quit ();
			} else {
				Application.LoadLevel (Application.loadedLevel);
			}
		}

        if (this.isGameOver)
            return;

        this.leftText.text = mainSlider.value.ToString();
        this.rightText.text = gameplayData.getTarget().ToString();
        this.scoreText.text = gameplayData.getScore().ToString();

		// update slider indicator
		if (mainSlider.value == gameplayData.getTarget ()) {
			slideIndicator.sprite = sliders [1];
			buttonBG.sprite = colors[1];
			buttonText.text = "HIT ME!";
		} else {
//			slideIndicator.color = new Color (1f, 0.6f, 0f);
			slideIndicator.sprite = sliders[0];
			buttonBG.sprite = colors[0];
			buttonText.text = "NOT YET";
		}

		if (isStarted) {
			gameplayData.decreaseSecondLeft (Time.deltaTime);
			this.secondText.text = gameplayData.getSecondLeft ().ToString ("0");

			if (gameplayData.getSecondLeft () <= 0.0f)
				gameOver ();
		}
	}

	public void StartGame() {
		isStarted = true;
	}

    public void onWhozzClick() {    
        // Indicator is on the center
        if (!isGameOver && mainSlider.value == gameplayData.getTarget()) {
			scoreTextAnim.Animate ();
            nextLevel();
            return;
        } else if (!isGameOver && mainSlider.value != gameplayData.getTarget()) {
            gameplayData.decreaseSecondLeft(2.0f);
			if (setting.VibrateActive ()) {
				Handheld.Vibrate ();
			}

			if (setting.SoundActive()) {
                wrongSound.Stop();
                wrongSound.Play();
            }
            return;
        }

        if (isGameOver) {
            Application.LoadLevel(Application.loadedLevel);
            return;
        }
    }

    public void onValueSliderChange() {
		if (setting.SoundActive())
            coinSound.Play();
    }

    private void nextLevel() {
        gameplayData.addScore(gameplayData.getUpperBound() * (long)((gameplayData.getSecondLeft() / 5) + 1));
        gameplayData.nextLevel();

        mainSlider.minValue = gameplayData.getLowerBound();
        mainSlider.maxValue = gameplayData.getUpperBound();
        int pointer = (int)Random.Range(mainSlider.minValue, mainSlider.maxValue);
        while (pointer == gameplayData.getTarget()) {
            pointer = (int)Random.Range(mainSlider.minValue, mainSlider.maxValue);
        }
        mainSlider.value = pointer;

        gameplayData.addSecondLeft(2.0f);
    }

    private void gameOver() {
        // TODO implement game over
        isGameOver = true;

        for (int i = 0; i < removeTexts.Count; i++)
            removeTexts[i].enabled = false;
        for (int i = 0; i < removeImage.Count; i++)
            removeImage[i].enabled = false;
        mainSlider.enabled = false;

		buttonBG.sprite = colors[1];
        buttonText.text = "SLIDE AGAIN";

//        finishedText.enabled = true;
		gameOverPanel.Show();

		panelManager.panelType = PanelManager.PanelType.GAMEOVER;

		Social.ReportScore(gameplayData.getScore(), "CggI-vvHtVEQAhAB", (bool success) => {
			if(success) {
				Debug.Log("Success");
			} else {
				Debug.Log("Failed to post");
			}
		});

        Debug.logger.Log("Game Over");
    }
}
