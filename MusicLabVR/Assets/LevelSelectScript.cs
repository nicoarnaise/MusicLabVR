using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelSelectScript : MonoBehaviour {

	public int level;
	public float succeedRate = 80f;
	public string fileName;
    public bool doAct = false;

	private GameObject gameState;
	private GameState gs;

    void Start()
    {
        gameState = GameObject.Find("GameState");
        gs = gameState.GetComponent<GameState>();
    }

    void Update()
    {
        if (doAct)
        {
            doAct = false;
            EnterLevel();
        }
    }

	public void EnterLevel (){
		if (level == 0) {
			Quit ();
		} else {
			gs.neededPercentage = succeedRate;
			gs.fileName = fileName;
			if (fileName == "level0") {
				if (gs.tutoInc == 1) {
					gs.audioSource.Stop ();
					gs.audioSource.PlayOneShot (gs.audioClip [1]);
					gs.tutoInc = 2;
				}
                gs.waitForSceneLoad = true;
				SceneManager.LoadScene ("level1");
			} else {
				if (fileName == "files") {
                    gs.fileName = "";
                    gs.waitForSceneLoad = true;
					SceneManager.LoadScene ("Files");
				} else if(fileName == "sandbox") {
                    gs.fileName = "";
					if (gs.tutoInc == 13) {
						gs.audioSource.Stop();
						gs.audioSource.PlayOneShot (gs.audioClip [13]);
						gs.tutoInc = 14;
					}
                    gs.waitForSceneLoad = true;
                    SceneManager.LoadScene("SandBox");
                } else {
                    gs.waitForSceneLoad = true;
					SceneManager.LoadScene ("level2");
				}
			}
		}
	}

	public void Quit()
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit ();
		#endif
	}

}

