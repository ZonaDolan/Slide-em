using UnityEngine;
using System.Collections;

public class PanelManager : MonoBehaviour {
	[Header("Panel")]
	public GameObject menuPanel;
	public PanelCustom gamePanel;
	public GameObject gameOverPanel;

	[Header("Manager")]
	public SceneManager sceneManager;

	public enum PanelType {
		MENU,
		GAMEPLAY,
		GAMEOVER
	}
	private PanelType panelType;


	// Use this for initialization
	void Start () {
		ChangePanel (PanelType.MENU);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ChangePanel(PanelType type) {
		panelType = type;

		switch (type) {

		case PanelType.MENU:
			menuPanel.SetActive (true);
			gamePanel.gameObject.SetActive (false);
			gameOverPanel.SetActive (false);
			break;

		case PanelType.GAMEPLAY:
			menuPanel.SetActive (false);
//			gamePanel.SetActive (true);
			gamePanel.Show();
			gameOverPanel.SetActive (false);
			sceneManager.StartGame ();
			break;

		case PanelType.GAMEOVER:
			menuPanel.SetActive (false);
			gamePanel.gameObject.SetActive (false);
			gameOverPanel.SetActive (true);
			break;
		}
	}
}
