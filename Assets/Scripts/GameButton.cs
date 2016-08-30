using UnityEngine;
using System.Collections;

public class GameButton : MonoBehaviour {
	public PanelManager panelManager;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GoToGameScene() {
		panelManager.ChangePanel (PanelManager.PanelType.GAMEPLAY);
	}

	public void GoToMenuScene() {
		panelManager.ChangePanel (PanelManager.PanelType.MENU);
	}
}
