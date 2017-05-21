using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsScript : MonoBehaviour {

    public float scale;
	
	// Update is called once per frame
	void Update () {
        if (scale <= 0) scale = 1;
		if(!transform.parent.localScale.Equals(new Vector3(scale,scale, scale)))
        {
            transform.parent.localScale = new Vector3(scale, scale, scale);
        }
	}
}
