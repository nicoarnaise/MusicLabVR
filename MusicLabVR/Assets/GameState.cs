using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using System.IO;

public class GameState : MonoBehaviour {

	// On a besoin d'une variable globalState pour qu'une seule instance de la classe soit creee,
	// lors du Awake()
	public static GameState globalState;


	// Variables globales du jeu.
	public float[] levelPercentage;
	public float neededPercentage;

	void Awake() {
		// Utilisation du design Pattern Singleton, si c'est la première fois que ce script est appele, il est cree, 
		// sinon, il est detruit car il y en a deja un
		if (globalState == null) {
			DontDestroyOnLoad (transform.gameObject);
			globalState = this;
		} else if (globalState != this) {
			Destroy (gameObject);
		}
	}


	void Update(){
	}
}