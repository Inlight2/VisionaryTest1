using UnityEngine;
using System.Collections;

public class PickupSpawner : MonoBehaviour {

	[SerializeField] Pickup[] pickups = new Pickup[3];

	const int SPAWN_FREQUENCY = 150;
	int spawnWaitTime;

	MapInfo map;

	void Start() {
		map = MapInfo.map;
	}

	void OnEnable() {
		GameManager.EndEvent += GameEnd;
	}

	void OnDisable() {
		GameManager.EndEvent -= GameEnd;
	}

	void GameEnd() {
		Transform parent = transform;
		for (int i = 0; i < parent.childCount; i++) {
			Destroy (parent.GetChild (i).gameObject);
		}
	}

	//picks random locations till it finds one that isn't a wall.
	//pickups can stack
	Pickup SpawnPickup() {
		Point randomPoint = new Point (Random.Range (0, map.MapRangeX), Random.Range (0, map.MapRangeY));
		Debug.Log(randomPoint.x + " " + randomPoint.y);
		if (map.IsWallMapSpace (randomPoint)) {
			return SpawnPickup ();
		}
		Pickup newPickup = Instantiate(pickups[Random.Range(0,pickups.Length)]);
		randomPoint = map.ConvertToWorldLocation (randomPoint.x, randomPoint.y);
		newPickup.transform.parent = transform;
		newPickup.transform.position = new Vector3 (randomPoint.x, randomPoint.y, 0f);
		return newPickup;
	}

	void Update() {
		Debug.Log (GameManager.gameManager.GameOn);
		if (GameManager.gameManager.GameOn) {
			spawnWaitTime++;
		}
		if (spawnWaitTime >= SPAWN_FREQUENCY) {
			SpawnPickup ();
			spawnWaitTime = 0;
		}
	}
}
