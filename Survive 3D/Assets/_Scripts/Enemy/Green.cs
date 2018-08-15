using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Green : Enemy, IDamageable {

	#region Variables

	private bool canHitBlue = true;

	#endregion

	public override void Init(float x, float z) {
		transform.localPosition = new Vector3(x, 4.5f, z);
		playerTrans = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		canHitBlue = true;
	}

	private void Start() {
		StartCoroutine(HitBlueToggle());
	}

	protected override void OnCollisionEnter(Collision other) {
		if (other.gameObject.CompareTag("ForeGround")) {
			canMove = true;
		}
		else if (other.gameObject.CompareTag("Player")) {
			Rigidbody rbPlayer = other.gameObject.GetComponent<Rigidbody>();
			Animations.Instance.Push(rbPlayer, rbEnemy, other.contacts[0].point);
			other.gameObject.GetComponent<Player>().Damage();
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

	public override int getPoints() {
		return pointValue;
	}

	public override void Damage(bool hitPlayer = false) {
		base.Damage(hitPlayer);
	}

}
