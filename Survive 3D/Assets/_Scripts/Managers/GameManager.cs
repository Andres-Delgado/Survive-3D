using System.Collections;
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

	public bool keyboardInput { get; set; }
	public bool isPaused { get; set; }
	public bool running { get; set; }

	#endregion

	private void Awake() {
		_instance = this;
		keyboardInput = true;
		isPaused = false;
		running = false;
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
		UIManager.Instance.MenuOnOff(false);
	}

	public void EndGame(int choice) {
		running = false;
		SpawnManager.Instance.EndGame(choice);
		switch (choice) {
			case 1:
				UIManager.Instance.AbilityOn(1);
				break;
			case 2:
				UIManager.Instance.AbilityOn(2);
				break;
			default:
				UIManager.Instance.MenuOnOff(true);
				break;
		}
	}

	public void ExitGame() {
		Application.Quit();
	}
}
