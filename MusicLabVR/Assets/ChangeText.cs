using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeText : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void change(){
		this.GetComponent<TextMesh> ().text = this.transform.parent.GetComponent<MusicFileScript> ().fileName;
	}
}
