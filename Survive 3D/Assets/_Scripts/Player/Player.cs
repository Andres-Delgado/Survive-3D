using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour {

	#region Variables

	[Header("Object References")]
	[SerializeField] private Transform graphicTrans;
	[SerializeField] private Animations anim;
	[SerializeField] private GameObject bulletPrefab;

	private Rigidbody rbPlayer;
	private Plane ground;

	[Header("Variables")]
	[SerializeField] private int health = 4;
	[SerializeField] private int highScore = 0;
	[SerializeField] private float speed = 7.0f;
	[SerializeField] private float fireRate = 0.5f;
	[SerializeField] private int potions = 0;
	[SerializeField] private bool canDash = false;
	[SerializeField] private bool canBulletWave = false;
	[SerializeField] private int score = 0;

	private bool inDashCooldown = false;
	private bool inBulletWaveCooldown = false;
	private int credits = 0;
	private float dashForce = 25.0f;
	private float dashCooldown = 0.0f;
	private float bulletWaveCooldown = 0.0f;

	private bool canShoot = true;

	#endregion

	private void Awake() {
		rbPlayer = this.GetComponent<Rigidbody>();
	}

	///
	/// <summary>
	/// Initializes player with highscore, credits, and upgrades.
	/// </summary>
	/// <param name="highValue">Player's highscore</param>
	/// <param name="creditValue">Player's credits</param>
	/// <param name="speedUpgradeValue">Player's current speed upgrades</param>
	/// <param name="fireRateUpgradeValue">Player's current firerate upgrades</param>
	/// <param name="potionQuantity">Player's potion quantity</param>
	/// <param name="dashAbility">Does player have Dash?</param>
	/// <param name="bulletAbility">Does player have Bullet Wave?</param>
	public void Init(int highValue, int creditValue, int speedUpgradeValue, int fireRateUpgradeValue, int potionQuantity, bool dashAbility, bool bulletAbility) {
		highScore = highValue;
		credits = creditValue;
		speed += speedUpgradeValue;
		fireRate *= Mathf.Pow(0.8f, fireRateUpgradeValue);
		potions = potionQuantity;
		canDash = dashAbility;
		canBulletWave = bulletAbility;
		UIManager.Instance.SetScoreText(score, highScore);
		UIManager.Instance.SetCreditText(credits);
		UIManager.Instance.SetHealth(health);
	}

	private void Update() {
		if (GameManager.Instance.isPaused) { return; }

		if (GameManager.Instance.keyboardInput && canShoot) {
			if (Input.GetMouseButtonDown(0)) { Shoot(); }
		}
		else if (!GameManager.Instance.keyboardInput && canShoot) {
			if (Mathf.Abs(Input.GetAxis("FireTrigger")) >= 0.75f) { Shoot(); }
		}

		if (GameManager.Instance.keyboardInput && (potions > 0) && Input.GetKeyDown(KeyCode.E) && (health < 4)) {
			UsePotion();
		}
		else if (!GameManager.Instance.keyboardInput && (potions > 0) && Input.GetButtonDown("ControllerY") && (health < 4)) {
			UsePotion();
		}
		CheckDash();

		CheckBulletWave();
		

	}

	private void FixedUpdate() {
		if (GameManager.Instance.isPaused) { return; }
		Move();
	}

	private void Move() {
		float xInput = Input.GetAxisRaw("Horizontal");
		float zInput = Input.GetAxisRaw("Vertical");
		Vector3 velocity = new Vector3(xInput, 0, zInput).normalized * speed;
		velocity.y = -3.5f;		
		rbPlayer.MovePosition(rbPlayer.position + velocity * Time.fixedDeltaTime);

		if (GameManager.Instance.keyboardInput) {
			if ((Mathf.Abs(Input.GetAxisRaw("Mouse X")) > 0) || (Mathf.Abs(Input.GetAxisRaw("Mouse Y")) > 0) ||
				(Mathf.Abs(xInput) > 0) || (Mathf.Abs(zInput) > 0)) {
				RotateToMousePosition();
			}
		}
		else if ((Mathf.Abs(Input.GetAxisRaw("ControllerXAxis")) > 0.0f) || (Mathf.Abs(Input.GetAxisRaw("ControllerYAxis")) > 0.0f)) {
			RotateController();
		}
	}

	private void CheckDash() {
		if (inDashCooldown) {
			if ((Time.time - dashCooldown) >= 10.0f) {
				inDashCooldown = false;
				UIManager.Instance.SetDash(true);
			}
		}
		if (GameManager.Instance.keyboardInput && canDash && !inDashCooldown && Input.GetMouseButtonDown(1)) {
			StartCoroutine(Dash());
		}
		else if (!GameManager.Instance.keyboardInput && canDash && !inDashCooldown && Input.GetButtonDown("ControllerLB")) {
			StartCoroutine(Dash());
		}
	}

	private void CheckBulletWave() {
		if (inBulletWaveCooldown) {
			if ((Time.time - bulletWaveCooldown) >= 10.0f) {
				inBulletWaveCooldown = false;
				UIManager.Instance.SetBulletWave(true);
			}
		}

		if (GameManager.Instance.keyboardInput && canBulletWave && !inBulletWaveCooldown && Input.GetKeyDown(KeyCode.Space)) {
			BulletWave();
		}
		else if (!GameManager.Instance.keyboardInput && canBulletWave && !inBulletWaveCooldown && Input.GetButtonDown("ControllerB")) {
			BulletWave();
		}
	}

	IEnumerator Dash() {
		UIManager.Instance.SetDash(false);
		dashCooldown = Time.time;
		inDashCooldown = true;
		Vector3 direction = graphicTrans.transform.forward.normalized;
		direction.y = 0;
		rbPlayer.velocity = direction * dashForce;
		yield return new WaitForSeconds(0.15f);
		rbPlayer.velocity = new Vector3(0, 0, 0);
	}

	private void BulletWave() {
		UIManager.Instance.SetBulletWave(false);
		bulletWaveCooldown = Time.time;
		inBulletWaveCooldown = true;

		float angle = graphicTrans.eulerAngles.y;
		if (angle > 180.0f) { angle -= 360.0f; }

		Vector3[] positions = new Vector3[16];
		for (int i = 0; i < 16; i++ ) {
			angle += (i * 22.5f);
			if (angle > 180.0f) { angle -= 360.0f; }
			float xValue = Mathf.Sin(angle * Mathf.Deg2Rad);
			float zValue = Mathf.Cos(angle * Mathf.Deg2Rad);
			positions[i] = new Vector3(rbPlayer.position.x, rbPlayer.position.y, rbPlayer.position.z);

			GameObject bullet = Instantiate<GameObject>(bulletPrefab, positions[i], Quaternion.identity);
			bullet.GetComponent<Bullet>().Init(xValue, zValue);
		}
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

	

	private void RotateController() {
		float joystickX = Input.GetAxis("ControllerXAxis");
		float joystickY = Input.GetAxis("ControllerYAxis");
		//if (Mathf.Abs(joystickX) <= sensitivity) { joystickX = 0.0f; }
		//if (Mathf.Abs(joystickY) <= sensitivity) { joystickY = 0.0f; }
		//Debug.Log("X: " + joystickX + "\nY: " + joystickY);

		float angle = Mathf.Atan2(joystickX, joystickY) * Mathf.Rad2Deg;
		graphicTrans.rotation = Quaternion.Euler(0, angle, 0);		
	}

	private void RotateToMousePosition() {
		Plane ground = new Plane(Vector3.up, transform.position);
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		float dist = 0f;

		if (ground.Raycast(ray, out dist)) {
			//create a vector position at latest raycast hit point
			Vector3 mousePoint = new Vector3(ray.GetPoint(dist).x,
				transform.position.y, ray.GetPoint(dist).z);

			//*useful for showing where ray comes from in testing ONLY
			//Debug.DrawLine(ray.origin, mousePoint);

			//rotate object toward vector position
			Quaternion targetRotation = Quaternion.LookRotation(mousePoint - transform.position);
			graphicTrans.rotation = Quaternion.Slerp(graphicTrans.rotation, targetRotation, 1.0f/*speed * Time.deltaTime*/);

			//transform.rotation = new Quaternion(90, transform.rotation.z, transform.rotation.z, transform.rotation.w);
			//transform.position = Vector3.Slerp(transform.position, mousePoint, speed * Time.deltaTime);
		}
	}

	IEnumerator ShootTimer() {
		yield return new WaitForSeconds(fireRate);
		canShoot = true;
	}

	public int GetCredits() { return credits; }
	public int GetScore() { return score; }
	public int GetHighScore() { return highScore; }

	public void SetCredits(int value) {
		credits += value;
		UIManager.Instance.SetCreditText(credits);
	}

	public void SetScore(int scoreValue) {
		score += scoreValue;
		if (score > highScore) {
			highScore = score;
		}
		UIManager.Instance.SetScoreText(score, highScore);
	}

	private void UsePotion() {
		health++;
		potions--;
		SpawnManager.Instance.SetPotionText(potions);
		UIManager.Instance.SetHealth(health);
	}

	public int getPotions() {
		return potions;
	}

	public int GetHealth() {
		return health;
	}

	public void Damage() {
		Animations.Instance.ShakeCamera();
		health--;
		UIManager.Instance.SetHealth(health);

		if (health <= 0) { Death(0); }
	}

	public void Death(int ground) {
		if ((ground == 0) && (score >= 1000) && !canDash) {
			GameManager.Instance.EndGame(1);
		}
		else if ((ground == 0) && (score >= 1300) && !canBulletWave) {
			GameManager.Instance.EndGame(2);
		}
		else { GameManager.Instance.EndGame(ground); }
	}

	public void Die() {
		Destroy(this.gameObject);
	}

	#region Legacy rotate methods
	/*private void Rotate() {
		Vector3 playerPosition = Camera.main.WorldToScreenPoint(transform.position);
		Vector3 mousePosition = Input.mousePosition;

		mousePosition.x -= playerPosition.x;
		mousePosition.y -= playerPosition.y;


		float angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
		graphicTrans.rotation = Quaternion.Euler(new Vector3(0, (angle - 90.0f) * -1.0f, 0));
	}

	private void Rotate2() {
		//Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f);
		//Vector3 mousePosition = Camera.main.WorldToScreenPoint(Input.mousePosition);
		//mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		float angle = 0;
		Vector3 playerPosition = Camera.main.WorldToScreenPoint(transform.position);
		Vector3 mousePosition = Input.mousePosition;

		mousePosition.x -= playerPosition.x;
		mousePosition.y -= playerPosition.y;

		float distance = mousePosition.x * mousePosition.x;
		distance += mousePosition.y * mousePosition.y;
		distance = Mathf.Sqrt(distance);

		Debug.Log("X: " + mousePosition.x);
		Debug.Log("Y: " + mousePosition.y);
		Debug.Log("Distance: " + distance);

		if (mousePosition.x >= 0) {
			angle = 90 - (Mathf.Asin(mousePosition.y / distance) * Mathf.Rad2Deg);
			Debug.Log("ANGLE: " + angle);
		}
		else if (mousePosition.x < 0) {
			angle = -90 + (Mathf.Asin(mousePosition.y / distance) * Mathf.Rad2Deg);
			Debug.Log("ANGLE: " + angle);
		}
		graphicTrans.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
	}*/
	#endregion
}
