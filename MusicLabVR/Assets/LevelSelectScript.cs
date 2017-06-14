using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelSelectScript : MonoBehaviour {

	public int level;
	public float succeedRate = 80f;
	public string fileName;

	private GameObject gameState;
	private GameState gs;

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
				SceneManager.LoadScene ("level1");
			} else {
				if (fileName == "files") {
                    gs.fileName = "";
					SceneManager.LoadScene ("Files");
				} else if(fileName == "sandbox") {
                    gs.fileName = "";
					if (gs.tutoInc == 13) {
						gs.audioSource.Stop;
						gs.audioSource.PlayOneShot (gs.audioClip [13]);
						gs.tutoInc = 14;
					}
                    SceneManager.LoadScene("SandBox");
                } else {   
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

