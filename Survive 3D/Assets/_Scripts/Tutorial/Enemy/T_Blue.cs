using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_Blue : Enemy, IDamageable {

	#region Variables

	private bool canHitPlayer = false;
	private bool allowedMove = false;

	#endregion

	public override void Init(float x, float z) {
		transform.localPosition = new Vector3(x, 3.0f, z);
		playerTrans = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}

	public void Init(float x, float z, bool move, bool canHit) {
		transform.localPosition = new Vector3(x, 3.0f, z);
		playerTrans = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		allowedMove = move;
		canHitPlayer = canHit;
	}

	protected override void FixedUpdate() {
		if (T_GameManager.Instance.isPaused) { return; }

		if (canMove) { Move(); }
	}

	protected override void OnCollisionEnter(Collision other) {
		if (other.gameObject.CompareTag("ForeGround") && allowedMove) {
			canMove = true;
		}

		if (other.gameObject.CompareTag("Player")) {
			if (canHitPlayer) {
				//other.gameObject.GetComponent<T_Player>().Damage();
			}
			Damage(true);
		}
	}

	public override int getPoints() {
		return pointValue;
	}

	public override void Damage(bool hitPlayer = false) {
		T_SpawnManager.Instance.IncrementKills();

		if (!hitPlayer) {
			playerTrans.GetComponent<T_Player>().SetScore(pointValue);
		}
		DestroySelf(hitPlayer);

	}

	protected override void DestroySelf(bool hitPlayer = false) {
		if (!hitPlayer) {
			//////////////////// Spawn Gem
		}
		Destroy(this.gameObject);
	}

	protected override void SpawnGem() {
		Vector3 gemPosition = new Vector3(this.transform.position.x, 3.0f, this.transform.position.z);
		Instantiate<GameObject>(gemPrefab, gemPosition, Quaternion.identity);
	}

	public void SetMove(bool _canMove) {
		canMove = _canMove;
	}

	public override void Death() {
		DestroySelf(true);
	}

}
