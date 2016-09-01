using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour {

	[Header("Slider Control")]
    public Slider mainSlider;
	public Image slideIndicator;

	[Header("Button")]
	public Image buttonBG;
	public Text buttonText;
	public Color[] colors;

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

	public AudioSource coinSound;

    private GameplayData gameplayData;
    private bool isGameOver = false;

	private bool isStarted;

	// Use this for initialization
	void Start () {
        Random.seed = (int)(System.DateTime.UtcNow.Subtract(new System.DateTime(1970, 1, 1))).TotalSeconds;
        gameplayData = new GameplayData(1, 3);

        mainSlider.minValue = gameplayData.getLowerBound();
        mainSlider.maxValue = gameplayData.getUpperBound();

		gameOverPanel.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
        if (this.isGameOver)
            return;

        this.leftText.text = mainSlider.value.ToString();
        this.rightText.text = gameplayData.getTarget().ToString();
        this.scoreText.text = gameplayData.getScore().ToString();

		// update slider indicator
		if (mainSlider.value == gameplayData.getTarget ()) {
			slideIndicator.color = colors[1];
			buttonBG.color = colors[1];
			buttonText.text = "HIT ME!";
		} else {
//			slideIndicator.color = new Color (1f, 0.6f, 0f);
			slideIndicator.color = colors[0];
			buttonBG.color = colors[0];
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
        } else if (isGameOver) {
            Application.LoadLevel(Application.loadedLevel);
        }
    }

    public void onValueSliderChange() {
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

        buttonBG.color = colors[1];
        buttonText.text = "SLIDE AGAIN";

//        finishedText.enabled = true;
		gameOverPanel.Show();

        Debug.logger.Log("Game Over");
    }
}
