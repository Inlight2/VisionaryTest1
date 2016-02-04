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
}
