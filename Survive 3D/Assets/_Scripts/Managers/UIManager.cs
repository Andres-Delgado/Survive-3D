using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour {

	#region Variables

	private static UIManager _instance;
	public static UIManager Instance {
		get {
			if (!_instance) {
				Debug.Log("Error: UIManager is null");
			}
			return _instance;
		}
	}

	[Header("Object References")]
	[SerializeField] private MainMenu mainMenu;
	[SerializeField] private PauseMenu pauseMenu;
	[SerializeField] private HUD hud;
	[SerializeField] private AbilityScreen abilityScreen;

	#endregion

	private void Awake() {
		_instance = this;
	}

	public void PauseToggle(bool togglePause) {
		if (togglePause) {
			if (!GameManager.Instance.keyboardInput) {
				Cursor.visible = false;
			}
			pauseMenu.PauseOff();
		}
		else {
			Cursor.visible = true;
			pauseMenu.PauseOn();
		}
	}

	public void SetCreditText(int value) {
		hud.SetCreditText(value);
		mainMenu.SetMenuText(0, value);
	}

	public void SetScoreText(int scoreValue, int highValue) {
		hud.SetScoreText(scoreValue, highValue);
		mainMenu.SetMenuText(1, highValue);
	}

	public void SetHealth(int value) {
		hud.SetHealth(value);
	}

	public void SetPotionText(int value) {
		mainMenu.SetPotionText(value);
		hud.SetPotionText(value);
	}

	public void TurnOnAbility(int choice) {
		hud.TurnOnAbility(choice);
	}

	public void SetAbility(int choice, bool canActivate) {
		switch (choice) {
			case 0:
				hud.DashingOnOff(canActivate);
				break;
			case 1:
				hud.BulletWaveOnOff(canActivate);
				break;
			default:
				break;
		}
	}

	public void MenuOnOff(bool on) {
		if (on) {
			Cursor.visible = true;
			abilityScreen.gameObject.SetActive(false);
			pauseMenu.gameObject.SetActive(false);
			hud.gameObject.SetActive(false);
			mainMenu.gameObject.SetActive(true);
		} else {
			if (!GameManager.Instance.keyboardInput) { Cursor.visible = false; }
			mainMenu.gameObject.SetActive(false);
			hud.gameObject.SetActive(true);
		}
	}

	public void AbilityOn(int choice) {
		Cursor.visible = true;
		pauseMenu.gameObject.SetActive(false);
		hud.gameObject.SetActive(false);
		abilityScreen.gameObject.SetActive(true);
		switch (choice) {
			case 1:
				abilityScreen.DashScreenOn();
				break;
			case 2:
				abilityScreen.bulletScreenOn();
				break;
			default:
				break;
		}
	}
}
