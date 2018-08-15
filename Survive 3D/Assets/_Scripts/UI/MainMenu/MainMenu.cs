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

	public void EnterShop() {
		int potions = SpawnManager.Instance.GetPotionCount();
		upgradeShopScreen.SetPotionText(potions);

		selectionScreen.gameObject.SetActive(false);
		upgradeShopScreen.gameObject.SetActive(true);
	}

	public void ExitShop() {
		upgradeShopScreen.gameObject.SetActive(false);
		selectionScreen.gameObject.SetActive(true);
	}
		
	public void EnterOptions() {
		selectionScreen.gameObject.SetActive(false);
		upgradeShopScreen.gameObject.SetActive(false);
		optionsScreen.gameObject.SetActive(true);
	}

	public void ExitOptions() {
		optionsScreen.gameObject.SetActive(false);
		upgradeShopScreen.gameObject.SetActive(false);
		selectionScreen.gameObject.SetActive(true);
	}

	public void SetHighScore(int value) {
		selectionScreen.SetHighScore(value);
	}

	public void SetCreditText(int value) {
		selectionScreen.SetCreditText(value);
		upgradeShopScreen.SetCreditText(value);
	}

	public void SetPotionText(int value) {
		upgradeShopScreen.SetPotionText(value);
	}

}
