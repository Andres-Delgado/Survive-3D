using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_UIManager : MonoBehaviour {

	#region Variables

	private static T_UIManager _instance;
	public static T_UIManager Instance {
		get {
			if (!_instance) {
				Debug.Log("Error: T_UIManager is null");
			}
			return _instance;
		}
	}

	[Header("References")]
	[SerializeField] private T_Selection tSelection;
	[SerializeField] private T_Screen tScreen;
	[SerializeField] private T_HUD tHUD;
	[SerializeField] private PauseMenu pauseMenu;

	[Header("Variables")]
	[SerializeField] private int completed;

	//[Header("Variables")]

	#endregion

	private void Awake() {
		_instance = this;
	}


	public void PauseToggle(bool togglePause) {
		if (togglePause) {
			if (!T_GameManager.Instance.keyboardInput) {
				Cursor.visible = false;
			}
			pauseMenu.PauseOff(true);
		}
		else {
			Cursor.visible = true;
			pauseMenu.PauseOn(true);
		}
	}


	public void SelectLevel(int level) {
		tSelection.gameObject.SetActive(false);
		tScreen.SelectLevel(level);
	}

	public void ExitToMenu() {
		tScreen.gameObject.SetActive(false);
		tSelection.gameObject.SetActive(true);
	}

	public void StartLevel(int level) {
		tScreen.gameObject.SetActive(false);
		tSelection.gameObject.SetActive(false);
		tHUD.StartLevel(level);
	}

	public void MenuOn() {
		Cursor.visible = true;
		tScreen.DeactivateAll();
		tScreen.gameObject.SetActive(false);
		pauseMenu.gameObject.SetActive(false);
		tHUD.DeactivateAll();
		tHUD.gameObject.SetActive(false);
		tSelection.gameObject.SetActive(true);
	}

	public void CompletedLevel(int level) {
		tSelection.CompletedLevel(level);
		tHUD.SetHealth(4);
	}

	public void SetScoreText(int score) {
		tHUD.SetScoreText(score);
	}

	public void SetHealth(int health) {
		tHUD.SetHealth(health);
	}
}

