using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_GameManager : MonoBehaviour {

	#region Variables

	private static T_GameManager _instance;
	public static T_GameManager Instance {
		get {
			if (!_instance) {
				Debug.Log("Error: T_GameManager is null");
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

	/*private void Update() {
		if (!running) { return; }
		if (keyboardInput) {
			if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) {
				//UIManager.Instance.PauseToggle(isPaused);
			}
		}
		else {
			if (Input.GetButtonDown("ControllerStart")) {
				//UIManager.Instance.PauseToggle(isPaused);
			}
		}
	}*/

	public void StartLevel(int level) {
		running = true;
		T_UIManager.Instance.StartLevel();
		T_SpawnManager.Instance.StartLevel(level);
	}

	public void EndLevel(int level, bool completed = false) {
		if (!completed) {
			T_SpawnManager.Instance.EndLevel();
		}
		running = false;
		T_UIManager.Instance.MenuOn();
	}
}
