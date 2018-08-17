using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_UIManager : MonoBehaviour {

	#region Variables

	private static T_UIManager _instance;
	public static T_UIManager Instance {
		get {
			if (!_instance) {
				Debug.Log("Error: T_UIManager is null");
			}
			return _instance;
		}
	}

	private void Awake() {
		_instance = this;
	}

	[Header("References")]
	[SerializeField] private T_Selection tSelection;
	[SerializeField] private T_Screen tScreen;
	[SerializeField] private T_HUD tHUD;

	[Header("Variables")]
	[SerializeField] private int completed;

	//[Header("Variables")]

	#endregion

	public void SelectLevel(int level) {
		tSelection.gameObject.SetActive(false);
		tScreen.SelectLevel(level);
	}

	public void ExitToMenu() {
		tScreen.gameObject.SetActive(false);
		tSelection.gameObject.SetActive(true);
	}

	public void StartLevel() {
		tScreen.gameObject.SetActive(false);
		tSelection.gameObject.SetActive(false);
	}

	public void MenuOn() {
		tScreen.DeactivateAll();
		tScreen.gameObject.SetActive(false);
		tSelection.gameObject.SetActive(true);
	}
}

