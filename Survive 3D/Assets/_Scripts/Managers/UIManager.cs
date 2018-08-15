﻿using System.Collections;
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
		mainMenu.SetCreditText(value);
	}

	public void SetScoreText(int scoreValue, int highValue) {
		hud.SetScoreText(scoreValue, highValue);
		mainMenu.SetHighScore(highValue);
	}

	public void SetHealth(int value) {
		hud.SetHealth(value);
	}

	public void SetPotionText(int value) {
		hud.SetPotionText(value);
	}

	public void TurnOnDash() {
		hud.TurnOnDash();
	}

	public void TurnOnBulletWave() {
		hud.TurnOnBulletWave();
	}

	public void SetDash(bool canDash) {
		if (canDash) {
			hud.DashingOff();
		}
		else { hud.DashingOn(); }
	}

	public void SetBulletWave(bool canBulletWave) {
		if (canBulletWave) {
			hud.BulletWaveOff();
		}
		else { hud.BulletWaveOn(); }
	}

	public void MenuOn() {
		Cursor.visible = true;
		abilityScreen.gameObject.SetActive(false);
		pauseMenu.gameObject.SetActive(false);
		hud.gameObject.SetActive(false);
		mainMenu.gameObject.SetActive(true);
	}

	public void MenuOff() {
		if (!GameManager.Instance.keyboardInput) { Cursor.visible = false; }
		mainMenu.gameObject.SetActive(false);
		hud.gameObject.SetActive(true);
	}

	public void AbilityOn(int choice) {
		Cursor.visible = true;
		pauseMenu.gameObject.SetActive(false);
		hud.gameObject.SetActive(false);
		abilityScreen.gameObject.SetActive(true);
		switch (choice) {
			case 0:
				abilityScreen.DashScreenOn();
				break;
			case 1:
				abilityScreen.bulletScreenOn();
				break;
			default:
				break;
		}
	}
}
