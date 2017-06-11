using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelSelectScript : MonoBehaviour {

	public int level;
	public float succeedRate = 80f;
	public GameObject gameState;

		public void EnterLevel (){
		if (level == 10) {
			Quit ();
		} else {
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
			GameState gs = gameState.GetComponent<GameState>();
		if (gs.levelSucceed[level] >= succeedRate ){
			this.gameObject.GetComponent<MeshRenderer> ().material.color = Color.green;
			this.gameObject.GetComponent<Image> ().color = Color.green;
			}
		}
}

