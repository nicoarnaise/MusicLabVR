using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBtnScript : MonoBehaviour {

    public MusicFileScript[] MFS;
    public bool doAct = false;

	// Use this for initialization
	void Start () {
		
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
            transform.parent.parent.GetComponentInChildren<TimeLineScript>().StopAllCoroutines();
        }
        if (gameObject.name.Equals("rest"))
            transform.parent.parent.GetComponentInChildren<TimeLineScript>().AddRest();
        if (gameObject.name.Equals("play"))
            transform.parent.parent.GetComponentInChildren<TimeLineScript>().Play();
    }
}
