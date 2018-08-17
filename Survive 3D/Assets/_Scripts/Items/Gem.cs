using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Gem : MonoBehaviour {

	#region Variables

	[Header("Variables")]
	[SerializeField] protected int gemValue = 0;

	#endregion

	private void Start() {
		try { StartCoroutine(Despawn()); }
		catch (Exception e) {
			Debug.Log("Exception Caught:\n");
			Debug.LogException(e, this.gameObject);
		}

	}

	protected virtual void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")) {
			Player player = other.GetComponent<Player>();
			player.SetCredits(gemValue);
			StopAllCoroutines();
			DestroySelf();
		}
	}

	IEnumerator Despawn() {
		yield return new WaitForSeconds(3.0f);
		if (!this.gameObject) {
			throw new Exception("GameObject is null,");
		}
		Destroy(this.gameObject);
	}

	protected void DestroySelf() {
		Destroy(this.gameObject);
	}

}
