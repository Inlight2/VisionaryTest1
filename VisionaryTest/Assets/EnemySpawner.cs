using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {

	public static EnemySpawner spawner;
	[SerializeField] Enemy[] enemies = new Enemy[3];

	Queue<Enemy> enemiesToSpawn = new Queue<Enemy> ();

	//lets slow down how fast they come out of spawn
	int spawnWaitFrames = 20;
	int waitedFrames= 0;

	void Awake() {
		spawner = this;
		GameManager.EndEvent += delegate {
			enemiesToSpawn.Clear ();
		};

		GameManager.BeginEvent += delegate {
			SpawnMoreEnemies ();
			SpawnMoreEnemies ();
		};
	}

	public void SpawnMoreEnemies () {
		Enemy newEnemy = Instantiate (enemies[Random.Range(0,3)]);

		newEnemy.transform.SetParent (transform);
		newEnemy.transform.position = transform.position;

		enemiesToSpawn.Enqueue (newEnemy);
	}

	void Update() {
		if (enemiesToSpawn.Count > 0 && waitedFrames >= spawnWaitFrames) {
			Enemy toSpawn = enemiesToSpawn.Dequeue ();
			toSpawn.gameObject.SetActive (true);
			waitedFrames = 0;
		} else {
			waitedFrames++;
		}
	}
}
