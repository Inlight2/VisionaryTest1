using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public static GameManager gameManager;
	[SerializeField] Text scoreText;
	[SerializeField] GameObject playerPrefab;
	[SerializeField] GameObject playerSpawn;

	public static Action BeginEvent;
	public static Action EndEvent;
	public static Action<Enemy> EnemyKilledEvent;

	int _score;
	public int score {
		get {
			return _score;
		}

		set {
			_score = value;
			scoreText.text = "Score: " + value;
		}
	}

	bool _gameOn = false;
	public bool GameOn {
		get {
			return _gameOn;
		}
	}

	void Awake() {
		gameManager = this;

		EnemyKilledEvent += EnemyKilled;
	}

	public void StartGame () {
		if (_gameOn) {
			return;
		}

		//add the player into the scene
		Player newPlayer = Instantiate (playerPrefab.GetComponent<Player>());
		newPlayer.transform.position = playerSpawn.transform.position;

		//trigger any classes that are listening for the begin event
		if (BeginEvent != null) {
			BeginEvent ();
		}
		_gameOn = true;

		score = 0;
	}

	public void EndGame () {
		if (!_gameOn) {
			return;
		}
		//trigger any classes listening for the endEvent
		if (EndEvent != null) {
			EndEvent ();
		}
		_gameOn = false;

		if (Player.player != null) {
			Destroy (Player.player.gameObject);
		}

		//clean up any enemies still around
		Transform parent = EnemySpawner.spawner.transform;
		for (int i = 0; i < parent.childCount; i++) {
			Destroy (parent.GetChild (i).gameObject);
		}
	}

	const int SCORE_TICKS = 7;
	int ticks = 0;

	void Update() {
		if (_gameOn) {
			ticks++;
			if (ticks >= SCORE_TICKS) {
				score += 1;
				ticks = 0;
			}
		}

		if (Input.GetKeyDown (KeyCode.Return)) {
			StartGame ();
		}
	}

	void EnemyKilled(Enemy enemy) {
		score += enemy.SCORE_WORTH;
		Destroy (enemy.gameObject);

		EnemySpawner.spawner.SpawnMoreEnemies ();
		EnemySpawner.spawner.SpawnMoreEnemies ();
	}
}
