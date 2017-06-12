using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelSelectScript : MonoBehaviour {

	public int level;
	public float succeedRate = 80f;

	private GameObject gameState;
	private GameState gs;

		public void EnterLevel (){
		if (level == 10) {
			Quit ();
		} else {
			gs.neededPercentage = succeedRate;
			SceneManager.LoadScene (level);
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

