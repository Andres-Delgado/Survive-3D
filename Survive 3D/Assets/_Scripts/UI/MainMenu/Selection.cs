using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Selection : MonoBehaviour {

	#region Variables 

	[Header("References")]
	[SerializeField] private TextMeshProUGUI highScoreText;
	[SerializeField] private TextMeshProUGUI creditsText;
	[SerializeField] private GameObject tutorialObject;
	[SerializeField] private GameObject exitCheckObject;

	private int highScore = 0;
	private int credits = 0;

	#endregion

	public void SetHighScore(int value) {
		highScore = value;
		highScoreText.text = "Highscore: " + highScore;
	}

	public void SetCreditText(int value) {
		credits = value;
		creditsText.text = "Credits: " + credits;
	}

	public void EnterExitTutorial(bool enter) {
		if (enter) {
			tutorialObject.SetActive(true);
			this.gameObject.SetActive(false);
		} else {
			this.gameObject.SetActive(true);
			tutorialObject.SetActive(false);
		}	
	}

	public void ExitGame(bool exit) {
		if (exit) {
			exitCheckObject.SetActive(true);
			this.gameObject.SetActive(false);
		} else {
			this.gameObject.SetActive(true);
			exitCheckObject.SetActive(false);
		}
	}

}
