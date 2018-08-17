using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_SpawnManager : MonoBehaviour {

	#region Variables

	private static T_SpawnManager _instance;
	public static T_SpawnManager Instance {
		get {
			if (!_instance) {
				Debug.Log("Error: T_SpawnManager is null");
			}
			return _instance;
		}
	}

	[Header("Object References")]
	[SerializeField] private GameObject playerPrefab;
	[SerializeField] private GameObject blueEnemyPrefab;
	[SerializeField] private GameObject greenEnemyPrefab;
	[SerializeField] private int killCount = 0;

	private T_Player player;
	private int currentTutorial;
	private float timeElapsed = 0.0f;

	private float blueSpawnTime = 2.0f;
	private float greenSpawnTime = 5.0f;

	//private float blueElapsedTime = 0.0f;
	//private float greenElapsedTime = 0.0f;
	private bool isBlueSpawn = true;
	private bool isGreenSpawn = true;
	//private bool increaseSpawnBlue = true;
	//private bool increaseSpawnGreen = true;

	private bool startSpawn = true;

	#endregion

	private void Awake() {
		_instance = this;
	}

	private void OnEnable() {
		isBlueSpawn = true;
		isGreenSpawn = true;
		startSpawn = true;
	}

	private void Update() {
		if (T_GameManager.Instance.isPaused || (!T_GameManager.Instance.running)) { return; }
		CheckIfFinished(currentTutorial);

		if (currentTutorial != 0) {
			if (startSpawn && (Time.time - timeElapsed >= 2.3f)) {
				startSpawn = false;
				player.StartMove();
				StartSpawning1();
			}
			else { CheckSpawns(); }
		}
	}

	public void StartLevel(int level) {
		switch (level) {
			case 0:
				enabled = true;
				currentTutorial = 0; ;
				CreatePlayer(0);
				SpawnLevel0();
				goto default;
			case 1:
				enabled = true;
				currentTutorial = 1;
				CreatePlayer(1);
				SpawnLevel1();
				goto default;
			case 2:

				break;
			case 3:

				break;
			default:
				killCount = 0;
				break;
		}
	}

	private void CheckIfFinished(int level) {
		switch (level) {
			case 0:
				if (killCount >= 8) {
					StopAllCoroutines();
					KillChildren();
					KillPlayer();
					enabled = false;
					T_GameManager.Instance.EndLevel(0, true);
				}
				break;
			case 1:
				if (killCount >= 20) {
					StopAllCoroutines();
					KillChildren();
					KillPlayer();
					enabled = false;
					T_GameManager.Instance.EndLevel(1, true);
				}

				break;
			case 2:

				break;
			case 3:

				break;
			default:
				break;
		}
	}

	public void EndLevel() {
		StopAllCoroutines();
		KillChildren();
		KillPlayer();
		enabled = false;
	}

	private void CreatePlayer(int level) {
		GameObject _player = Instantiate<GameObject>(playerPrefab);
		player = _player.GetComponent<T_Player>();
		//UIManager.Instance.SetPotionText(upgrades[2]);
		switch (level) {
			case 0:
				player.Init(0, 0, 0, 0, false);
				break;
			case 1:
				player.Init(0, 0, 0, 0, false, false, false);
				break;
			case 2:

				break;
			case 3:

				break;
			default:
				break;
		}
	}

	private void SpawnLevel0() {
		int xPosition = 7;
		int zPosition = 7;
		for (int i = 0; i < 4; i++) {
			zPosition *= -1;
			if (i == 2) { xPosition *= -1; }

			GameObject enemy = Instantiate<GameObject>(blueEnemyPrefab);
			enemy.transform.parent = this.gameObject.transform;
			enemy.GetComponent<T_Blue>().Init(xPosition, zPosition, false, false, false);
		}

		xPosition = 0;
		zPosition = 5;
		for (int i = 0; i < 4; i++) {
			xPosition *= -1;
			zPosition *= -1;
			if (i == 2) {
				xPosition = 5;
				zPosition = 0;
			}
			GameObject enemy = Instantiate<GameObject>(blueEnemyPrefab);
			enemy.transform.parent = this.gameObject.transform;
			enemy.GetComponent<T_Blue>().Init(xPosition, zPosition, false, false, false);
		}
	}

	private void SpawnLevel1() {
		GameObject enemy = Instantiate<GameObject>(greenEnemyPrefab);
		enemy.transform.parent = this.gameObject.transform;
		enemy.GetComponent<T_Green>().Init(7, 0, false);
		timeElapsed = Time.time;
	}

	private void StartSpawning1() {
		CheckSpawns();
	}

	private void CheckSpawns() {
		/*if (isBlueSpawn && increaseSpawnBlue && ((Time.time - blueElapsedTime) > 10.0f)) {
			blueSpawnTime *= 0.9f;
			blueElapsedTime = Time.time;
		}
		if (isGreenSpawn && increaseSpawnGreen && ((Time.time - greenElapsedTime) > 10.0f)) {
			greenSpawnTime *= 0.9f;
			greenElapsedTime = Time.time;
		}*/

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

		/*if (blueSpawnTime <= 0.6f) {
			if ((Time.time - blueElapsedTime) < 15.0f) {
				blueSpawnTime = 0.6f;
				increaseSpawnBlue = false;
			}
			else {
				blueSpawnTime = 1.0f;
				isGreenSpawn = true;
				greenElapsedTime = Time.time;
			}
		}*/

		yield return new WaitForSeconds(blueSpawnTime);
		GameObject enemy = Instantiate<GameObject>(blueEnemyPrefab);
		enemy.transform.parent = this.gameObject.transform;
		enemy.GetComponent<T_Blue>().Init(xValue, zValue, false);

		isBlueSpawn = true;
	}

	IEnumerator SpawnGreen() {
		float xValue = Random.Range(-9.0f, 9.0f);
		float zValue = Random.Range(-9.0f, 9.0f);

		/*if (greenSpawnTime < 1.75f) {
			increaseSpawnGreen = false;
			greenSpawnTime = 1.75f;
		}*/

		StartCoroutine(PlaceMarker(1, xValue, zValue));
		yield return new WaitForSeconds(greenSpawnTime);
		GameObject enemy = Instantiate<GameObject>(greenEnemyPrefab);
		enemy.transform.parent = this.gameObject.transform;
		enemy.GetComponent<T_Green>().Init(xValue, zValue, false);

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

	private void KillPlayer() {
		player.Die();
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

	public void IncrementKills() {
		killCount++;
	}
}
