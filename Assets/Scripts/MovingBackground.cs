using UnityEngine;
using System.Collections;

public class MovingBackground : MonoBehaviour {
	private RectTransform rectTransform;

	// Use this for initialization
	void Start () {
		rectTransform = GetComponent<RectTransform> ();
	}
	
	// Update is called once per frame
	void Update () {
		float posX = -Input.acceleration.x;
		float posY = -Input.acceleration.y;

		rectTransform.anchoredPosition = new Vector2 (posX * 40f, posY * 40f);
	}

}
