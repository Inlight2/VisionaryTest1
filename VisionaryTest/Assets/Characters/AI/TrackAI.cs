using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrackAI : Enemy {

	//Picks the direction that leads to being the closest to the player
	protected override List<Point> MakeDesision (MapNode node)
	{
		List<Point> nextSet;

		Vector3 playerPosition = Player.player.transform.position;

		float northDistance = Vector3.Distance (node.NorthNode.transform.position, playerPosition);
		float eastDistance = Vector3.Distance (node.EastNode.transform.position, playerPosition);
		float southDistance = Vector3.Distance (node.SouthNode.transform.position, playerPosition);
		float westDistance = Vector3.Distance (node.WestNode.transform.position, playerPosition);

		float[] distances = new float[]{ northDistance, eastDistance, southDistance, westDistance };
		float minDistance = Mathf.Min (distances);

		if (minDistance == northDistance) {
			nextSet = node.North;
		} else if (minDistance == eastDistance) {
			nextSet = node.East;
		} else if (minDistance == southDistance) {
			nextSet = node.South;
		} else {
			nextSet = node.West;
		}

		//prevents backtracking or non valid direction
		//defaults to random in that case
		if (nextSet.Count <= 0 || (lastNode != null && node.GetNodeFor(nextSet) == lastNode)) {
			return base.MakeDesision (node);
		}

		return nextSet;
	}
}
