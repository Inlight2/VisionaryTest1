using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Makes Random decisions at nodes
public class RandomAI : Enemy {

	protected override List<Point> MakeDesision (MapNode node)
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
}
