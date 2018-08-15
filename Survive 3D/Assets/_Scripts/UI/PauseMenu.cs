using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PauseMenu : MonoBehaviour {

	#region Variables

	[Header("Object References")]
	[SerializeField] private TextMeshProUGUI pauseText;

	public void PauseOn() {
		Time.timeScale = 0.0f;
		this.gameObject.SetActive(true);
		GameManager.Instance.isPaused = true;
	}

	public void PauseOff() {
		Time.timeScale = 1.0f;
		this.gameObject.SetActive(false);
		GameManager.Instance.isPaused = false;
	}

	public void MainMenuFromPause() {
		Time.timeScale = 1.0f;
		GameManager.Instance.isPaused = false;
		GameManager.Instance.EndGame(9);
	}

	#endregion
}
