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

	bool gameOn = false;

	void Awake() {
		gameManager = this;
	}

	public void StartGame () {
		if (gameOn) {
			return;
		}

		//add the player into the scene
		Player newPlayer = Instantiate (playerPrefab.GetComponent<Player>());
		newPlayer.transform.position = playerSpawn.transform.position;

		//trigger any classes that are listening for the begin event
		BeginEvent ();
		gameOn = true;

		score = 0;
	}

	public void EndGame () {
		if (!gameOn) {
			return;
		}
		//trigger any classes listening for the endEvent
		EndEvent ();
		gameOn = false;

		if (Player.player != null) {
			Destroy (Player.player.gameObject);
		}

		//clean up any enemies still around
		Transform parent = EnemySpawner.spawner.transform;
		for (int i = 0; i < parent.childCount; i++) {
			Destroy (parent.GetChild (i).gameObject);
		}
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.Return)) {
			StartGame ();
		}
	}
}
