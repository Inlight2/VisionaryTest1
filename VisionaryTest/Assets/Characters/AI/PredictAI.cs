using UnityEngine;
using System.Collections;

public class PredictAI : TrackAI {

	const int PREDICT_DISTANCE = 5;

	protected override System.Collections.Generic.List<Point> MakeDesision (MapNode node)
	{

		Player.Direction playerDir = Player.player.curDirection;
		Vector3 prediction = Player.player.transform.position;

		switch (playerDir) {
		case Player.Direction.NORTH:
			prediction += Vector3.up * PREDICT_DISTANCE;
			break;
		case Player.Direction.EAST:
			prediction += Vector3.right * PREDICT_DISTANCE;
			break;
		case Player.Direction.SOUTH:
			prediction += Vector3.down * PREDICT_DISTANCE;
			break;
		case Player.Direction.WEST:
			prediction += Vector3.left * PREDICT_DISTANCE;
			break;
		}

		return MakeDesision (node, prediction);
	}
}
