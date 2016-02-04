using UnityEngine;
using System.Collections;

public class MapInfo : MonoBehaviour {

	private LineReader reader;

	/// <summary>
	/// The binary map.
	/// A 1 is a wall that cannot be traversed while a 0 is free space.
	/// </summary>
	private int[,] binary = new int[19,11];

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

}
