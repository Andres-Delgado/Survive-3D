using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

	#region Variables

	[Header("Object References")]
	[SerializeField] private Selection selectionScreen;
	[SerializeField] private UpgradeShop upgradeShopScreen;
	[SerializeField] private Options optionsScreen;


	#endregion

	public void StartGameButton() {
		GameManager.Instance.StartGame();
	}
		
	public void ShopEnterExit(bool enter) {
		if (enter) {
			int potions = SpawnManager.Instance.GetPotionCount();
			upgradeShopScreen.SetPotionText(potions);
			selectionScreen.gameObject.SetActive(false);
			upgradeShopScreen.gameObject.SetActive(true);
		} else {
			upgradeShopScreen.gameObject.SetActive(false);
			selectionScreen.gameObject.SetActive(true);
		}
	}

	public void OptionsEnterExit(bool enter) {
		if (enter) {
			selectionScreen.gameObject.SetActive(false);
			upgradeShopScreen.gameObject.SetActive(false);
			optionsScreen.gameObject.SetActive(true);
		} else {
			optionsScreen.gameObject.SetActive(false);
			upgradeShopScreen.gameObject.SetActive(false);
			selectionScreen.gameObject.SetActive(true);
		}
	}



	public void SetPotionText(int value) {
		upgradeShopScreen.SetPotionText(value);
	}

	///
	/// <summary>
	/// Sets the Main Menu credits and highscore text.
	/// </summary>
	/// <param name="choice">Input 0 for updating credits or 1 for updating highscore.</param>
	/// <param name="credits"></param>
	/// <param name="highscore"></param>
	public void SetMenuText(int choice, int value) {
		switch (choice) {
			case 0:
				selectionScreen.SetCreditText(value);
				upgradeShopScreen.SetCreditText(value);
				break;
			case 1:
				selectionScreen.SetHighScore(value);
				break;
			default:
				break;
		}
	}

}
