using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeShop : MonoBehaviour {

	#region Variables

	[Header("References")]
	[SerializeField] private GameObject[] shopItemObjects = new GameObject[3];
	[SerializeField] private TextMeshProUGUI creditsText;

	[Header("Variables")]
	[SerializeField] private int speedPrice = 100;
	[SerializeField] private int firePrice = 150;
	[SerializeField] private int potionPrice = 50;
	
	[Space]
	[SerializeField] private int speedUpgrades = 0;
	[SerializeField] private int fireUpgrades = 0;
	[SerializeField] private int potions = 0;

	#endregion

	public void PurchaseItem(int choice) {
		int currentCredits = SpawnManager.Instance.GetCredits();
		switch (choice) {
			case 0:
				if (currentCredits >= speedPrice) {
					SetItemValues(choice, speedPrice);
					//Debug.Log("Puchased Speed Boost!");
				}
				//else { Debug.Log("You do not have enough credits for this..."); }
				break;
			case 1:
				if (currentCredits >= firePrice) {
					SetItemValues(choice, firePrice);
					//Debug.Log("Puchased Fire Rate Upgrade!");
				}
				//else { Debug.Log("You do not have enough credits for this..."); }
				break;
			case 2:
				if (currentCredits >= potionPrice) {
					SetItemValues(choice, potionPrice);
					//Debug.Log("Puchased a Potion!");
				}
				//else { Debug.Log("You do not have enough credits for this..."); }
				break;
			default:
				break;
		}
	}

	private void SetItemValues(int choice, int newPrice) {
		TextMeshProUGUI currentUpgradeText = shopItemObjects[choice].transform.GetChild(1).GetComponent<TextMeshProUGUI>();
		TextMeshProUGUI currentCostText = shopItemObjects[choice].transform.GetChild(2).GetComponent<TextMeshProUGUI>();
		SpawnManager.Instance.SetItem(choice, -newPrice);

		switch (choice) {
			case 0:
				currentUpgradeText.text = "Current Speed: +" + ++speedUpgrades;
				speedPrice *= 2;
				currentCostText.text = speedPrice + " Credits";
				break;
			case 1:
				currentUpgradeText.text = "Current Fire Rate: +" + ++fireUpgrades;
				firePrice *= 2;
				currentCostText.text = firePrice + " Credits";
				break;
			case 2:
				currentUpgradeText.text = "Current Potions: " + ++potions;
				break;
			default:
				break;
		}
		
		
	}

	public void SetCreditText(int value) {
		creditsText.text = "Credits: " + value;
	}

}
