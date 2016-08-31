﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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

	public AudioSource coinSound;

    private GameplayData gameplayData;

	// Use this for initialization
	void Start () {
        Random.seed = (int)(System.DateTime.UtcNow.Subtract(new System.DateTime(1970, 1, 1))).TotalSeconds;
        gameplayData = new GameplayData(1, 3);

        mainSlider.minValue = gameplayData.getLowerBound();
        mainSlider.maxValue = gameplayData.getUpperBound();
	}
	
	// Update is called once per frame
	void Update () {
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

        gameplayData.decreaseSecondLeft(Time.deltaTime);
        this.secondText.text = gameplayData.getSecondLeft().ToString("0");

        if (gameplayData.getSecondLeft() <= 0.0f)
            gameOver();
	}

    public void onWhozzClick() {    
        // Indicator is on the center
        if (mainSlider.value == gameplayData.getTarget()) {
			scoreTextAnim.Animate ();
            nextLevel();
        }
    }

    public void onValueSliderChange() {
        coinSound.Play();
    }

    private void nextLevel() {
        gameplayData.addScore(gameplayData.getUpperBound() * (long)gameplayData.getSecondLeft());
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
        Debug.logger.Log("Game Over");
    }
}
