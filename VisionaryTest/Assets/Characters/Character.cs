﻿using UnityEngine;
using System.Collections;
using System;

//Character moves in global coordinates
public class Character : MonoBehaviour {
	//character's movement speed
	[SerializeField] protected float initialSpeed = 0.1f;
	[SerializeField] protected float speed = 0.1f;
	//when a character over steps the target destination but we still want the movement past that object to be smooth
	private float overStep;

	void OnDisable() {
		StopAllCoroutines ();
	}

	/// <summary>
	/// Manages the movement to a point
	/// </summary>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	/// <param name="a">A method to be run on completion.</param>
	public IEnumerator MoveTo(int x, int y, Action a = null) {
		//overstep is carried over from last moveTo but has to be stepped
		//first in over to keep a consistent speed around corners
		if (overStep > 0) {
			//this will expend the over step
			MoveTowards (x, y);
			yield return null;
		}

		//will return true untill it reaches it's destination
		while (MoveTowards(x, y)) {
			//move once per frame
			yield return null;
		}
		if (a != null) {
			//run the next action queued up
			a ();
		}
	}

	/// <summary>
	/// Moves the towards the point on step at a time
	/// </summary>
	/// <returns><c>true</c>, still moving towards<c>false</c> has reached destination.</returns>
	/// <param name="x">The x destination.</param>
	/// <param name="y">The y destination.</param>
	public bool MoveTowards(int x, int y) {
		Vector3 destination = new Vector3 (x, y, 0f);

		//we need to detect if we're going to over step the target and save the extra movement for the next time we move
		float distance = Vector3.Distance (destination, gameObject.transform.position);

		if (distance < speed) {
			overStep = speed - distance;
			transform.position = new Vector3 (x, y, transform.position.z);
			return false;
		} else {
			//get the direction we're moving
			Vector3 direction = new Vector3 (transform.position.x - destination.x, transform.position.y - y, 0f);
			direction.Normalize();

			//store an overstep if there's overstep
			float speedThisFrame = speed;
			if (overStep > 0) {
				speedThisFrame = overStep;
				overStep = 0;
			}

			Vector3 newPosition = new Vector3 (transform.position.x + direction.x * speedThisFrame * Time.deltaTime,
				                      transform.position.y + direction.y * speedThisFrame * Time.deltaTime,
				                      transform.position.z);
			transform.position = newPosition;
			return true;
		}


	}


}
