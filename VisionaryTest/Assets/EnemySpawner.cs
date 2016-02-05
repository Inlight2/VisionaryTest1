using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public static EnemySpawner spawner;

	void Awake() {
		spawner = this;
	}
}
