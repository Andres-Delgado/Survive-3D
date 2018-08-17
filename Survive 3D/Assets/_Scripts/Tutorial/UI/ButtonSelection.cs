using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonSelection : MonoBehaviour, ISelectHandler, IDeselectHandler {

	#region Variables

	//[Header("References")]

	//[Header("Variables")]

	private Image buttonImage;
	private Color redColor;
	private Color greenColor;

	#endregion

	private void Awake() {
		ColorUtility.TryParseHtmlString("#FF0000", out redColor);
		ColorUtility.TryParseHtmlString("#FFC000", out greenColor);
		buttonImage = this.gameObject.GetComponent<Image>();
	}

	public void OnSelect(BaseEventData data) {
		buttonImage.color = greenColor;
	}

	public void OnDeselect(BaseEventData data) {
		buttonImage.color = redColor;
	}

	public void DeSelect() {
		buttonImage.color = redColor;
		EventSystem.current.SetSelectedGameObject(null);
	}

}
