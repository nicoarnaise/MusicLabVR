using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLineScript : MonoBehaviour {

    public GameObject NoteTimeLineRef;
    public int nbFourthMax = 48;
    public float spaceX = 0.2f;
    public float startX = -4.8f;
    public int nbLine = 2;
    public float Ypos = 0.003f;

    public bool doAct = false;
    public LengthBtnScript selected;

    private int nbFourth;
    private float Z1;
    private float Z2;

    private float startTime;
    private float currentTime;
    private int noteIndex;

    private List<KeyValuePair<NoteScript, float>> partition;

	// Use this for initialization
	void Start () {
        nbFourth = 0;
        if (nbLine == 2)
        {
            Z1 = 2.4f;
            Z2 = -2.4f;
        }
        else
        {
            Z1 = 0;
            Z2 = 0;
        }
        partition = new List<KeyValuePair<NoteScript, float>>();
    }

    public void AddNote(NoteScript Note, int Octave)
    {
        if (selected)
        {
            GameObject newNote = Instantiate(NoteTimeLineRef, transform, false);
            newNote.transform.localPosition = new Vector3(startX + spaceX * (nbFourth + (selected.value * 2) - 1), Ypos,Z1);
            newNote.transform.localScale = new Vector3(newNote.transform.localScale.x * (selected.value * 4), newNote.transform.localScale.y, newNote.transform.localScale.z);
            newNote.GetComponentInChildren<TextMesh>().text = "" + Octave;
            newNote.GetComponent<Renderer>().material = Note.idleMaterial;
            nbFourth += (int)(selected.value * 4);
            partition.Add(new KeyValuePair<NoteScript, float>(Note, selected.value));
        }   
    }

    public void AddRest()
    {
        if (selected)
        {
            GameObject newNote = Instantiate(NoteTimeLineRef, transform, false);
            newNote.transform.localPosition = new Vector3(startX + spaceX * (nbFourth + (selected.value * 2) - 1), Ypos, Z1);
            newNote.transform.localScale = new Vector3(newNote.transform.localScale.x * (selected.value * 4), newNote.transform.localScale.y, newNote.transform.localScale.z);
            newNote.GetComponentInChildren<TextMesh>().text = "";
            nbFourth += (int)(selected.value * 4);
            partition.Add(new KeyValuePair<NoteScript, float>(null, selected.value));
        }
    }

    public void Play()
    {
        startTime = Time.time;
        currentTime = 0;
        noteIndex = 0;

        StartCoroutine(playMusic());
    }

    public IEnumerator playMusic()
    {
        if (noteIndex < partition.Count)
        {
            float clipDuration = partition[noteIndex].Value;

            if(partition[noteIndex].Key)
                partition[noteIndex].Key.PlayT(1.2f * clipDuration);

            float errorTime = Time.time - startTime - currentTime;

            currentTime = clipDuration + currentTime;

            yield return new WaitForSeconds(clipDuration - errorTime);
            noteIndex++;

            // Repeat this routine again
            StartCoroutine(playMusic());
        }
    }
}
