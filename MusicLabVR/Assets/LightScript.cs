using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour {

    private Light l;

	// Use this for initialization
	void Start () {
        l = transform.GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.parent.localScale.x * 10 != l.range)
        {
            l.range = transform.parent.localScale.x * 10;
        }
	}
}
