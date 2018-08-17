using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class T_HUD : MonoBehaviour {

	#region Variables

	[Header("Object References")]
	[SerializeField] private GameObject[] healthIcons = new GameObject[4];
	[SerializeField] private GameObject dashImage;
	[SerializeField] private TextMeshProUGUI potionCountText;
	[SerializeField] private TextMeshProUGUI creditText;
	[SerializeField] private TextMeshProUGUI scoreText;

	#endregion

	private void OnEnable() {
		scoreText.text = "Score: 0";
	}

	public void SetScoreText(int score) {
		scoreText.text = "Score: " + score;
	}


}
