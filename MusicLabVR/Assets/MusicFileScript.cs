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

	void Start () {
		/*audioClip = new AudioClip[84];
		audioClip [0] = (AudioClip)Resources.Load ("Piano1mp3/do1");
		audioClip [1] = (AudioClip)Resources.Load ("Piano1mp3/do#1");
		audioClip [2] = (AudioClip)Resources.Load ("Piano1mp3/re1");
		audioClip [3] = (AudioClip)Resources.Load ("Piano1mp3/re#1");
		audioClip [4] = (AudioClip)Resources.Load ("Piano1mp3/mi1");
		audioClip [5] = (AudioClip)Resources.Load ("Piano1mp3/fa1");
		audioClip [6] = (AudioClip)Resources.Load ("Piano1mp3/fa#1");
		audioClip [7] = (AudioClip)Resources.Load ("Piano1mp3/sol1");
		audioClip [8] = (AudioClip)Resources.Load ("Piano1mp3/sol#1");
		audioClip [9] = (AudioClip)Resources.Load ("Piano1mp3/la1");
		audioClip [10] = (AudioClip)Resources.Load ("Piano1mp3/la#1");
		audioClip [11] = (AudioClip)Resources.Load ("Piano1mp3/si1");


		audioClip [12] = (AudioClip)Resources.Load ("Piano2mp3/do2");
		audioClip [13] = (AudioClip)Resources.Load ("Piano2mp3/do#2");
		audioClip [14] = (AudioClip)Resources.Load ("Piano2mp3/re2");
		audioClip [15] = (AudioClip)Resources.Load ("Piano2mp3/re#2");
		audioClip [16] = (AudioClip)Resources.Load ("Piano2mp3/mi2");
		audioClip [17] = (AudioClip)Resources.Load ("Piano2mp3/fa2");
		audioClip [18] = (AudioClip)Resources.Load ("Piano2mp3/fa#2");
		audioClip [19] = (AudioClip)Resources.Load ("Piano2mp3/sol2");
		audioClip [20] = (AudioClip)Resources.Load ("Piano2mp3/sol#2");
		audioClip [21] = (AudioClip)Resources.Load ("Piano2mp3/la2");
		audioClip [22] = (AudioClip)Resources.Load ("Piano2mp3/la#2");
		audioClip [23] = (AudioClip)Resources.Load ("Piano2mp3/si2");


		audioClip [24] = (AudioClip)Resources.Load ("Piano3mp3/do3");
		audioClip [25] = (AudioClip)Resources.Load ("Piano3mp3/do#3");
		audioClip [26] = (AudioClip)Resources.Load ("Piano3mp3/re3");
		audioClip [27] = (AudioClip)Resources.Load ("Piano3mp3/re#3");
		audioClip [28] = (AudioClip)Resources.Load ("Piano3mp3/mi3");
		audioClip [29] = (AudioClip)Resources.Load ("Piano3mp3/fa3");
		audioClip [30] = (AudioClip)Resources.Load ("Piano3mp3/fa#3");
		audioClip [31] = (AudioClip)Resources.Load ("Piano3mp3/sol3");
		audioClip [32] = (AudioClip)Resources.Load ("Piano3mp3/sol#3");
		audioClip [33] = (AudioClip)Resources.Load ("Piano3mp3/la3");
		audioClip [34] = (AudioClip)Resources.Load ("Piano3mp3/la#3");
		audioClip [35] = (AudioClip)Resources.Load ("Piano3mp3/si3");


		audioClip [36] = (AudioClip)Resources.Load ("Piano4mp3/do4");
		audioClip [37] = (AudioClip)Resources.Load ("Piano4mp3/do#4");
		audioClip [38] = (AudioClip)Resources.Load ("Piano4mp3/re4");
		audioClip [39] = (AudioClip)Resources.Load ("Piano4mp3/re#4");
		audioClip [40] = (AudioClip)Resources.Load ("Piano4mp3/mi4");
		audioClip [41] = (AudioClip)Resources.Load ("Piano4mp3/fa4");
		audioClip [42] = (AudioClip)Resources.Load ("Piano4mp3/fa#4");
		audioClip [43] = (AudioClip)Resources.Load ("Piano4mp3/sol4");
		audioClip [44] = (AudioClip)Resources.Load ("Piano4mp3/sol#4");
		audioClip [45] = (AudioClip)Resources.Load ("Piano4mp3/la4");
		audioClip [46] = (AudioClip)Resources.Load ("Piano4mp3/la#4");
		audioClip [47] = (AudioClip)Resources.Load ("Piano4mp3/si4");


		audioClip [48] = (AudioClip)Resources.Load ("Piano5mp3/do5");
		audioClip [49] = (AudioClip)Resources.Load ("Piano5mp3/do#5");
		audioClip [50] = (AudioClip)Resources.Load ("Piano5mp3/re5");
		audioClip [51] = (AudioClip)Resources.Load ("Piano5mp3/re#5");
		audioClip [52] = (AudioClip)Resources.Load ("Piano5mp3/mi5");
		audioClip [53] = (AudioClip)Resources.Load ("Piano5mp3/fa5");
		audioClip [54] = (AudioClip)Resources.Load ("Piano5mp3/fa#5");
		audioClip [55] = (AudioClip)Resources.Load ("Piano5mp3/sol5");
		audioClip [56] = (AudioClip)Resources.Load ("Piano5mp3/sol#5");
		audioClip [57] = (AudioClip)Resources.Load ("Piano5mp3/la5");
		audioClip [58] = (AudioClip)Resources.Load ("Piano5mp3/la#5");
		audioClip [59] = (AudioClip)Resources.Load ("Piano5mp3/si5");


		audioClip [60] = (AudioClip)Resources.Load ("Piano6mp3/do6");
		audioClip [61] = (AudioClip)Resources.Load ("Piano6mp3/do#6");
		audioClip [62] = (AudioClip)Resources.Load ("Piano6mp3/re6");
		audioClip [63] = (AudioClip)Resources.Load ("Piano6mp3/re#6");
		audioClip [64] = (AudioClip)Resources.Load ("Piano6mp3/mi6");
		audioClip [65] = (AudioClip)Resources.Load ("Piano6mp3/fa6");
		audioClip [66] = (AudioClip)Resources.Load ("Piano6mp3/fa#6");
		audioClip [67] = (AudioClip)Resources.Load ("Piano6mp3/sol6");
		audioClip [68] = (AudioClip)Resources.Load ("Piano6mp3/sol#6");
		audioClip [69] = (AudioClip)Resources.Load ("Piano6mp3/la6");
		audioClip [70] = (AudioClip)Resources.Load ("Piano6mp3/la#6");
		audioClip [71] = (AudioClip)Resources.Load ("Piano6mp3/si6");


		audioClip [72] = (AudioClip)Resources.Load ("Piano7mp3/do7");
		audioClip [73] = (AudioClip)Resources.Load ("Piano7mp3/do#7");
		audioClip [74] = (AudioClip)Resources.Load ("Piano7mp3/re7");
		audioClip [75] = (AudioClip)Resources.Load ("Piano7mp3/re#7");
		audioClip [76] = (AudioClip)Resources.Load ("Piano7mp3/mi7");
		audioClip [77] = (AudioClip)Resources.Load ("Piano7mp3/fa7");
		audioClip [78] = (AudioClip)Resources.Load ("Piano7mp3/fa#7");
		audioClip [79] = (AudioClip)Resources.Load ("Piano7mp3/sol7");
		audioClip [80] = (AudioClip)Resources.Load ("Piano7mp3/sol#7");
		audioClip [81] = (AudioClip)Resources.Load ("Piano7mp3/la7");
		audioClip [82] = (AudioClip)Resources.Load ("Piano7mp3/la#7");
		audioClip [83] = (AudioClip)Resources.Load ("Piano7mp3/si7");*/

		// audioSource = this.gameObject.GetComponent<AudioSource> ();

		String newPath =  Path.Combine("StreamingAssets", fileName); // Get Path to file in resources folder without .json !
		String savedString = JsonTestClass.LoadJSONFromFile(newPath); // Load Json from file

		musicFile = new JsonTestClass.MusicalFile ();
		musicFile = JsonTestClass.JSONToObject(savedString) ;  // Create Object from Json
		numberNote = musicFile.numberNote;
		noteIndex = 0;

		StartCoroutine(playMusic()); // Launch the action function of this instance

	}

	private IEnumerator playMusic()
	{
		if (noteIndex < numberNote) {
			int clipIndex = musicFile.musicalNote [noteIndex].noteName + 12*musicFile.musicalNote [noteIndex].noteOctave;
			notes [clipIndex].GetComponent<NoteScript> ().Play ();
			notes [clipIndex].GetComponent<NoteScript> ().GetComponent<AudioSource>().SetScheduledEndTime(AudioSettings.dspTime + (double) (musicFile.musicalNote [noteIndex].noteTempo));
			//audioSource.PlayOneShot (audioClip [clipIndex]);
			Debug.Log (clipIndex);
			yield return new WaitForSeconds (musicFile.musicalNote [noteIndex].noteTempo);
			noteIndex++;

			// Repeat this routine again
			StartCoroutine(playMusic());
		}
			

	}






}
