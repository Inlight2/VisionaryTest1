﻿using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	const float VERTICAL_LIMIT = 5.5f;
	const float HORIZONTAL_LIMIT = 9.5F;

	public float speed = 10f;
	Vector3 direction = Vector3.zero;

	public bool piercing = false;
	const int PIERCE_LIMIT = 3;
	int pierces = 0;

	void Update () {
		transform.position = transform.position + direction * speed * Time.deltaTime;
		if (Mathf.Abs (transform.position.x) > HORIZONTAL_LIMIT || Mathf.Abs (transform.position.y) > VERTICAL_LIMIT  ||
			MapInfo.map.IsWall(transform.position.x, transform.position.y)) {
			Destroy (this.gameObject);
		}
	}

	public void SetDirection (Player.Direction dir) {
		switch (dir) {
		case Player.Direction.NORTH:
			direction = Vector3.up;
			break;
		case Player.Direction.EAST:
			direction = Vector3.right;
			break;
		case Player.Direction.SOUTH:
			direction = Vector3.down;
			break;
		case Player.Direction.WEST:
			direction = Vector3.left;
			break;
		}
	}

	public void OnTriggerEnter (Collider other) {
		if (other.GetComponent<Enemy> ()) {
			GameManager.EnemyKilledEvent (other.GetComponent<Enemy> ());
			if (piercing && pierces <= PIERCE_LIMIT) {
				pierces++;
			} else {
				Destroy (gameObject);
			}
		} else if (other.GetComponent<Pickup>() && other.GetComponent<Pickup>().curType == Pickup.pickType.ENEMY_BOOST) {
			//Destroy (other.gameObject);

			if (piercing && pierces <= PIERCE_LIMIT) {
				pierces++;
			} else {
				Destroy (gameObject);
			}
		}
	}
}
