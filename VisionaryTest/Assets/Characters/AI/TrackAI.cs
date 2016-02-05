using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrackAI : Enemy {

	protected override List<Point> MakeDesision (MapNode node)
	{
		return MakeDesision (node, Player.player.transform.position);
	}

	//I have made this it's own function so that I can extend the class and make slightly different versions
	//Picks the direction that leads to being the closest to the player
	protected List<Point> MakeDesision (MapNode node, Vector3 trackPosition)
	{
		List<Point> nextSet;

		float northDistance = Vector3.Distance (node.NorthNode.transform.position, trackPosition);
		float eastDistance = Vector3.Distance (node.EastNode.transform.position, trackPosition);
		float southDistance = Vector3.Distance (node.SouthNode.transform.position, trackPosition);
		float westDistance = Vector3.Distance (node.WestNode.transform.position, trackPosition);

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
