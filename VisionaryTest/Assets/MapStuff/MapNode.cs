using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
/// <summary>
/// A class to hold x and y directions for AI
/// </summary>
public class Point {
	[SerializeField] public int x;
	[SerializeField] public int y;

	public Point(int _x, int _y) {
		x = _x;
		y = _y;
	}

	//And this was the moment I realized I completely forgot about vector2s
	public static float Distance(Point a, Point b) {
		return Mathf.Sqrt (Mathf.Pow ((a.x - b.x), 2) + Mathf.Pow ((a.y - b.y), 2));
	}
}

public class MapNode : MonoBehaviour {

	//We need direction for each possible cardinal direction
	//the reason we don't store this in an array is because I need to be able to see the contents from the editor to set up the AI
	[SerializeField] List<Point> northDirections = new List<Point>();
	[SerializeField] List<Point> eastDirections = new List<Point>();
	[SerializeField] List<Point> southDirections = new List<Point>();
	[SerializeField] List<Point> westDirections = new List<Point>();

	public List<Point> North {
		get {
			return northDirections;
		}
	}

	public List<Point> East {
		get {
			return eastDirections;
		}
	}

	public List<Point> South {
		get {
			return southDirections;
		}
	}

	public List<Point> West {
		get {
			return westDirections;
		}
	}

	void Awake() {
		//I have this turned on so I can edit the map easier but it needs to be off during game play
		GetComponent<MeshRenderer> ().enabled = false;
	}

	void Start() {
		MapInfo.map.AddNodeToMap (this);
	}

	public MapNode GetNodeFor(List<Point> points) {
		Vector2 position = new Vector2 (transform.position.x, transform.position.y);
		foreach (Point p in points) {
			position.x += p.x;
			position.y += p.y;
		}

		return MapInfo.map.GetNodeAt (position.x, position.y);
	}

	public MapNode NorthNode {
		get {
			return GetNodeFor (North);
		}
	}

	public MapNode EastNode {
		get {
			return GetNodeFor (East);
		}
	}

	public MapNode SouthNode {
		get {
			return GetNodeFor (South);
		}
	}

	public MapNode WestNode {
		get {
			return GetNodeFor (West);
		}
	}

}
