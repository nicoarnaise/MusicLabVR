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
    public int pageToShow;

    private int nbFourth;
    private float Z1;
    private float Z2;

    private float startTime;
    private float currentTime;
    private int noteIndex;
    private int pageShown;
    

    // list Note and their duration
    public List<Note> partition;

    // list NoteTimeLine
    private List<Page> pages;

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
        partition = new List<Note>();
        pages = new List<Page>();
        // creation of the first page
        pages.Add(new Page());
        pageToShow = 0;
        pageShown = 0;
    }

    void Update() {
        if (pageToShow < pages.Count && pageToShow > -1)
            showPage(pageToShow);
        else
            pageToShow = pageShown;
    }

    private void showPage(int pToShow)
    {
        if(pToShow != pageShown && pToShow < pages.Count && pToShow > -1)
        {
            if (GetComponentInChildren<MenuPrefScript>().gameObject)
                Destroy(GetComponentInChildren<MenuPrefScript>().transform.parent.gameObject);

            pageToShow = pToShow;
            foreach(Note note in pages[pageShown].content)
            {
                note.NoteTimeLine.SetActive(false);
            }
            pageShown = pageToShow;
            foreach (Note note in pages[pageShown].content)
            {
                note.NoteTimeLine.SetActive(true);
            }
            nbFourth = pages[pageShown].nbFourth;
        }
    }

    public void AddNote(NoteScript Note, int Octave, string addon)
    {
        if (selected)
        {
            showPage(pages.Count - 1);

            GameObject newNoteGO = Instantiate(NoteTimeLineRef, transform, false);
            newNoteGO.transform.localPosition = new Vector3(startX + spaceX * (nbFourth + (selected.value * 2) - 1), Ypos,Z1);
            newNoteGO.transform.localScale = new Vector3(newNoteGO.transform.localScale.x * (selected.value * 4), newNoteGO.transform.localScale.y, newNoteGO.transform.localScale.z);
            if(addon != "")
                newNoteGO.GetComponentInChildren<TextMesh>().text = "" + Octave + "\n" + addon;
            else
                newNoteGO.GetComponentInChildren<TextMesh>().text = "" + Octave;
            newNoteGO.GetComponent<Renderer>().material = Note.idleMaterial;

            if(nbFourth + selected.value * 4 > nbFourthMax)
            {
                foreach(Note note in pages[pages.Count - 1].content)
                {
                    note.NoteTimeLine.SetActive(false);
                }
                // add new page
                pages.Add(new Page());
                nbFourth = 0;
                pageToShow++;
                pageShown++;
                newNoteGO.transform.localPosition = new Vector3(startX + spaceX * (nbFourth + (selected.value * 2) - 1), Ypos, Z1);
            }
            Note newNote = new Note(newNoteGO, Note, pageShown, pages[pageShown].content.Count - 1, partition.Count - 1, (int)(selected.value * 4));
            pages[pageShown].content.Add(newNote);
            nbFourth += newNote.nbFourth;
            pages[pageShown].nbFourth = nbFourth;
            partition.Add(newNote);
        }   
    }

    public void AddRest()
    {
        if (selected)
        {
            showPage(pages.Count - 1);

            GameObject newNoteGO = Instantiate(NoteTimeLineRef, transform, false);
            newNoteGO.transform.localPosition = new Vector3(startX + spaceX * (nbFourth + (selected.value * 2) - 1), Ypos, Z1);
            newNoteGO.transform.localScale = new Vector3(newNoteGO.transform.localScale.x * (selected.value * 4), newNoteGO.transform.localScale.y, newNoteGO.transform.localScale.z);
            newNoteGO.GetComponentInChildren<TextMesh>().text = "";

            if (nbFourth + selected.value * 4 > nbFourthMax)
            {
                foreach (Note note in pages[pages.Count - 1].content)
                {
                    note.NoteTimeLine.SetActive(false);
                }
                // add new page
                pages.Add(new Page());
                nbFourth = 0;
                pageToShow++;
                pageShown++;
                newNoteGO.transform.localPosition = new Vector3(startX + spaceX * (nbFourth + (selected.value * 2) - 1), Ypos, Z1);
            }
            Note newNote = new Note(newNoteGO, null, pageShown, pages[pageShown].content.Count - 1, partition.Count - 1, (int)(selected.value * 4));
            pages[pages.Count - 1].content.Add(newNote);
            nbFourth += (int)(selected.value * 4);
            pages[pages.Count - 1].nbFourth = nbFourth;
            partition.Add(newNote);
        }
    }

    public void AddAt(int page, float x, Note note)
    {
        int size = (int)(note.NoteTimeLine.transform.localScale.x / (0.002f));
        showPage(page);
        pages[page].nbFourth += size;
        int index = 0;
        while(pages[page].content[index].NoteTimeLine.transform.localPosition.x < x)
        {
            index++;
        }
        pages[page].content.Insert(index, note);
        partition.Insert(note.indexInPartition, note);
        // à finir
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

    public void remove (NoteTimeLineScript toRemove, bool destroy)
    {
        Note noteToRemove = partition[toRemove.partitionIndex];
        int size = noteToRemove.nbFourth;
        float tRx = spaceX * size;
        partition.RemoveAt(toRemove.partitionIndex);
        pages[pageShown].content.Remove(noteToRemove);
        foreach (Note note in pages[pageShown].content)
        {
            if (note.NoteTimeLine.transform.localPosition.x > toRemove.transform.localPosition.x)
                note.NoteTimeLine.transform.localPosition = new Vector3(note.NoteTimeLine.transform.localPosition.x - tRx, note.NoteTimeLine.transform.localPosition.y, note.NoteTimeLine.transform.localPosition.z);
        }
        pages[pageShown].nbFourth -= size;
        nbFourth -= size;
        if (pageShown < pages.Count - 1)
        {
            while(pages[pageShown+1].content[0].NoteTimeLine.transform.localScale.x / (0.002f) <= nbFourthMax - nbFourth )
            {
                int sizeNext = (int)(pages[pageShown + 1].content[0].NoteTimeLine.transform.localScale.x / (0.002f));
                AddAt(pageShown, spaceX * (nbFourth + (sizeNext * 2) - 1), pages[pageShown + 1].content[0]);
                pages[pageShown].content.Add(pages[pageShown + 1].content[0]);
                pages[pageShown].nbFourth += sizeNext;
                showPage(pageShown + 1);
                remove(pages[pageShown].content[0].NoteTimeLine.GetComponent<NoteTimeLineScript>(), false);
                showPage(pageShown - 1);
            }
        }
        if(destroy)
            Destroy(toRemove.transform.gameObject);
    }
}

public class Page
{
    public List<Note> content;
    public int nbFourth;

    public Page()
    {
        content = new List<Note>();
        nbFourth = 0;
    }
}

public class Note
{
    public GameObject NoteTimeLine;
    public NoteScript NoteToPlay;
    public int page;
    public int indexInPage;
    public int indexInPartition;
    public int nbFourth;

    public Note(GameObject _NoteTimeLine, NoteScript _NoteToPlay, int _page, int _indexInPage, int _indexInPartition, int _nbFourth)
    {
        NoteTimeLine = _NoteTimeLine;
        NoteToPlay = _NoteToPlay;
        page = _page;
        indexInPage = _indexInPage;
        indexInPartition = _indexInPartition;
        nbFourth = _nbFourth;
    }
}