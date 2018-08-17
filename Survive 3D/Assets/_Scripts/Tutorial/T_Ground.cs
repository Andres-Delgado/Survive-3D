using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_Ground : Ground {

	protected override void OnCollisionEnter(Collision other) {
		if (other.gameObject.CompareTag("Player")) {
			other.gameObject.GetComponent<T_Player>().Death(0);
		}
		else if (other.gameObject.CompareTag("Enemy")) {
			other.gameObject.GetComponent<Enemy>().Death();
		}
	}

}
