using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;


public class MusicFileScript : MonoBehaviour {

	public String fileName;
	private List<KeyValuePair<NoteScript, float>> partition;
	private List<Note> partitionNote;

	public GameObject[] notes;
	// private AudioClip[] audioClip;

	private int numberNote;
	private int noteIndex;
	private JsonTestClass.MusicalFile musicFile;

	private float currentTime;
    private float startTime;

	void Start () {

		GameObject gameState = GameObject.Find("GameState");
		GameState gs = gameState.GetComponent<GameState>();
		fileName = gs.fileName;

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

      //  Play();
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

	public bool partitionMatch(List<Note> toCompare){
		bool result = true;


		if (toCompare.Count != partition.Count) {
			result = false;
		}
		else{
				for(int i=0; i< toCompare.Count; i++){
				if ((partition[i].Key != toCompare[i].NoteToPlay) || (partition[i].Value != toCompare[i].nbFourth/4)){
						result = false;
						break;
					}
				}
			}
		return result;
	}

	public float MatchingPercentage(List<int> line){
		float nbCorrect = 0f;
		for (int i = 0; i < line.Count; i++) {
			if (line [i] == 3) {
				nbCorrect++;
			}
		}
		float result = 100 * (nbCorrect / ((float)line.Count));
		return result;
	}

	public List<int> MatchingLine(List<Note> toCompare){
		int nbNote = Mathf.Min (toCompare.Count, partition.Count);
		List<int> result = new List<int> ();
		for (int i = 0; i < nbNote; i++) {
			result.Add (CompareNote (partition [i], new KeyValuePair<NoteScript, float> (toCompare [i].NoteToPlay, (float)(toCompare [i].nbFourth) / 4)));
		}
		int nbMaxNote = Mathf.Max (toCompare.Count, partition.Count);
		for (int i = nbNote; i < nbMaxNote; i++) {
			result.Add (0);
		}
		return result;
	}

	public int CompareNote(KeyValuePair<NoteScript, float> a, KeyValuePair<NoteScript, float> b){
		if (!(a.Key) || !(b.Key)) {
			if (a.Key == b.Key) {
				if (a.Value == b.Value) {
					return 3;
				} else {
					return 1;
				}
			} else {
				if (a.Value == b.Value) {
					return 2;
				} else {
					return 0;
				}
			}


		} else {

			if (a.Key.getNoteName () == b.Key.getNoteName ()) {
				if (a.Value == b.Value) {
					return 3;
				} else {
					return 1;
				}
			} else {
				if (a.Value == b.Value) {
					return 2;
				} else {
					return 0;
				}
			
			}
		}
	}

	/*public List<int> PartitionTranspose(List<Note> toTranspose){
		int nbQuaterTot = 0;
		for (int i = 0; i < toTranspose.Count; i++) {
			nbQuaterTot += toTranspose [i].nbFourth;
		}

		List<int> transposed = new List<int> ();
		for (int j = 0; j< toTranspose.Count; j++){
			int nbQuater = toTranspose [j].nbFourth;
			for (int k = 0; k < nbQuater; k++) {
				transposed.Add (toTranspose [k].NoteToPlay.getNoteName ());
			}
		}
	} */
}
