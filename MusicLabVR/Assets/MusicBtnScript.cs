using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBtnScript : MonoBehaviour {

    public MusicFileScript[] MFS;
	public GameObject prefabWin;
    public bool doAct = false;

	private TimeLineScript timeLineScript;

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
		timeLineScript = transform.parent.parent.GetComponentInChildren<TimeLineScript> ();

		if (gameObject.name.Equals ("playSample")) {
			if (MFS.Length == 0) {
				timeLineScript.Save ();
			} else {
				foreach (MusicFileScript mf in MFS) {
					mf.Play ();
				}
			}
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
			GameObject gameState = GameObject.Find("GameState");
			GameState gs = gameState.GetComponent<GameState>();
			timeLineScript.Play ();
			if (MFS.Length != 0) {
				if (MFS [0].MatchingPercentage (MFS [0].MatchingLine (timeLineScript.partition)) >= gs.neededPercentage) {
					Instantiate (prefabWin, transform.parent.parent.parent, true);
					Debug.Log ("Level Complete !");
				} else {
					Debug.Log ("Your composition does not match yet");
				}
			}
		}
		if (gameObject.name.Equals ("WinObject")) {
			//TODO
		}
			
    }
}
