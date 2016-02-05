using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//This ended up just sort of doing a 50/50 wandering and following
public class FollowAI : Enemy {

	protected override List<Point> MakeDesision (MapNode node)
	{
		List<Point> nextSet;

		float yDirection = Player.player.transform.position.y - transform.position.y;
		float xDirection = Player.player.transform.position.x - transform.position.x;

		if (Mathf.Abs (xDirection) > Mathf.Abs (yDirection)) {
			if (xDirection > 0) {
				nextSet = node.East;
			} else {
				nextSet = node.West;
			}
		} else {
			if (yDirection > 0) {
				nextSet = node.North;
			} else {
				nextSet = node.South;
			}
		}

		//prevents backtracking or non valid direction
		//defaults to random in that case
		if (nextSet.Count <= 0 || (lastNode != null && node.GetNodeFor(nextSet) == lastNode)) {
			return base.MakeDesision (node);
		}

		return nextSet;
	}
}
