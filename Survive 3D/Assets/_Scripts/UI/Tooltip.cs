using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Tooltip : MonoBehaviour {

	#region Variables

	[Header("References")]
	[SerializeField] private GameObject tooltipObject;
	[SerializeField] private TextMeshProUGUI tooltipText;

	[Header("Variables")]
	[SerializeField] private int shopItem;

	#endregion

	public void TooltipOn() {
		//Debug.Log("Here");
		switch (shopItem) {
			case 0:
				tooltipText.text = "Passive: Increases player's move speed by 1 for each upgrade.";
				break;
			case 1:
				tooltipText.text = "Passive: Increases player's fire rate by 20% for each upgrade.";
				break;
			case 2:
				tooltipText.text = "Consumable: Regains 1 lost life. Press E to use.";
				break;
			default:
				tooltipText.text = "";
				break;
		}
		tooltipObject.SetActive(true);
		//tooltipObject.transform.position = new Vector3(Input.mousePosition.x + 300, Input.mousePosition.y + 90, Input.mousePosition.z - 1000);
	}

	public void TooltipOff() {
		tooltipObject.SetActive(false);
		tooltipText.text = "";
	}
}
	

