using UnityEngine;
using System.Collections;

public class PanelCustom : MonoBehaviour {
	public RectTransform rectTransform;

	private bool isShowing, isClosing;
	private float lerp;

	// Use this for initialization
	void Start () {
		rectTransform = GetComponent<RectTransform> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isShowing) {
			if (lerp >= 1f) {
				isShowing = false;
			} else {
				lerp += Time.deltaTime * 3f;
				float posY = Mathfx.Berp (1000, 0, lerp);

				rectTransform.anchoredPosition = new Vector2 (0f, posY);
			}
		}
	}

	public void Show() {
		lerp = 0f;
		isShowing = true;
		gameObject.SetActive (true);
	}
}
