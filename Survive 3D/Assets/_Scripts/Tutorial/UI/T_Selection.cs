using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class T_Selection : MonoBehaviour {

	#region Variables

	[Header("References")]
	[SerializeField] private GameObject[] tutorialFrames = new GameObject[4];
	[SerializeField] private GameObject[] selectButtons = new GameObject[4];
	[SerializeField] private TextMeshProUGUI tooltipText;
	[SerializeField] private TextMeshProUGUI completedText;

	[Header("Variables")]
	[SerializeField] private bool selected = false;

	private bool[] completedLevels = new bool[4];
	private int completed = 0;

	#endregion

	private void Update() {
		if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) ||
			Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow)) && !selected) {
			selected = true;
			tutorialFrames[0].GetComponent<Button>().Select();
		}
		
	}

	private void OnEnable() { HideButtons(true); }

	public void CompletedLevel(int level) {
		if (completedLevels[level]) { return; }
		completedLevels[level] = true;
		completed++;
		completedText.text = "Completed " + completed + "/4";
		tutorialFrames[level].transform.GetChild(3).gameObject.SetActive(true);
	}

	public void SelectTutorial(int level) {
		try {
			HideButtons();
			selected = true;
			tutorialFrames[level].transform.GetChild(0).gameObject.SetActive(true);
			selectButtons[level].SetActive(true);
			SetTooltipText(level);
		} catch (Exception e) { Debug.LogException(e, this); }
	}

	public void HideButtons(bool outsideClick = false) {
		try {
			for (int i = 0; i < 4; i++) {
				selectButtons[i].SetActive(false);
				tutorialFrames[i].transform.GetChild(0).gameObject.SetActive(false);
			}
			if (outsideClick) {
				SetTooltipText();
			}
		} catch (Exception e) { Debug.LogException(e, this); }
	}

	private void SetTooltipText() {
		selected = false;
		EventSystem.current.SetSelectedGameObject(null);
		tooltipText.text = "Press any of the tutorials to see a description here.";
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
