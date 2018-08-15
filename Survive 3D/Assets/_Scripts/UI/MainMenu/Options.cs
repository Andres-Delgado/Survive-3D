using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Options : MonoBehaviour {

	#region Variables

	[Header("References")]
	[SerializeField] private GameObject keymapFrame;
	[SerializeField] private TextMeshProUGUI inputText;



	#endregion

	public void EnterKeymap() {
		keymapFrame.gameObject.SetActive(true);
		this.gameObject.SetActive(false);
	}

	public void ExitKeymap() {
		keymapFrame.gameObject.SetActive(false);
		this.gameObject.SetActive(true);
	}

	public void MouseKeysInput() {
		inputText.text = "Mouse/Keyboard Input";
		GameManager.Instance.keyboardInput = true;
	}

	public void XboxInput() {
		inputText.text = "Xbox One Controller Input";
		GameManager.Instance.keyboardInput = false;
	}

}
