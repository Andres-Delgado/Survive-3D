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
	private int currentTutorial = 0;

	#endregion

	private void Awake() {
		_instance = this;
	}

	private void Update() {
		if (T_GameManager.Instance.isPaused || (!T_GameManager.Instance.running)) { return; }
		CheckIfFinished(currentTutorial);
	}

	public void StartLevel(int level) {
		switch (level) {
			case 0:
				currentTutorial = killCount = 0;
				CreatePlayer(0);
				SpawnLevel0();
				break;
			case 1:

				break;
			case 2:

				break;
			case 3:

				break;
			default:
				break;
		}
	}

	private void CheckIfFinished(int level) {
		switch (level) {
			case 0:
				if (killCount >= 8) {
					KillChildren();
					KillPlayer();
					T_GameManager.Instance.EndLevel(0, true);
				}
				break;
			case 1:

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
		KillChildren();
		KillPlayer();
	}

	private void CreatePlayer(int level) {
		GameObject _player = Instantiate<GameObject>(playerPrefab);
		player = _player.GetComponent<T_Player>();
		//UIManager.Instance.SetPotionText(upgrades[2]);
		//player.Init(highScore, credits, upgrades[0], upgrades[1], upgrades[2], dashAbility, bulletWaveAbility);
		player.Init(0, 0, 0, 0, false);
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

	private void SpawnLevel0() {
		int xPosition = 7;
		int zPosition = 7;
		for (int i = 0; i < 4; i++) {
			zPosition *= -1;
			if (i == 2) { xPosition *= -1; }

			GameObject enemy = Instantiate<GameObject>(blueEnemyPrefab);
			enemy.transform.parent = this.gameObject.transform;
			enemy.GetComponent<T_Blue>().Init(xPosition, zPosition, false, false);
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
			enemy.GetComponent<IDamageable>().Init(xPosition, zPosition);
		}
	}

	public void IncrementKills() {
		killCount++;
	}
}
