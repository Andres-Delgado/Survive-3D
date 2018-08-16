using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

	#region Variables

	private static SpawnManager _instance;
	public static SpawnManager Instance {
		get {
			if (!_instance) {
				Debug.Log("Error: Spawn Manager is null");
			}
			return _instance;
		}
	}

	[Header("Object References")]
	[SerializeField] private GameObject playerPrefab;
	[SerializeField] private GameObject blueEnemyPrefab;
	[SerializeField] private GameObject greenEnemyPrefab;

	private Player player;

	[Header("Varaibles")]
	[SerializeField] private int highScore = 0;
	[SerializeField] private int credits = 0;
	[SerializeField] private float blueSpawnTime = 1.5f;
	[SerializeField] private float greenSpawnTime = 4.0f;
	[SerializeField] private int[] upgrades = new int[3];
	[SerializeField] private bool dashAbility = false;
	[SerializeField] private bool bulletWaveAbility = false;

	private float blueElapsedTime = 0.0f;
	private float greenElapsedTime = 0.0f;
	private bool isBlueSpawn = true;
	private bool isGreenSpawn = false;
	private bool increaseSpawnBlue = true;
	private bool increaseSpawnGreen = true;

	#endregion

	private void Awake() {
		_instance = this;
	}

	public void StartGame() {
		CreatePlayer();
		blueElapsedTime = greenElapsedTime = Time.time;
		blueSpawnTime = 1.5f;	// STOP BETWEEN (0.6, 0.7)
		greenSpawnTime = 4.0f;	// STOP BETWEEN (1.5, 2.0)
		isBlueSpawn = increaseSpawnBlue = true;
		isGreenSpawn = false;
		increaseSpawnGreen = true;
		if (GameManager.Instance.keyboardInput) {
			Cursor.visible = true;
		} else { Cursor.visible = false; }
		if (dashAbility) {
			UIManager.Instance.TurnOnAbility(0);
			UIManager.Instance.SetAbility(0, true);
		}
		if (bulletWaveAbility) {
			UIManager.Instance.TurnOnAbility(1);
			UIManager.Instance.SetAbility(1, true);
		}
		enabled = true;
	}

	private void Update() {
		if (GameManager.Instance.isPaused || (!GameManager.Instance.running)) { return; }
		CheckSpawns();
	}

	private void CheckSpawns() {
		if (isBlueSpawn && increaseSpawnBlue && ((Time.time - blueElapsedTime) > 10.0f)) {
			blueSpawnTime *= 0.9f;
			blueElapsedTime = Time.time;
		}
		if (isGreenSpawn && increaseSpawnGreen && ((Time.time - greenElapsedTime) > 10.0f)) {
			greenSpawnTime *= 0.9f;
			greenElapsedTime = Time.time;
		}

		if (isBlueSpawn) {
			isBlueSpawn = false;
			StartCoroutine(SpawnBlue());
		}
		if (isGreenSpawn) {
			isGreenSpawn = false;
			StartCoroutine(SpawnGreen());
		}
	}

	IEnumerator SpawnBlue() {
		float xValue = Random.Range(-9.5f, 9.5f);
		float zValue = Random.Range(-9.5f, 9.5f);
		StartCoroutine(PlaceMarker(0, xValue, zValue));

		if (blueSpawnTime <= 0.6f) {
			if ((Time.time - blueElapsedTime) < 15.0f) {
				blueSpawnTime = 0.6f;
				increaseSpawnBlue = false;
			} else {
				blueSpawnTime = 1.0f;
				isGreenSpawn = true;
				greenElapsedTime = Time.time;
			}
		}
		yield return new WaitForSeconds(blueSpawnTime);
		GameObject enemy = Instantiate<GameObject>(blueEnemyPrefab);
		enemy.transform.parent = this.gameObject.transform;
		enemy.GetComponent<IDamageable>().Init(xValue, zValue);

		isBlueSpawn = true;
	}

	IEnumerator SpawnGreen() {
		float xValue = Random.Range(-9.0f, 9.0f);
		float zValue = Random.Range(-9.0f, 9.0f);
		 if (greenSpawnTime < 1.75f) {
			increaseSpawnGreen = false;
			greenSpawnTime = 1.75f;
		}
		StartCoroutine(PlaceMarker(1, xValue, zValue));
		yield return new WaitForSeconds(greenSpawnTime);
		GameObject enemy = Instantiate<GameObject>(greenEnemyPrefab);
		enemy.transform.parent = this.gameObject.transform;
		enemy.GetComponent<IDamageable>().Init(xValue, zValue);

		isGreenSpawn = true;
	}

	IEnumerator PlaceMarker(int enemyChoice, float x, float z) {
		GameObject marker;
		switch (enemyChoice) {
			case 0:
				marker = Instantiate<GameObject>(blueEnemyPrefab.GetComponent<Enemy>().markerPrefab);
				marker.transform.parent = this.gameObject.transform;
				marker.transform.localPosition = new Vector3(x, marker.transform.localPosition.y, z);
				yield return new WaitForSeconds(blueSpawnTime + 0.75f);
				Destroy(marker.gameObject);
				break;
			case 1:
				marker = Instantiate<GameObject>(greenEnemyPrefab.GetComponent<Enemy>().markerPrefab);
				marker.transform.parent = this.gameObject.transform;
				marker.transform.localPosition = new Vector3(x, marker.transform.localPosition.y, z);
				yield return new WaitForSeconds(greenSpawnTime + 0.75f);
				Destroy(marker.gameObject);
				break;
			default:
				break;
		}
	}

	public void EndGame(int choice = 0) {
		StopAllCoroutines();
		KillChildren();
		DestroyGems();
		KillPlayer(choice);
		enabled = false;
	}

	private void KillChildren() {
		int childCount = transform.childCount;
		Transform[] children = new Transform[childCount];

		for (int i = 0; i < childCount; i++) {
			children[i] = transform.GetChild(i).GetComponent<Transform>();
		}

		for (int i = 0; i < childCount; i++) {
			Destroy(children[i].gameObject);
		}
	}

	private void DestroyGems() {
		GameObject[] allGems = GameObject.FindGameObjectsWithTag("Gem");
		int length = allGems.Length;
		for (int i = 0; i < length; i++) {
			Destroy(allGems[i].gameObject);
		}
	}
	
	private void CreatePlayer() {
		GameObject _player = Instantiate<GameObject>(playerPrefab);
		player = _player.GetComponent<Player>();
		UIManager.Instance.SetPotionText(upgrades[2]);
		player.Init(highScore, credits, upgrades[0], upgrades[1], upgrades[2], dashAbility, bulletWaveAbility);	
	}

	public void KillPlayer(int choice) {
		if (choice != 9) {
			highScore = player.GetHighScore();
			if ((highScore >= 1000) && !dashAbility) { dashAbility = true; }
			else if ((highScore >= 1300) && !bulletWaveAbility) { bulletWaveAbility = true; }

			credits = player.GetCredits();
			upgrades[2] = player.getPotions();
		}
		UIManager.Instance.SetCreditText(credits);
		UIManager.Instance.SetScoreText(0, highScore);
		SetPotionText(upgrades[2]);
		player.Die();
	}

	public int GetCredits() {
		return credits;
	}

	public int GetPotionCount() {
		return upgrades[2];
	}

	public void SetItem(int choice, int creditValue) {
		credits += creditValue;
		upgrades[choice]++;
		UIManager.Instance.SetCreditText(credits);
		UIManager.Instance.SetPotionText(upgrades[2]);
	}

	public void SetPotionText(int value) {
		UIManager.Instance.SetPotionText(value);
	}
}
