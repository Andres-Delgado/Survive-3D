﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class T_Selection : MonoBehaviour {

	#region Variables

	[Header("References")]
	[SerializeField] private GameObject[] tutorialLevels = new GameObject[4];
	[SerializeField] private GameObject[] selectButtons = new GameObject[4];
	[SerializeField] private TextMeshProUGUI tooltipText;



	#endregion

	public void SelectTutorial(int level) {
		try {
			HideButtons();

			tutorialLevels[level].transform.GetChild(0).gameObject.SetActive(true);
			selectButtons[level].SetActive(true);
			SetTooltipText(level);
		} catch (Exception e) { Debug.LogException(e, this); }
	}

	public void HideButtons(bool outsideClick = false) {
		try {
			for (int i = 0; i < 4; i++) {
				selectButtons[i].SetActive(false);
				tutorialLevels[i].transform.GetChild(0).gameObject.SetActive(false);
			}
			if (outsideClick) {
				SetTooltipText();
			}
		} catch (Exception e) { Debug.LogException(e, this); }
	}

	private void SetTooltipText() {
		tooltipText.text = "Press any one the tutorials to see a description here.";
	}

	private void SetTooltipText(int level) {
		switch (level) {
			case 0:
				tooltipText.text = "\"Basic Controls\" description";
				break;
			case 1:
				tooltipText.text = "\"Enemy Behavior\" description";
				break;
			case 2:
				tooltipText.text = "\"Items & Powerups\" description";
				break;
			case 3:
				tooltipText.text = "\"Skills & Abilities\" description";
				break;
			default:
				tooltipText.text = "";
				break;
		}
	}
	

}
