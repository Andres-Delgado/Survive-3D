using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	#region Variables

	[Header("Variables")]
	[SerializeField] private float speed = 15.0f;

	//private Player player;

	private float xValue, zValue;
	//private int points = 0;

	#endregion

	public void Init(float xGiven, float zGiven) {
		xValue = xGiven;
		zValue = zGiven;
		enabled = true;
	}

	protected virtual void Update() {
		if ((Mathf.Abs(transform.position.x) >= 25.0f) || (Mathf.Abs(transform.position.z) >= 25.0f)) {
			DestroySelf();
		}
	}

	protected virtual void FixedUpdate() {
		if (GameManager.Instance.isPaused) { return; }
		Move();
	}

	public void Move() {
		Vector3 moveH = new Vector3(xValue, 0, 0);
		Vector3 moveV = new Vector3(0, 0, zValue);
		Vector3 velocity = (moveH + moveV) * speed * Time.fixedDeltaTime;
		transform.Translate(velocity);
	}

	protected virtual void OnTriggerEnter(Collider other) {
		if (other.CompareTag("EnemyModel")) {
			IDamageable enemy = other.GetComponentInParent<IDamageable>();
			enemy.Damage();
			DestroySelf();
		}
	}

	protected virtual void DestroySelf() {
		Destroy(this.gameObject);
	}
}
