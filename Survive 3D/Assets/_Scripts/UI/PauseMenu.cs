using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PauseMenu : MonoBehaviour {

	#region Variables

	[Header("Object References")]
	[SerializeField] private TextMeshProUGUI pauseText;

	public void PauseOn(bool inTutorial = false) {
		Time.timeScale = 0.0f;
		this.gameObject.SetActive(true);
		if (inTutorial) {
			T_GameManager.Instance.isPaused = true;
		} else { GameManager.Instance.isPaused = true; }
	}

	public void PauseOff(bool inTutorial = false) {
		Time.timeScale = 1.0f;
		this.gameObject.SetActive(false);
		if (inTutorial) {
			T_GameManager.Instance.isPaused = false;
		} else { GameManager.Instance.isPaused = false; }	
	}

	public void MainMenuFromPause(bool inTutorial = false) {
		Time.timeScale = 1.0f;
		
		if (inTutorial) {
			T_GameManager.Instance.isPaused = false;
			T_GameManager.Instance.EndLevel(0);
		} else {
			GameManager.Instance.isPaused = false;
			GameManager.Instance.EndGame(9);
		}
		
	}

	#endregion
}
