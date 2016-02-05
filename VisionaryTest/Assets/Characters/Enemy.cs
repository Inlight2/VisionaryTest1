using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : Character {
	const int _SCORE_WORTH = 10;
	public int SCORE_WORTH {
		get {
			return _SCORE_WORTH;
		}
	}

	//to prevent AI backtracking
	protected MapNode lastNode;
	//How we keep track of which directions we've already followed
	int directionsIndex = 0;
	protected List<Point> curDirections;

	void OnEnable() {
		List<Point> leaveSpawn = new List<Point> ();
		leaveSpawn.Add (new Point (0, 2));
		//leave the spawn
		directionsIndex = 0;
		curDirections = leaveSpawn;
		FollowDirections();
	}

	/// <summary>
	/// Given a map node Decides what direction to go in
	/// </summary>
	/// <returns>The next set of directions.</returns>
	/// <param name="node">Node.</param>
	protected virtual List<Point> MakeDesision (MapNode node)
	{
		List<Point> nextSet;

		int random = Random.Range (1, 5);
		switch (random) {
		case 1:
			nextSet = node.North;
			break;
		case 2:
			nextSet = node.East;
			break;
		case 3:
			nextSet = node.South;
			break;
		case 4:
			nextSet = node.West;
			break;
		default:
			return MakeDesision (node);
		}
		//Make sure we're not choosing a path that doesn't exist
		if (nextSet.Count <= 0 || (lastNode != null && node.GetNodeFor(nextSet) == lastNode)) {
			return MakeDesision (node);
		}

		return nextSet;
	}

	void FollowDirections () {
		if (directionsIndex >= curDirections.Count) {
			//When a set of directions are over, the AI will be sitting on the next node
			MapNode curNode = MapInfo.map.GetNodeAt(transform.position.x, transform.position.y);

			//When the AI reaches the first node, this check catches the null ref
			//There is no way to back track back into enemy spawn so this is fine

			directionsIndex = 0;
			curDirections = MakeDesision (curNode);
			lastNode = curNode;

		}
		Point nextDirection = curDirections [directionsIndex];
		directionsIndex++;
		MoveTo (nextDirection.x + transform.position.x, nextDirection.y + transform.position.y, FollowDirections);
	}
		
}
