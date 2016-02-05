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

	/// <summary>
	/// converts from world coordinates to map coordinates.
	/// </summary>
	/// <returns>The to map location.</returns>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	Point ConvertToMapLocation (int x, int y) {
		Point mapLocation = new Point (0, 0);
		mapLocation.x = x + MAP_X_OFFSET;//9
		mapLocation.y = MAP_Y_OFFSET - y;//5
		return mapLocation;
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
	public bool IsWall (float _x, float _y) {
		int x = Mathf.RoundToInt (_x);
		int y = Mathf.RoundToInt (_y);

		//Use Point here so we don't have to keep converting to int
		Point mapLocation = ConvertToMapLocation (x, y);
		//1 wall, 0 open
		return binary[mapLocation.x, mapLocation.y] == WALL;
	}
}
