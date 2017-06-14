using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class CreateFiles : MonoBehaviour {

	public GameObject fileObject;
    public int nbFileShown = 10;

    private List<GameObject> files;
    private int firstIndex;

    private float x = 0f;
    private float y = 0.1f;
    private float z = -4f;

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

        firstIndex = 0;
        files = new List<GameObject>();
        int nbShown = 0;

		String ressourcePath = Path.Combine (Application.dataPath, "Resources"); // Get Path to game resources folder
		DirectoryInfo di = new DirectoryInfo(Path.Combine(ressourcePath,"StreamingAssets")); // Get Path to Streaming assets

		int numberFiles = CreateFiles.DirCount (di)/2;
        FileInfo[] fis = di.GetFiles();
		for (int i = 0; i < numberFiles; i++) {
			GameObject newObj = Instantiate (fileObject, this.transform,false);
			newObj.transform.localPosition = new Vector3 (x, y, z + i);
			String fileName = fis [2*i].Name;
			String realName = fileName.Substring (0, (fileName.Length - 5));
			newObj.GetComponent<MusicFileScript> ().fileName = realName;
			newObj.GetComponent<LevelSelectScript> ().fileName = realName;
			newObj.transform.GetChild (0).GetComponent<TextMesh> ().text = realName;
            if (nbShown < nbFileShown)
                nbShown++;
            else
                newObj.SetActive(false);
            files.Add(newObj);
		}
	}
	
    public void showNext()
    {
        if (!files[files.Count - 1].activeSelf)
        {
            files[firstIndex].SetActive(false);
            firstIndex++;
            for(int i = firstIndex; i < firstIndex + nbFileShown; i++)
            {
                files[i].transform.localPosition = new Vector3(x, y, z + i - firstIndex);
                files[i].SetActive(true);
            }
        }
    }

    public void showPrev()
    {
        if (firstIndex > 0)
        {
            files[firstIndex + nbFileShown - 1].SetActive(false);
            firstIndex--;
            for (int i = firstIndex; i < firstIndex + nbFileShown; i++)
            {
                files[i].transform.localPosition = new Vector3(x, y, z + i - firstIndex);
                files[i].SetActive(true);
            }
        }
    }

	// Update is called once per frame
	void Update () {
		
	}

	
}
