using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;


public class MusicFileScript : MonoBehaviour {

	public String fileName;
	private List<KeyValuePair<NoteScript, float>> partition;

	public GameObject[] notes;
	// private AudioClip[] audioClip;

	private int numberNote;
	private int noteIndex;
	private JsonTestClass.MusicalFile musicFile;

	private float currentTime;
    private float startTime;

	void Start () {
		
		String newPath = Path.Combine("StreamingAssets", fileName); // Get Path to file in resources folder without .json !
		String savedString = JsonTestClass.LoadJSONFromFile(newPath); // Load Json from file

		musicFile = new JsonTestClass.MusicalFile();
		musicFile = JsonTestClass.JSONToObject(savedString);  // Create Object from Json

		numberNote = musicFile.numberNote;

		partition = new List<KeyValuePair<NoteScript, float>>();
		for (int i = 0; i < numberNote; i++) {

			int clipIndex = musicFile.musicalNote[i].noteName + 12 * musicFile.musicalNote[i].noteOctave;
			float clipDuration = musicFile.musicalNote[i].noteTempo;
			if (clipIndex < 84) {
				partition.Add (new KeyValuePair<NoteScript, float> (notes [clipIndex].GetComponent<NoteScript> (), clipDuration));

			} else {
				partition.Add (new KeyValuePair<NoteScript, float> (null, clipDuration));

			}

		}
        Play();
	}

    public void Play()
    {

		startTime = Time.time;
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

	public bool partitionMatch(List<KeyValuePair<NoteScript, float>> toCompare){
		bool result = true;
		if (toCompare.Count != partition.Count) {
			result = false;
		}
		else{
				for(int i=0; i< toCompare.Count; i++){
				if ((partition[i].Key != toCompare[i].Key) || (partition[i].Value != toCompare[i].Value)){
						result = false;
						break;
					}
				}
			}
		return result;
	}
}
