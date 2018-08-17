using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {

	#region Variables

	[Header("References")]
	[SerializeField] public GameObject markerPrefab;
	[SerializeField] protected GameObject gemPrefab;

	protected Rigidbody rbEnemy;
	protected Transform playerTrans;

	[Header("Variables")]
	[SerializeField] protected int health;
	[SerializeField] protected float speed;
	[SerializeField] protected int pointValue;
	[SerializeField] protected int dropRate;

	protected float gravity = -2.25f;
	protected bool canMove = false;

	#endregion

	protected void Awake() {
		rbEnemy = transform.GetComponent<Rigidbody>();
	}

	public virtual void Init(float x, float z) {
		transform.localPosition = new Vector3(x, transform.localPosition.y, z);
		playerTrans = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}

	protected virtual void FixedUpdate() {
		if (GameManager.Instance.isPaused) { return; }
		
		if (canMove) { Move(); }
	}

	protected virtual void OnCollisionEnter(Collision other) {
		if (other.gameObject.CompareTag("ForeGround")) {
			canMove = true;
		}
		if (other.gameObject.CompareTag("Player")) {
			other.gameObject.GetComponent<Player>().Damage();
			Damage(true);
		}
	}

	protected virtual void Move() {
		Vector3 positionEnemy = rbEnemy.position;
		positionEnemy.x -= playerTrans.position.x;
		positionEnemy.y += gravity;
		positionEnemy.z -= playerTrans.position.z;
		positionEnemy.x *= -1.0f;
		positionEnemy.z *= -1.0f;

		Vector3 velocity = positionEnemy.normalized * speed;
		rbEnemy.MovePosition(rbEnemy.position + velocity * Time.fixedDeltaTime);
	}

	public virtual int getPoints() {
		return pointValue;
	}

	public virtual void Damage(bool hitPlayer = false) {
		health--;
		SetScale();
		if (health <= 0) {
			if (!hitPlayer) {
				playerTrans.GetComponent<Player>().SetScore(pointValue);

			}
			DestroySelf(hitPlayer);
		}
	}

	public virtual void Death() {
		DestroySelf(true);
	}

	protected virtual void DestroySelf(bool hitPlayer = false) {
		if (!hitPlayer) {
			int number = Random.Range(0, dropRate);
			if (number == 0) {
				SpawnGem();
			}
		}
		Destroy(this.gameObject);
	}

	protected virtual void SpawnGem() {
		Vector3 gemPosition = new Vector3(this.transform.position.x, 3.0f, this.transform.position.z);
		Instantiate<GameObject>(gemPrefab, gemPosition, Quaternion.identity);

	}

	protected virtual void SetScale() {
		switch (health) {
			case 3:
				transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
				break;
			case 2:
				transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
				break;
			case 1:
				transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
				break;
			default:
				break;
		}
	}
}
