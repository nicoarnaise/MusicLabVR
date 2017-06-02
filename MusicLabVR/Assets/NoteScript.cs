using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteScript : MonoBehaviour {

    public Material idleMaterial;
    public Material playMaterial;
    public bool play = false;

    private int Octave;
    private TimeLineScript TimeLine;
    private AudioSource audioS;
    private Renderer rend;

	// Use this for initialization
	void Start () {
        audioS = transform.GetComponent<AudioSource>();
        rend = transform.GetComponent<Renderer>();
        TimeLine = transform.parent.parent.GetComponent<OctaveScript>().TimeLine;
        Octave = transform.parent.parent.GetComponent<OctaveScript>().Octave;
    }

    void Update()
    {
        if (play)
        {
            play = false;
            Play();
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
        TimeLine.AddNote(this, Octave);
    }

    // play the note for time seconds
    public void PlayT(float time)
    {
        audioS.Play();
        Invoke("Stop", time);
    }

    private void Stop()
    {
        audioS.Stop();
    }
}
