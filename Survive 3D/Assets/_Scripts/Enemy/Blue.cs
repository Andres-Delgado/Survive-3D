using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blue : Enemy, IDamageable {

	#region Variables



	#endregion

	public override void Init(float x, float z) {
		transform.localPosition = new Vector3(x, 3.0f, z);
		playerTrans = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}

	public override int getPoints() {
		return pointValue;
	}

	public override void Damage(bool hitPlayer = false) {
		base.Damage(hitPlayer);
	}

}
