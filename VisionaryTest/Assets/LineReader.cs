using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;

public class LineReader : MonoBehaviour {
	[SerializeField]
	string fileLocation = "";

	FileInfo sourceFile = null;
	StreamReader reader = null;
	string text = "";

	public List<String> lines = new List<string> ();

	void Awake () {
		//open file, read lines and store lines in a list
		sourceFile = new FileInfo (fileLocation);
		reader = sourceFile.OpenText ();
		while (true){
			text = reader.ReadLine ();
			if (text == null) {
				break;
			}
			lines.Add (text);
		}
	}
}
