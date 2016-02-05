using UnityEngine;
using System.Collections;

public class Player : Character {

	public enum Direction {
		NORTH,
		EAST,
		SOUTH,
		WEST,
	}

	public enum Mode {
		GHOST,
		NORMAL,
	}

	public static Direction curDirection;
	public static Mode curMode;

	private bool moving = false;

	void Awake() {
		curMode = Mode.NORMAL;
		curDirection = Direction.NORTH;
	}

	//attempt to move in the direction the player is facing
	void Move() {
		Vector2 moveDirection = Vector2.zero;
		moving = false;
		//check which direction then check if the space in that direction is a wall
		switch (curDirection) {
		case Direction.NORTH:
			moveDirection = new Vector2 (0, 1);
			break;
		case Direction.EAST:
			moveDirection = new Vector2 (1, 0);
			break;
		case Direction.SOUTH:
			moveDirection = new Vector2 (0, -1);
			break;
		case Direction.WEST:
			moveDirection = new Vector2 (-1, 0);
			break;
		default:
			return;
		}
		Vector2 curPos = new Vector2 (transform.position.x, transform.position.y);
		if (!MapInfo.map.IsWall (curPos + moveDirection)) {
			moving = true;
			this.MoveTo (curPos + moveDirection, Move);
		} else {
			return;
		}
	}

	void Update() {
		if (Input.GetKeyDown ("W")) {
			curDirection = Direction.NORTH;
		}
		if (Input.GetKeyDown ("A")) {
			curDirection = Direction.WEST;
		}
		if (Input.GetKeyDown ("S")) {
			curDirection = Direction.SOUTH;
		}
		if (Input.GetKeyDown ("D")) {
			curDirection = Direction.EAST;
		}

		if (!moving) {
			Move ();
		}
	}
}
