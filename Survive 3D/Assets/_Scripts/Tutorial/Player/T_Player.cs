using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_Player : MonoBehaviour {

	#region Variables

	[Header("Object References")]
	[SerializeField] private Transform graphicTrans;
	[SerializeField] private GameObject bulletPrefab;

	private Rigidbody rbPlayer;
	private Plane ground;

	[Header("Variables")]
	[SerializeField] private int health = 4;
	[SerializeField] private float speed = 7.0f;
	[SerializeField] private float fireRate = 0.5f;
	[SerializeField] private int potions = 0;
	[SerializeField] private bool canDash = false;
	[SerializeField] private int score = 0;


	private bool canShoot = true;
	private bool inDashCooldown = false;
	private float dashForce = 25.0f;
	private float dashCooldown = 0.0f;
	//private int credits = 0;

	private bool allowedMove = true;

	#endregion

	private void Awake() {
		rbPlayer = this.GetComponent<Rigidbody>();
	}

	public void Init(int creditValue, int speedUpgradeValue, int fireRateUpgradeValue, int potionQuantity, bool dashAbility, bool canMove = true, bool _canShoot = true) {
		//credits = creditValue;
		speed += speedUpgradeValue;
		//fireRate *= Mathf.Pow(0.8f, fireRateUpgradeValue);
		potions = potionQuantity;
		canDash = dashAbility;
		allowedMove = canMove;
		canShoot = _canShoot;
		//UIManager.Instance.SetScoreText(score, highScore);
		//UIManager.Instance.SetCreditText(credits);
		//UIManager.Instance.SetHealth(health);
	}

	private void Update() {
		if (T_GameManager.Instance.isPaused) { return; }

		if (T_GameManager.Instance.keyboardInput) {
			if (canShoot && Input.GetMouseButtonDown(0)) { Shoot(); }
			if ((potions > 0) && Input.GetKeyDown(KeyCode.E) && (health < 4)) { UsePotion(); }
		}
		else {
			if (canShoot && (Mathf.Abs(Input.GetAxis("FireTrigger")) >= 0.75f)) { Shoot(); }
			if ((potions > 0) && Input.GetButtonDown("ControllerY") && (health < 4)) { UsePotion(); }
		}

		CheckDash();
	}

	private void FixedUpdate() {
		if (T_GameManager.Instance.isPaused) { return; }
		if (allowedMove) { Move(); }
		else { if (T_GameManager.Instance.keyboardInput) {
				if ((Mathf.Abs(Input.GetAxisRaw("Mouse X")) > 0) || (Mathf.Abs(Input.GetAxisRaw("Mouse Y")) > 0)) {
					RotateToMousePosition();
				}
			}
		}
	}

	private void Move() {
		float xInput = Input.GetAxisRaw("Horizontal");
		float zInput = Input.GetAxisRaw("Vertical");
		Vector3 velocity = new Vector3(xInput, 0, zInput).normalized * speed;
		velocity.y = -3.5f;
		rbPlayer.MovePosition(rbPlayer.position + velocity * Time.fixedDeltaTime);

		if (T_GameManager.Instance.keyboardInput) {
			if ((Mathf.Abs(Input.GetAxisRaw("Mouse X")) > 0) || (Mathf.Abs(Input.GetAxisRaw("Mouse Y")) > 0) ||
				(Mathf.Abs(xInput) > 0) || (Mathf.Abs(zInput) > 0)) {
				RotateToMousePosition();
			}
		}
		else if ((Mathf.Abs(Input.GetAxisRaw("ControllerXAxis")) > 0.0f) || (Mathf.Abs(Input.GetAxisRaw("ControllerYAxis")) > 0.0f)) {
			RotateController();
		}
	}

	private void RotateToMousePosition() {
		Plane ground = new Plane(Vector3.up, transform.position);
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		float dist = 0.0f;

		if (ground.Raycast(ray, out dist)) {
			Vector3 mousePoint = new Vector3(ray.GetPoint(dist).x,
				transform.position.y, ray.GetPoint(dist).z);

			//*useful for showing where ray comes from in testing ONLY
			//Debug.DrawLine(ray.origin, mousePoint);

			Quaternion targetRotation = Quaternion.LookRotation(mousePoint - transform.position);
			graphicTrans.rotation = Quaternion.Slerp(graphicTrans.rotation, targetRotation, 1.0f/*speed * Time.deltaTime*/);
		}
	}

	private void RotateController() {
		float joystickX = Input.GetAxis("ControllerXAxis");
		float joystickY = Input.GetAxis("ControllerYAxis");
		float angle = Mathf.Atan2(joystickX, joystickY) * Mathf.Rad2Deg;
		graphicTrans.rotation = Quaternion.Euler(0, angle, 0);
	}


	private void CheckDash() {
		if (inDashCooldown) {
			if ((Time.time - dashCooldown) >= 10.0f) {
				inDashCooldown = false;
				////UIManager.Instance.SetAbility(0, true);
			}
		}

		if (canDash && !inDashCooldown) {
			if (T_GameManager.Instance.keyboardInput && Input.GetMouseButtonDown(1)) {
				StartCoroutine(Dash());
			}
			else if (!T_GameManager.Instance.keyboardInput && Input.GetButtonDown("ControllerLB")) {
				StartCoroutine(Dash());
			}
		}
	}

	IEnumerator Dash() {
		/////UIManager.Instance.SetAbility(0, false);
		dashCooldown = Time.time;
		inDashCooldown = true;
		Vector3 direction = graphicTrans.transform.forward.normalized;
		direction.y = 0;
		rbPlayer.velocity = direction * dashForce;
		yield return new WaitForSeconds(0.15f);
		rbPlayer.velocity = new Vector3(0, 0, 0);
	}

	private void Shoot() {
		float angle = graphicTrans.eulerAngles.y;
		if (angle > 180.0f) { angle -= 360.0f; }

		float xValue = Mathf.Sin(angle * Mathf.Deg2Rad);
		float zValue = Mathf.Cos(angle * Mathf.Deg2Rad);

		Vector3 bulletPosition = new Vector3(rbPlayer.position.x + xValue, rbPlayer.position.y, rbPlayer.position.z + zValue);

		GameObject bullet = Instantiate<GameObject>(bulletPrefab, bulletPosition, Quaternion.identity);
		bullet.GetComponent<Bullet>().Init(xValue, zValue);

		canShoot = false;
		StartCoroutine(ShootTimer());
	}

	IEnumerator ShootTimer() {
		yield return new WaitForSeconds(fireRate);
		canShoot = true;
	}

	private void UsePotion() {
		health++;
		potions--;
		//SpawnManager.Instance.SetPotionText(potions);
		//UIManager.Instance.SetHealth(health);
	}

	public void SetScore(int scoreValue) {
		score += scoreValue;
		T_UIManager.Instance.SetScoreText(score);
	}

	public void Damage() {
		Animations.Instance.ShakeCamera();
		health--;
		T_UIManager.Instance.SetHealth(health);
		if (health <= 0) { Death(0); }
	}

	public void Death(int ground) {
		/*if ((ground == 0) && (score >= 1000) && !canDash) {
			GameManager.Instance.EndGame(1);
		}
		else if ((ground == 0) && (score >= 1300) && !canBulletWave) {
			GameManager.Instance.EndGame(2);
		}
		else { GameManager.Instance.EndGame(ground); }*/
		if (ground == 0) {

		}
		T_GameManager.Instance.EndLevel(ground);
	}

	public void SetCredits(int value) {
		//credits += value;
		//UIManager.Instance.SetCreditText(credits);
	}

	public void StartMove() {
		allowedMove = true;
		canShoot = true;
}

	public void Die() {
		Destroy(this.gameObject);
	}
}
