using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_Bullet : Bullet {

	protected override void FixedUpdate() {
		if (T_GameManager.Instance.isPaused) { return; }
		Move();
	}

}
