using UnityEngine;
using System.Collections;

public class AnimObject : MonoBehaviour {
	public enum Type {
		SCORE,
		BACKGROUND
	}
	public Type type;

	private RectTransform rectTransform;
	private float lerp;

	private bool startAnim;

	// Background type
	private bool left, right;
	private float defX, defY;

	// Use this for initialization
	void Start () {
		rectTransform = GetComponent<RectTransform> ();

		switch (type) {

		case Type.BACKGROUND:
			defX = rectTransform.anchoredPosition.x;
			defY = rectTransform.anchoredPosition.y;
				break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (startAnim) {
			switch (type) {
			case Type.SCORE:
				AnimateScore ();
				break;

			case Type.BACKGROUND:
				AnimateBG ();
				break;
			}
		}
	}

	public void Animate() {
		startAnim = true;
		lerp = 0f;
	}

	private void AnimateScore() {
		if (lerp >= 1f) {
			startAnim = false;
		} else {
			lerp += Time.deltaTime * 8f;

			float scale = Mathfx.Berp (0.3f, 1f, lerp);
			rectTransform.localScale = new Vector3 (scale, scale, 1f);
		}
	}

	private void AnimateBG() {
//		if (up) {
//			
//		}
	}
}
