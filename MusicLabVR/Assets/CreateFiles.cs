using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class CreateFiles : MonoBehaviour {

	public GameObject fileObject;

	public static int DirCount(DirectoryInfo d)
	{
		int result = 0;
		// Add file sizes.
		FileInfo[] fis = d.GetFiles();
		foreach (FileInfo fi in fis)
		{
			result++;
		}
		return result;
	}

	// Use this for initialization
	void Start () {
		
		String ressourcePath = Path.Combine (Application.dataPath, "Resources"); // Get Path to game resources folder
		DirectoryInfo di = new DirectoryInfo(Path.Combine(ressourcePath,"StreamingAssets")); // Get Path to Streaming assets

		int numberFiles = CreateFiles.DirCount (di)/2;
		FileInfo[] fis = di.GetFiles();
		float x = 0f;
		float y = 0.1f;
		float z = -4f;
		for (int i = 0; i < numberFiles; i++) {
			GameObject newObj = Instantiate (fileObject, this.transform,false);
			newObj.transform.localPosition = new Vector3 (x, y, z + i);
			String fileName = fis [2*i].Name;
			String realName = fileName.Substring (0, (fileName.Length - 5));
			newObj.GetComponent<MusicFileScript> ().fileName = realName;
			newObj.transform.GetChild (0).GetComponent<TextMesh> ().text = realName;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	
}
