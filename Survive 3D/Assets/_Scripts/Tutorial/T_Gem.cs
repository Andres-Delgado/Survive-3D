using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_Gem : Gem {

	protected override void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")) {
			T_Player player = other.GetComponent<T_Player>();
			player.SetCredits(gemValue);
			StopAllCoroutines();
			DestroySelf();
		}
	}

}
