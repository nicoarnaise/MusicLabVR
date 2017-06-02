using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;


public class MusicFileScript : MonoBehaviour {

	public String fileName;
	public GameObject[] notes;
	// private AudioClip[] audioClip;

	private int numberNote;
	private int noteIndex;
	private JsonTestClass.MusicalFile musicFile;

	private float currentTime;
    private float startTime;

	void Start () {
        Play();
	}

    public void Play()
    {
        startTime = Time.time;
        String newPath = Path.Combine("StreamingAssets", fileName); // Get Path to file in resources folder without .json !
        String savedString = JsonTestClass.LoadJSONFromFile(newPath); // Load Json from file

        musicFile = new JsonTestClass.MusicalFile();
        musicFile = JsonTestClass.JSONToObject(savedString);  // Create Object from Json

        numberNote = musicFile.numberNote;
        noteIndex = 0;
        currentTime = 0f;

        StartCoroutine(playMusic()); // Launch the action function of this instance
    }

	public IEnumerator playMusic()
	{
        if (noteIndex < numberNote)
        {
            int clipIndex = musicFile.musicalNote[noteIndex].noteName + 12 * musicFile.musicalNote[noteIndex].noteOctave;
            float clipDuration = musicFile.musicalNote[noteIndex].noteTempo;
            if (clipIndex < 84)
            {
                notes[clipIndex].GetComponent<NoteScript>().PlayT(1.2f * clipDuration);
                //notes[clipIndex].GetComponent<NoteScript>().GetComponent<AudioSource>().SetScheduledEndTime(AudioSettings.dspTime + 1.2 * (clipDuration));
                //audioSource.PlayOneShot (audioClip [clipIndex]);
            }
            
            float errorTime = Time.time - startTime - currentTime;

            currentTime = clipDuration + currentTime;
            
            yield return new WaitForSeconds(clipDuration - errorTime);
            noteIndex++;

            // Repeat this routine again
            StartCoroutine(playMusic());
        }
	}
}
