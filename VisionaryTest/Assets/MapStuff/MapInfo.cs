using UnityEngine;
using System.Collections;

public class MapInfo : MonoBehaviour {
	const int MAP_X_OFFSET = 9;
	const int MAP_Y_OFFSET = 5;

	const int WALL = 1;

	private LineReader reader;
	public static MapInfo map;

	/// <summary>
	/// The binary map.
	/// A 1 is a wall that cannot be traversed while a 0 is free space.
	/// </summary>
	private int[,] binary = new int[19,11];

	/// <summary>
	/// Contains refferences to all the nodes on the map
	/// </summary>
	private MapNode[,] nodeMap = new MapNode[19, 11];

	public int MapRangeX {
		get {
			return binary.GetLength (0);
		}
	}
	public int MapRangeY {
		get {
			return binary.GetLength (1);
		}
	}

	void Awake() {
		//allow this object to be access statically
		map = this;
	}

	void Start() {
		reader = GetComponent<LineReader> ();
		
		string str;
		//initialize the binary map array
		for (int j = 0; j < reader.lines.Count; j++) {
			str = reader.lines [j];
			for (int i = 0; i < str.Length; i++) {
				binary [i, j] = (int)char.GetNumericValue (str [i]);
			}
		}
	}

	Point ConvertToMapLocation (float x, float y) {
		return ConvertToMapLocation (Mathf.RoundToInt (x), Mathf.RoundToInt (y));
	}

	/// <summary>
	/// converts from world coordinates to map coordinates.
	/// </summary>
	/// <returns>The to map location.</returns>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	public Point ConvertToMapLocation (int x, int y) {
		Point mapLocation = new Point (0, 0);
		mapLocation.x = x + MAP_X_OFFSET;//9
		mapLocation.y = MAP_Y_OFFSET - y;//5
		return mapLocation;
	}

	//coverts back to world position from map position
	public Point ConvertToWorldLocation(int x, int y) {
		//surprisingly the exact same equation to convert back
		Point worldLocation = new Point (0, 0);
		worldLocation.x = x - MAP_X_OFFSET;
		worldLocation.y = MAP_Y_OFFSET - y;

		return worldLocation;
	}

	public bool IsWall (Vector2 v) {
		return IsWall (v.x, v.y);
	}

	/// <summary>
	/// Determines whether this spot in world space has a wall in it.
	/// </summary>
	/// <returns><c>true</c> if this instance is a wall at the specified x y; otherwise, <c>false</c>.</returns>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	public bool IsWall (float x, float y) {

		//Use Point here so we don't have to keep converting to int
		Point mapLocation = ConvertToMapLocation (x, y);
		//1 wall, 0 open
		return IsWallMapSpace(mapLocation);
	}

	public bool IsWallMapSpace (Point mapLocation) {
		return binary[mapLocation.x, mapLocation.y] == WALL;
	}

	/// <summary>
	/// Adds the node to map.
	/// </summary>
	/// <param name="node">Node.</param>
	public void AddNodeToMap (MapNode node) {
		Point mapLocation = ConvertToMapLocation (node.transform.position.x, node.transform.position.y);
		if (nodeMap [mapLocation.x, mapLocation.y] != null) {
			Debug.LogError ("two nodes some how got added to the same location");
		}
		nodeMap [mapLocation.x, mapLocation.y] = node;
	}

	/// <summary>
	/// Gets the node at world position.
	/// </summary>
	/// <returns>The <see cref="MapNode"/>.</returns>
	/// <param name="position">World position.</param>
	public MapNode GetNodeAt (float x, float y) {
		Point mapLocation = ConvertToMapLocation (x, y);
		MapNode node = nodeMap [mapLocation.x, mapLocation.y];
		return node;
	}


}
