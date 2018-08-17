using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class T_HUD : MonoBehaviour {

	#region Variables

	[Header("Object References")]
	[SerializeField] private GameObject healthFrame;
	[SerializeField] private GameObject[] healthIcons = new GameObject[4];
	[SerializeField] private GameObject dashImage;
	[SerializeField] private GameObject potionImage;
	[SerializeField] private TextMeshProUGUI potionCountText;
	[SerializeField] private TextMeshProUGUI creditText;
	[SerializeField] private TextMeshProUGUI scoreText;

	#endregion



	public void SetScoreText(int score) {
		scoreText.text = "Score: " + score;
	}

	public void StartLevel(int level) {
		switch (level) {
			case 3:

				goto case 2;
			case 2:

				goto case 1;
			case 1:
				healthFrame.SetActive(true);
				goto case 0;
			case 0:
				scoreText.text = "Score: 0";
				goto default;
			default:
				this.gameObject.SetActive(true);
				break;

		}
	}

	public void SetHealth(int value) {
		for (int i = 0; i < 4; i++) {
			if ((i + 1) <= value) { healthIcons[i].SetActive(true); }
			else { healthIcons[i].SetActive(false); }
		}
	}

	public void DeactivateAll() {
		healthFrame.SetActive(false);
		dashImage.SetActive(false);
		potionImage.SetActive(false);
		creditText.gameObject.SetActive(false);
		SetHealth(4);
	}

}
