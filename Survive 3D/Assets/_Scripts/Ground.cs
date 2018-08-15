using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour {

	private void OnCollisionEnter(Collision other) {
		if (other.gameObject.CompareTag("Player")) {
			other.gameObject.GetComponent<Player>().Death(0);
		}
		else if (other.gameObject.CompareTag("Enemy")) {
			other.gameObject.GetComponent<Enemy>().Death();
		}
	}

}
