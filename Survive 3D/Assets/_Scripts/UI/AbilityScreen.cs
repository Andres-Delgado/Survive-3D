using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityScreen : MonoBehaviour {

	#region Variables

	[Header("References")]
	[SerializeField] private GameObject dashScreen;
	[SerializeField] private GameObject bulletScreen;


	#endregion

	public void DashScreenOn() {
		bulletScreen.SetActive(false);
		dashScreen.SetActive(true);
	}

	public void bulletScreenOn() {
		dashScreen.SetActive(false);
		bulletScreen.SetActive(true);
	}

}
