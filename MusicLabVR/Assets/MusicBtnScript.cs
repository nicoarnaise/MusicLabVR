using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicBtnScript : MonoBehaviour {

    public MusicFileScript[] MFS;
	public GameObject prefabWin;
    public bool doAct = false;

	private TimeLineScript timeLineScript;

	public GameObject gameState;
	private GameState gs;

	// Use this for initialization
	void Start () {
		gameState = GameObject.Find("GameState");
		gs = gameState.GetComponent<GameState>();
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
		timeLineScript = GameObject.Find("EditMenu").GetComponent<TimeLineScript> ();

        if (gameObject.name.Equals ("playSample")) {
			if (MFS.Length == 0) {
				timeLineScript.Save ();
                Instantiate(prefabWin, transform.parent.parent.parent, true);
            } else {
				if (gs.tutoInc == 7) {
					gs.audioSource.Stop();
					gs.audioSource.PlayOneShot (gs.audioClip [7]);
					gs.tutoInc = 8;
				}
				foreach (MusicFileScript mf in MFS) {
					mf.Play ();
				}
			}
		}
        if (gameObject.name.Equals("stop"))
        {
			if (gs.tutoInc == 8) {
				gs.audioSource.Stop();
				gs.audioSource.PlayOneShot (gs.audioClip [8]);
				gs.tutoInc = 9;
			}
            foreach (MusicFileScript mf in MFS)
            {
                mf.StopAllCoroutines();
            }
			timeLineScript.StopAllCoroutines();
        }
		if (gameObject.name.Equals ("reset")) {
			if (gs.tutoInc ==9) {
				gs.audioSource.Stop();
				gs.audioSource.PlayOneShot (gs.audioClip [9]);
				gs.tutoInc = 10;
			}
			timeLineScript.Reset ();
		}

        if(gameObject.name.Equals("rest"))
            timeLineScript.AddRest();

		if (gameObject.name.Equals ("play")) {

			if (gs.tutoInc == 6) {
				gs.audioSource.Stop();
				gs.audioSource.PlayOneShot (gs.audioClip [6]);
				gs.tutoInc = 7;
			}

			timeLineScript.Play ();
			if (MFS.Length != 0) {
                List<int> results = MFS[0].MatchingLine(timeLineScript.partition);
                int maxPartition = timeLineScript.partition.Count;
                for(int index = 0; index < maxPartition; index ++)
                {
                    timeLineScript.setCorrection(timeLineScript.partition[index], results[index]);
                }
				if (MFS [0].MatchingPercentage (MFS [0].MatchingLine (timeLineScript.partition)) >= gs.neededPercentage) {
					if (gs.tutoInc == 11) {
						gs.audioSource.Stop();
						gs.audioSource.PlayOneShot (gs.audioClip [11]);
						gs.tutoInc = 12;
					}
					Instantiate (prefabWin, transform.parent.parent.parent, true);
					Debug.Log ("Level Complete !");
				} else {
					Debug.Log ("Your composition does not match yet");
				}
			}
		}

        if (gameObject.name.Contains ("WinObject")) {
			if (gs.tutoInc == 12) {
				gs.audioSource.Stop();
				gs.audioSource.PlayOneShot (gs.audioClip [12]);
				gs.tutoInc = 13;
			}
            SceneManager.LoadScene(0);
		}
			
    }
}
