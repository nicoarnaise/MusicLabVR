using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBtnScript : MonoBehaviour {

    public MusicFileScript[] MFS;
    public bool doAct = false;

	private TimeLineScript timeLineScript;

	// Use this for initialization
	void Start () {
		timeLineScript = transform.parent.parent.GetComponentInChildren<TimeLineScript> ();
	}
	
	// Update is called once per frame
	void Update () {
        if (doAct)
        {
            doAct = false;
            DoAction();
        }
	}

    public void DoAction()
    {
        if (gameObject.name.Equals("playSample"))
            foreach (MusicFileScript mf in MFS){
                mf.Play();
            }
        if (gameObject.name.Equals("stop"))
        {
            foreach (MusicFileScript mf in MFS)
            {
                mf.StopAllCoroutines();
            }
			timeLineScript.StopAllCoroutines();
        }
        if (gameObject.name.Equals("rest"))
			timeLineScript.AddRest();
		if (gameObject.name.Equals ("play")) {
			timeLineScript.Play ();
			if (MFS [0].partitionMatch (timeLineScript.partition)) {
				Debug.Log ("Level Complete !");
			} else {
				Debug.Log ("Your composition does not match yet");
			}
		}
    }
}
