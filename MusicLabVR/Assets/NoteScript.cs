using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteScript : MonoBehaviour {

    public Material idleMaterial;
    public Material playMaterial;
    public bool play = false;

    private AudioSource audioS;
    private Renderer rend;

	// Use this for initialization
	void Start () {
        audioS = transform.GetComponent<AudioSource>();
        rend = transform.GetComponent<Renderer>();
	}

    void Update()
    {
        if (play)
        {
            Play();
            play = false;
        }
        if (!audioS.isPlaying)
        {
            rend.material = idleMaterial;
        }
        else
        {
            rend.material = playMaterial;
        }
    }

    public void Play(){
        audioS.Play();
    }


}
