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


	#endregion

	private void Awake() {
		_instance = this;
		keyboardInput = true;
		isPaused = false;
	}

	public void StartLevel(int level) {

		T_SpawnManager.Instance.StartLevel(level);
	}

	public void EndLevel(int choice, bool completed = false) {

	}
}
