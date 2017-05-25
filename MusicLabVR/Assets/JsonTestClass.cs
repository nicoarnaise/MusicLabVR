using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;


public class JsonTestClass{

	 /*	MusicalFile musicFile = new MusicalFile ();
		musicFile.numberNote = 4;
		musicFile.musicalNote = new MusicalNote[4];
		musicFile.musicalNote [0] = new MusicalNote (1,4,4);
		musicFile.musicalNote [1] = new MusicalNote (2,4,4);
		musicFile.musicalNote [2] = new MusicalNote (3,4,2);
		musicFile.musicalNote [3] = new MusicalNote (4,4,2);


		String generatedString = MapToJSON (musicFile); // Save object to JSON string

		String ressourcePath = Path.Combine (Application.dataPath, "Resources"); // Get Path to game resources folder
		String filePath =  Path.Combine("StreamingAssets", "Test.json"); // Get Path to file in resources folder
		String realPath = Path.Combine(ressourcePath,filePath); // Get Real Path

		SaveJSONToFile (generatedString, realPath); // Write Json string to file

		String newPath =  Path.Combine("StreamingAssets", "Test"); // Get Path to file in resources folder without .json !
		String savedString = LoadJSONFromFile(newPath); // Load Json from file

		MusicalFile musicFileLoaded = JSONToObject(savedString) ;  // Create Object from Json

		Debug.Log (musicFileLoaded.musicalNote [1].ToString()); */


	public static string MapToJSON(MusicalFile obj){ // Save object to JSON string
		return JsonUtility.ToJson (obj);
	}

	public static void SaveJSONToFile(string JSON, string path){ // Write Json string to file
		File.WriteAllText (path, JSON);
		#if UNITY_EDITOR
		UnityEditor.AssetDatabase.Refresh ();
		#endif
	}


	public static string LoadJSONFromFile(string path){ // Load Json string from file
		TextAsset asset = Resources.Load(path) as TextAsset;
		return asset.text;
	}

	public static MusicalFile JSONToObject(string JSON){ // Create Object from Json
		return JsonUtility.FromJson<MusicalFile> (JSON);
	}




	[System.Serializable]
	public class MusicalFile{
		
		public MusicalNote[] musicalNote;
		public int numberNote ;

	}
		

	[System.Serializable]
	public class MusicalNote{
		
		public int noteName;
		public int noteOctave;
		public float noteTempo;


		public MusicalNote(int n, int o, float t){
			noteName = n;
			noteOctave = o;
			noteTempo = t;
		}

		public override string ToString ()
		{
			return ("noteName = " + noteName + " noteOctave = " + noteOctave + " noteTempo = " + noteTempo);
		}
	}



}
