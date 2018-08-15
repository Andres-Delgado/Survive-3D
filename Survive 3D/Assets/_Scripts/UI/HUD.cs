using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour {

	#region Variables

	[Header("Object References")]
	[SerializeField] private GameObject[] healthIcons = new GameObject[4];
	[SerializeField] private GameObject dashImage;
	[SerializeField] private GameObject bulletWaveImage;
	[SerializeField] private TextMeshProUGUI potionCountText;
	[SerializeField] private TextMeshProUGUI creditText;
	[SerializeField] private TextMeshProUGUI scoreText;
	[SerializeField] private TextMeshProUGUI highScoreText;


	#endregion

	public void SetCreditText(int value) {
		creditText.text = "Credits: " + value;
	}

	public void SetScoreText(int scoreValue, int highValue) {
		scoreText.text = "Score: " + scoreValue;
		highScoreText.text = "Highscore: " + highValue;
	}

	public void SetHealth(int value) {

		for (int i = 0; i < 4; i++) {
			if ((i + 1) <= value) { healthIcons[i].SetActive(true); }
			else { healthIcons[i].SetActive(false); }
		}
	}

	public void SetPotionText(int value) {
		potionCountText.text = "x " + value;
	}

	public void TurnOnDash() {
		dashImage.SetActive(true);
	}

	public void TurnOnBulletWave() {
		bulletWaveImage.SetActive(true);
	}

	public void DashingOn() {
		dashImage.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
	}

	public void DashingOff() {
		dashImage.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
	}

	public void BulletWaveOn() {
		bulletWaveImage.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
	}

	public void BulletWaveOff() {
		bulletWaveImage.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
	}

}
