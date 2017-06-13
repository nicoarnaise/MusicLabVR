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
			if (fileName == "level1") {
				SceneManager.LoadScene ("level1");
			} else {
				SceneManager.LoadScene ("level2");
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

		public void Awake(){
			gameState = GameObject.Find("GameState");
			gs = gameState.GetComponent<GameState>();
		if (gs.levelPercentage[level] >= succeedRate ){
			this.gameObject.GetComponent<MeshRenderer> ().material.color = Color.green;
			this.gameObject.GetComponent<Image> ().color = Color.green;
			}
		}
}

