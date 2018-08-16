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

	public void TurnOnAbility(int choice) {
		switch (choice) {
			case 0:
				dashImage.SetActive(true);
				break;
			case 1:
				bulletWaveImage.SetActive(true);
				break;
			default:
				break;
		}
	}

	public void DashingOnOff(bool indash) {
		if (indash) {
			dashImage.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
		} else {
			dashImage.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
		}
	}

	public void BulletWaveOnOff(bool inBulletWave) {
		if (inBulletWave) {
			bulletWaveImage.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
		} else {
			bulletWaveImage.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
		}
	}

}
