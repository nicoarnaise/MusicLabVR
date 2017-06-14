using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class TimeLineScript : MonoBehaviour {

    public GameObject NoteTimeLineRef;
    public int nbFourthMax = 48;
    public float spaceX = 0.2f;
    public float startX = -4.8f;
    public float Ypos = 0.003f;

    public Material True;
    public Material False;
    public Material Error;
    public Material NoRes;

    public bool doAct = false;
    public LengthBtnScript selected;
    public int pageToShow;

    public int nbFourth;
    private float Z1;
    private float Z2;
    private int nbLine = 2;

    private float startTime;
    private float currentTime;
    private int noteIndex;
    private int pageShown;

    public int indexToInsert;
    
	public GameObject gameState;
	private GameState gs;

    // list Note and their duration
    public List<Note> partition;

    // list NoteTimeLine

	// Use this for initialization
	void Start () {
		GameObject gameState = GameObject.Find("GameState");
		GameState gs = gameState.GetComponent<GameState>();

        nbFourth = 0;
        indexToInsert = -1;
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
        partition = new List<Note>();
        // creation of the first page
        pageToShow = 0;
        pageShown = 0;
    }

    void Update() {
        if (pageToShow > pageShown && !partition[partition.Count - 1].NoteTimeLine.activeSelf || pageToShow < pageShown && pageToShow > -1)
            showPage(pageToShow);
        else
            pageToShow = pageShown;
    }

    private void Refresh()
    {
        int index = 0;
        foreach (Note note in partition)
        {
            note.setIndexInPartition(index);
            index++;
            note.corr.GetComponent<Renderer>().material = NoRes;
            note.corr.GetComponentInChildren<TextMesh>().text = "";
            if (note.NoteTimeLine.activeSelf)
            {
                note.NoteTimeLine.SetActive(false);
                if(note.corr)
                    note.corr.SetActive(false);
            }
        }
		if (partition.Count > 0) {
			index = 0;
			for (int page = 0; page <= pageShown; page++) {
				nbFourth = 0;
				Note note = partition [index];
				while (nbFourth + note.nbFourth < nbFourthMax) {
					if (page == pageShown) {
						note.NoteTimeLine.transform.localPosition = new Vector3 (startX + spaceX * (nbFourth + note.nbFourth / 2 - 1), Ypos, Z1);
                        note.NoteTimeLine.SetActive(true);
                        if (note.corr)
                        {
                            note.corr.transform.localPosition = new Vector3(note.NoteTimeLine.transform.localPosition.x, Ypos, Z2);
                            note.corr.SetActive(true);
                        }
                    }
					note.page = page;
					nbFourth += note.nbFourth;
					index++;
					if (index < partition.Count)
						note = partition [index];
					else
						break;
				}
				if (index >= partition.Count)
					break;
			}
            if(nbFourth == 0)
            {
                showPage(pageShown - 1);
            }
		} else {
			nbFourth = 0;
		}
    }

    public void setCorrection(Note note, int v)
    {
        note.corr.GetComponentInChildren<TextMesh>().text = ""+v;
        switch (v)
        {
            case 0:
                note.corr.GetComponent<Renderer>().material = False;
                break;
            case 3:
                note.corr.GetComponent<Renderer>().material = True;
                break;
            default:
                note.corr.GetComponent<Renderer>().material = Error;
                break;
        }
    }

    private void showPage(int pToShow)
    {
        if(pToShow < pageShown && pToShow > -1 || pToShow > pageShown && !partition[partition.Count-1].NoteTimeLine.activeSelf)
        {
            if (GetComponentInChildren<MenuPrefScript>())
                Destroy(GetComponentInChildren<MenuPrefScript>().transform.parent.gameObject);

            pageToShow = pToShow;
            int index = 0;
            foreach (Note note in partition)
            {
                note.setIndexInPartition(index);
                index++;
                if (note.NoteTimeLine.activeSelf)
                {
                    note.NoteTimeLine.SetActive(false);
                    if(note.corr)
                        note.corr.SetActive(false);
                }
            }

            pageShown = pageToShow;
            index = 0;
            for (int page = 0; page <= pageShown; page++)
            {
                nbFourth = 0;
                Note note = partition[index];
                while (nbFourth + note.nbFourth < nbFourthMax)
                {
                    if (page == pageShown)
                    {
                        note.NoteTimeLine.transform.localPosition = new Vector3(startX + spaceX * (nbFourth + note.nbFourth / 2 - 1), Ypos, Z1);
                        note.NoteTimeLine.SetActive(true);
                        if (note.corr)
                        {
                            note.corr.transform.localPosition = new Vector3(note.NoteTimeLine.transform.localPosition.x, Ypos, Z2);
                            note.corr.SetActive(true);
                        }
                    }
                    note.page = page;
                    nbFourth += note.nbFourth;
                    index++;
                    if(index < partition.Count)
                        note = partition[index];
                    else
                        break;
                }
                if (index >= partition.Count && page < pageShown)
                {
                    nbFourth = 0;
                    pageShown = page;
                    index = partition.Count - 1;
                    while (partition[index].page == pageShown)
                        index--;
                    index++;
                    while (index < partition.Count)
                    {
                        note = partition[index];
                        note.NoteTimeLine.transform.localPosition = new Vector3(startX + spaceX * (nbFourth + note.nbFourth / 2 - 1), Ypos, Z1);
                        note.NoteTimeLine.SetActive(true);
                        if (note.corr)
                        {
                            note.corr.transform.localPosition = new Vector3(note.NoteTimeLine.transform.localPosition.x, Ypos, Z2);
                            note.corr.SetActive(true);
                        }
                        nbFourth += note.nbFourth;
                        index++;
                    }
                    break;
                }
            }
        }
    }

    public void AddNote(NoteScript noteS, int Octave, string addon)
    {
        if (selected)
        {
			if (gs.tutoInc == 3) {
				gs.audioSource.Stop;
				gs.audioSource.PlayOneShot (gs.audioClip [3]);
				gs.tutoInc = 4;
			}
            // show the last page (here by giving the partition length, we know it would be over the number of pages)
            if(indexToInsert == -1)
                showPage(partition.Count);

            GameObject newNoteGO = Instantiate(NoteTimeLineRef, transform, false);
            newNoteGO.transform.localPosition = new Vector3(startX + spaceX * (nbFourth + (selected.value * 2) - 1), Ypos,Z1);
            newNoteGO.transform.localScale = new Vector3(newNoteGO.transform.localScale.x * (selected.value * 4), newNoteGO.transform.localScale.y, newNoteGO.transform.localScale.z);
            if(addon != "")
                newNoteGO.GetComponentInChildren<TextMesh>().text = "" + Octave + "\n" + addon;
            else
                newNoteGO.GetComponentInChildren<TextMesh>().text = "" + Octave;
            newNoteGO.GetComponent<Renderer>().material = noteS.idleMaterial;

            Note newNote = new Note(newNoteGO, noteS, pageShown, partition.Count, (int)(selected.value * 4));

            if (Error)
            {
                GameObject corr = Instantiate(NoteTimeLineRef, transform.FindChild("resultsContainer"), false);
                corr.transform.localPosition = new Vector3(newNote.NoteTimeLine.transform.localPosition.x, Ypos, Z2);
                corr.transform.localScale = newNote.NoteTimeLine.transform.localScale;
                corr.GetComponent<Renderer>().material = NoRes;
                corr.GetComponentInChildren<TextMesh>().text = "";
                newNote.corr = corr;
            }
			if (indexToInsert > -1) {
				AddAt (newNote, indexToInsert);
				if (gs.tutoInc == 4) {
					gs.audioSource.Stop;
					gs.audioSource.PlayOneShot (gs.audioClip [4]);
					gs.tutoInc = 5;
				}
			}
				
            else
            {
                partition.Add(newNote);
                if (nbFourth + selected.value * 4 > nbFourthMax)
                {
                    newNote.NoteTimeLine.SetActive(false);
                    showPage(pageShown + 1);
                }
                else
                {
                    nbFourth += newNote.nbFourth;
                }
            }
        }   
    }

    public void AddRest()
    {
        if (selected)
        {

			if (gs.tutoInc == 10) {
				gs.audioSource.Stop;
				gs.audioSource.PlayOneShot (gs.audioClip [10]);
				gs.tutoInc = 11;
			}
            // show the last page (here by giving the partition length, we know it would be over the number of pages)
            if(indexToInsert==-1)
                showPage(partition.Count);

            GameObject newNoteGO = Instantiate(NoteTimeLineRef, transform, false);
            newNoteGO.transform.localPosition = new Vector3(startX + spaceX * (nbFourth + (selected.value * 2) - 1), Ypos, Z1);
            newNoteGO.transform.localScale = new Vector3(newNoteGO.transform.localScale.x * (selected.value * 4), newNoteGO.transform.localScale.y, newNoteGO.transform.localScale.z);
            newNoteGO.GetComponentInChildren<TextMesh>().text = "";
           
            Note newNote = new Note(newNoteGO, null, pageShown, partition.Count - 1, (int)(selected.value * 4));

            if (Error)
            {
                GameObject corr = Instantiate(NoteTimeLineRef, transform.FindChild("resultsContainer"), false);
                corr.transform.localPosition = new Vector3(newNote.NoteTimeLine.transform.localPosition.x, Ypos, Z2);
                corr.transform.localScale = newNote.NoteTimeLine.transform.localScale;
                corr.GetComponent<Renderer>().material = NoRes;
                corr.GetComponentInChildren<TextMesh>().text = "";
                newNote.corr = corr;
            }
            if (indexToInsert > -1)
                AddAt(newNote, indexToInsert);
            else
            {
                partition.Add(newNote);
                if (nbFourth + selected.value * 4 > nbFourthMax)
                {
                    newNote.NoteTimeLine.SetActive(false);
                    showPage(pageShown + 1);
                }
                else
                {
                    nbFourth += newNote.nbFourth;
                }
            }
        }
    }

    public void AddAt(Note note, int indexInPartition)
    {
        partition.Insert(indexInPartition, note);
        indexToInsert = -1;
        // refresh the page
        Refresh();
    }

    public void Play()
    {
        startTime = Time.time;
        currentTime = 0;
        noteIndex = 0;

        StartCoroutine(playMusic());
    }

    public void Reset()
    {
        foreach(Note note in partition)
        {
            Destroy(note.NoteTimeLine);
            Destroy(note.corr);
        }
        partition.Clear();
        Refresh();
    }

    public IEnumerator playMusic()
    {
        if (noteIndex < partition.Count)
        {
            float clipDuration = partition[noteIndex].nbFourth * 0.25f;

            if(partition[noteIndex].NoteToPlay)
                partition[noteIndex].NoteToPlay.PlayT(1.2f * clipDuration);

            float errorTime = Time.time - startTime - currentTime;

            currentTime = clipDuration + currentTime;

            yield return new WaitForSeconds(clipDuration - errorTime);
            noteIndex++;

            // Repeat this routine again
            StartCoroutine(playMusic());
        }
    }
		

	public void Save()
	{

		JsonTestClass.MusicalFile musicFile = new JsonTestClass.MusicalFile();
		musicFile.numberNote = partition.Count;
		musicFile.musicalNote = new JsonTestClass.MusicalNote[partition.Count];


		for (int i = 0; i < partition.Count; i++) {
			if (partition [i].NoteToPlay) {
				musicFile.musicalNote[i] = new JsonTestClass.MusicalNote (partition [i].NoteToPlay.getNoteName (), partition [i].NoteToPlay.Octave - 1, partition [i].nbFourth * 0.25f);
			} else {
				musicFile.musicalNote[i] = new JsonTestClass.MusicalNote (0, 10, partition [i].nbFourth * 0.25f);
			}
		}



		String ressourcePath = Path.Combine (Application.dataPath, "Resources"); // Get Path to game resources folder
		DirectoryInfo di = new DirectoryInfo(Path.Combine(ressourcePath,"StreamingAssets")); // Get Path to Streaming assets

		int numberFiles = CreateFiles.DirCount (di)/2;

		String filePath =  Path.Combine("StreamingAssets", "Comp"+numberFiles+".json"); // Get Path to file in resources folder
		String realPath = Path.Combine(ressourcePath,filePath); // Get Real Path

		JsonTestClass.SaveJSONToFile(JsonTestClass.MapToJSON (musicFile), realPath);
	}

    public void remove (NoteTimeLineScript toRemove)
    {
		if (gs.tutoInc == 5) {
			gs.audioSource.Stop;
			gs.audioSource.PlayOneShot (gs.audioClip [5]);
			gs.tutoInc = 6;
		}
        Note toDestroy = partition[toRemove.partitionIndex];
        partition.RemoveAt(toRemove.partitionIndex);
        Destroy(toDestroy.NoteTimeLine);
        Destroy(toDestroy.corr);

        // refresh the page
        Refresh();
    }
}

public class Note
{
    public GameObject NoteTimeLine;
    public NoteScript NoteToPlay;
    public int page;
    private int indexInPartition;
    public int nbFourth;
    public GameObject corr;

    public Note(GameObject _NoteTimeLine, NoteScript _NoteToPlay, int _page, int _indexInPartition, int _nbFourth)
    {
        NoteTimeLine = _NoteTimeLine;
        NoteToPlay = _NoteToPlay;
        page = _page;
        setIndexInPartition(_indexInPartition);
        nbFourth = _nbFourth;
    }

    public void setIndexInPartition(int index)
    {
        indexInPartition = index;
        NoteTimeLine.GetComponent<NoteTimeLineScript>().partitionIndex = index;
    }
}