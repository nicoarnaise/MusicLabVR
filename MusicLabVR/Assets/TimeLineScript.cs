﻿using System.Collections;
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
    public int pageToShow;

    public int nbFourth;
    private float Z1;
    private float Z2;

    private float startTime;
    private float currentTime;
    private int noteIndex;
    private int pageShown;

    public int indexToInsert;
    

    // list Note and their duration
    public List<Note> partition;

    // list NoteTimeLine

	// Use this for initialization
	void Start () {
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
            if (note.NoteTimeLine.activeSelf)
                note.NoteTimeLine.SetActive(false);
        }
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
                }
                note.page = page;
                nbFourth += note.nbFourth;
                index++;
                if (index < partition.Count)
                    note = partition[index];
                else
                    break;
            }
            if (index >= partition.Count)
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
                if(note.NoteTimeLine.activeSelf)
                    note.NoteTimeLine.SetActive(false);
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

    public void AddRest()
    {
        if (selected)
        {
            // show the last page (here by giving the partition length, we know it would be over the number of pages)
            if(indexToInsert==-1)
                showPage(partition.Count);

            GameObject newNoteGO = Instantiate(NoteTimeLineRef, transform, false);
            newNoteGO.transform.localPosition = new Vector3(startX + spaceX * (nbFourth + (selected.value * 2) - 1), Ypos, Z1);
            newNoteGO.transform.localScale = new Vector3(newNoteGO.transform.localScale.x * (selected.value * 4), newNoteGO.transform.localScale.y, newNoteGO.transform.localScale.z);
            newNoteGO.GetComponentInChildren<TextMesh>().text = "";
           
            Note newNote = new Note(newNoteGO, null, pageShown, partition.Count - 1, (int)(selected.value * 4));
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

    public void remove (NoteTimeLineScript toRemove)
    {
        partition.RemoveAt(toRemove.partitionIndex);
        Destroy(toRemove.transform.gameObject);

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