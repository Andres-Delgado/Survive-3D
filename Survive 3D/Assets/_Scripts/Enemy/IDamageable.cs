using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable {

	void Init(float x, float z);
	void Damage(bool hitPlayer = false);
	int getPoints();

}
