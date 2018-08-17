using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_Green : Enemy, IDamageable {

	#region Variables

	private bool canHitBlue = true;
	private bool canSpawnGem = false;

	#endregion

	public override void Init(float x, float z) {
		transform.localPosition = new Vector3(x, 4.5f, z);
		playerTrans = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		canHitBlue = true;
	}

	public void Init(float x, float z, bool _canSpawnGem) { 
		transform.localPosition = new Vector3(x, 4.5f, z);
		playerTrans = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		canSpawnGem = _canSpawnGem;
	}

	private void Start() {
		StartCoroutine(HitBlueToggle());
	}

	protected override void FixedUpdate() {
		if (T_GameManager.Instance.isPaused) { return; }

		if (canMove) { Move(); }
	}

	protected override void OnCollisionEnter(Collision other) {
		if (other.gameObject.CompareTag("ForeGround")) {
			canMove = true;
		}
		else if (other.gameObject.CompareTag("Player")) {
			Rigidbody rbPlayer = other.gameObject.GetComponent<Rigidbody>();
			Animations.Instance.Push(rbPlayer, rbEnemy, other.contacts[0].point);
			other.gameObject.GetComponent<T_Player>().Damage();
			Damage(true);

		}
		else if (canHitBlue && other.gameObject.CompareTag("Enemy")) {
			Rigidbody rbPlayer = other.gameObject.GetComponent<Rigidbody>();
			Animations.Instance.Push(rbPlayer, rbEnemy, other.contacts[0].point);
		}
	}

	IEnumerator HitBlueToggle() {
		yield return new WaitForSeconds(0.78f);
		canHitBlue = false;
	}

	public override void Damage(bool hitPlayer = false) {
		health--;
		SetScale();
		if (health <= 0) {
			if (!hitPlayer) {
				playerTrans.GetComponent<T_Player>().SetScore(pointValue);
			}
			T_SpawnManager.Instance.IncrementKills();
			DestroySelf(hitPlayer);
		}
	}

	protected override void DestroySelf(bool hitPlayer = false) {
		if (canSpawnGem && !hitPlayer) {
			int number = Random.Range(0, dropRate);
			if (number == 0) {
				SpawnGem();
			}
		}
		Destroy(this.gameObject);
	}


}
