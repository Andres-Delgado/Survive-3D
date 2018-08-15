﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	#region Variables

	private static GameManager _instance;
	public static GameManager Instance {
		get {
			if (!_instance) {
				Debug.Log("Error: GameManager is null");
			}
			return _instance;
		}
	}

	[Header("Variables")]
	public bool keyboardInput = true;
	public bool isPaused = false;
	public bool running = false;

	#endregion

	private void Awake() {
		_instance = this;
	}

	private void Start() {

	}

	private void Update() {
		if (!running) { return; }
		if (keyboardInput) {
			if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) {
				UIManager.Instance.PauseToggle(isPaused);
			}
		}
		else {
			if (Input.GetButtonDown("ControllerStart")) {
				UIManager.Instance.PauseToggle(isPaused);
			}
		}
	}

	public void StartGame() {
		running = true;
		SpawnManager.Instance.StartGame();
		UIManager.Instance.MenuOff();
	}

	public void EndGame(int choice) {
		running = false;
		SpawnManager.Instance.EndGame(choice);
		switch (choice) {
			case 1:
				UIManager.Instance.AbilityOn(0);
				break;
			case 2:
				UIManager.Instance.AbilityOn(1);
				break;
			default:
				UIManager.Instance.MenuOn();
				break;
		}
	}

	public void ExitGame() {
		Application.Quit();
	}
}