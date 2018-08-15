using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour {

	#region Variables

	[Header("Variables")]
	[SerializeField] protected int gemValue = 5;

	#endregion

	protected void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")) {
			Player player = other.GetComponent<Player>();
			player.SetCredits(gemValue);
			DestroySelf();
		}
	}

	protected void DestroySelf() {
		Destroy(this.gameObject);
	}

}
