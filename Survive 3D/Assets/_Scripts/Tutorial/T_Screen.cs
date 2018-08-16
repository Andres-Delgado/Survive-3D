using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_Screen : MonoBehaviour {

	#region Variables

	[Header("References")]
	[SerializeField] private GameObject[] tutorials = new GameObject[4];

	#endregion

	public void SelectLevel(int level) {
		this.gameObject.SetActive(true);
		tutorials[level].SetActive(true);
	}

	public void ExitToMenu() {
		for (int i = 0; i < 4; i++) {
			tutorials[i].SetActive(false);
		}
		T_UIManager.Instance.ExitToMenu();
	}

	public void StartLevel(int level) {
		T_GameManager.Instance.StartLevel(level);
	}
}
