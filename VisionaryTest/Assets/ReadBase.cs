using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public abstract class ReadBase : MonoBehaviour {

	public LineReader reader;

	void Awake() {
		reader = GetComponent<LineReader> ();
	}

	public void PrintArray<T>(T[] arr) {
		string output = "";
		foreach (T el in arr) {
			output += el.ToString () + ",";
		}
		output = output.Substring (0, output.Length - 1);
		Debug.Log (output);
	}

	public void PrintDoubleArray(int[,] arr) {
		string line = "";
		int x = 0, y = 0;
		while (y < arr.GetLength(1)) {
			while (x < arr.GetLength(0)) {
				line += arr [x, y];
				x++;
			}
			Debug.Log (line);
			line = "";
			x = 0;
			y++;
		}
	}
}
