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




	#endregion

	private void Awake() {
		_instance = this;
	}

	public void StartLevel(int level) {

		T_SpawnManager.Instance.StartLevel(level);
	}
}
