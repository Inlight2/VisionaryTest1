using UnityEngine;
using System.Collections;

public class Player : Character {

	public static Player player;
	//the pivot is what turns the gun on the player
	[SerializeField] GameObject pivot;
	[SerializeField] Bullet bulletPrefab;

	[SerializeField] Material normalMaterial;
	[SerializeField] Material ghostMaterial;

	public enum Direction {
		NORTH,
		EAST,
		SOUTH,
		WEST,
	}

	const float GHOST_DURATION = 5;
	public enum Mode {
		GHOST,
		NORMAL,
	}

	[SerializeField] private Direction nextDirection;
	[SerializeField] public Direction curDirection;
	public static Mode curMode;

	//The player has to lift the fire key and press it again
	//as opposed to just holding it down
	//private bool shooting = false;
	private bool moving = false;
	//where we're goin
	private Vector2 target;
	//where we came from
	private Vector2 origin;

	public int piercingBullets = 0;

	void Awake() {
		player = this;
		origin = new Vector2 (transform.position.x, transform.position.y);
		curMode = Mode.NORMAL;

		//next direction allows you to input a turn before you get there
		//but won't stick you to a wall
		nextDirection = Direction.NORTH;
		curDirection = Direction.NORTH;

		//I'm getting some wierd physics interaction on the player, and
		//flipping these options fix it

	}

	//reduces speed back to it's original value;
	public void RevertSpeed() {
		speed = 3f;
	}

	Vector2 DirectionToVector(Direction dir) {
		switch (dir) {
		case Direction.NORTH:
			return Vector2.up;
		case Direction.EAST:
			return Vector2.right;
		case Direction.SOUTH:
			return Vector2.down;
		case Direction.WEST:
			return Vector2.left;
		default:
			return Vector2.zero;
		}
	}

	/// <summary>
	/// attempt to move in the specified direction
	/// </summary>
	/// <param name="dir">Direction</param>
	void Move() {
		Vector2 nextMoveDirection = DirectionToVector (nextDirection);
		Vector2 curMoveDirection = DirectionToVector(curDirection);

		//check which direction then check if the space in that direction is a wall
		Vector2 curPos = new Vector2 (transform.position.x, transform.position.y);
		if (!MapInfo.map.IsWall (curPos + nextMoveDirection)) {
			moving = true;
			origin = new Vector2 (transform.position.x, transform.position.y);
			target = curPos + nextMoveDirection;
			curDirection = nextDirection;
			MoveTo (target.x, target.y, Move);
		} else if (!MapInfo.map.IsWall (curPos + curMoveDirection)) {
			moving = true;
			origin = new Vector2 (transform.position.x, transform.position.y);
			target = curPos + curMoveDirection;
			MoveTo (target.x, target.y, Move);
		} else {
			moving = false;
		}
	}

	//the player can backtrack even when not on a corner
	void SwapDirection () {
		if (!moving) {
			return;
		}
		StopAllCoroutines ();

		Vector2 tmp = origin;
		origin = target;
		target = tmp;
		curDirection = nextDirection;
		MoveTo (target.x, target.y, Move);
	}
		
	void FireBullet (Direction dir) {
		Bullet newBullet = Instantiate<Bullet> (bulletPrefab);
		newBullet.transform.position = transform.position;
		newBullet.SetDirection (dir);
		if (piercingBullets > 0) {
			newBullet.piercing = true;
			piercingBullets--;
		}
	}

	public void BeginGhost() {
		//reset the timer if we're already ghosting
		if (curMode == Mode.GHOST) {
			StopCoroutine ("Ghost");
		}
		
		StartCoroutine ("Ghost");
	}

	//ghost mode for 5 seconds
	IEnumerator Ghost() {
		curMode = Mode.GHOST;
		GetComponent<MeshRenderer> ().material = ghostMaterial;
		yield return new WaitForSeconds (GHOST_DURATION);
		GetComponent<MeshRenderer> ().material = normalMaterial;
		curMode = Mode.NORMAL;
	}

	void Update() {
		
		//get movement inputs from player
		if (Input.GetKeyDown (KeyCode.W)) {
			if (curDirection == Direction.SOUTH) {
				SwapDirection ();
			}
			nextDirection = Direction.NORTH;
		}
		if (Input.GetKeyDown (KeyCode.A)) {
			if (curDirection == Direction.EAST) {
				SwapDirection ();
			}
			nextDirection = Direction.WEST;
		}
		if (Input.GetKeyDown (KeyCode.S)) {
			if (curDirection == Direction.NORTH) {
				SwapDirection ();
			}
			nextDirection = Direction.SOUTH;
		}
		if (Input.GetKeyDown (KeyCode.D)) {
			if (curDirection == Direction.WEST) {
				SwapDirection ();
			}
			nextDirection = Direction.EAST;
		}

		//get shots fired input
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			FireBullet (Direction.NORTH);
			//0,0,0,1
			pivot.transform.localRotation = new Quaternion (0f, 0f, 0f, 1f);
		} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
			FireBullet (Direction.EAST);
			//0,0,0.7,-0.7
			pivot.transform.localRotation = new Quaternion (0f, 0f, 0.7f, -0.7f);
		} else if (Input.GetKeyDown (KeyCode.DownArrow)) {
			FireBullet (Direction.SOUTH);
			//0,0,1,0
			pivot.transform.localRotation = new Quaternion (0f, 0f, 1f, 0f);
		} else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			FireBullet (Direction.WEST);
			//0,0,.7,.7
			pivot.transform.localRotation = new Quaternion (0f, 0f, 0.7f, 0.7f);
		}

		if (!moving) {
			Move ();
		}
	}

	public void OnTriggerEnter (Collider other) {
		if (other.GetComponent<Enemy> ()) {
			if (curMode == Mode.NORMAL) {
				Destroy (gameObject);
				GameManager.gameManager.EndGame ();
			}
		}
	}
}
