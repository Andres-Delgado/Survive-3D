using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_SpawnManager : MonoBehaviour {

	#region Variables

	private static T_SpawnManager _instance;
	public static T_SpawnManager Instance {
		get {
			if (!_instance) {
				Debug.Log("Error: T_SpawnManager is null");
			}
			return _instance;
		}
	}




	#endregion

	private void Awake() {
		_instance = this;
	}

	public void StartLevel(int level) {
		Debug.Log("Starting Tutorial: " + level);
	}
}
