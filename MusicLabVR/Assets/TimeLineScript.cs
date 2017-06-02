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
    private List<KeyValuePair<NoteScript, float>> partition;

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
        partition = new List<KeyValuePair<NoteScript, float>>();
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
            pageToShow = pToShow;
            foreach(GameObject NoteTimeLine in pages[pageShown].content)
            {
                NoteTimeLine.SetActive(false);
            }
            pageShown = pageToShow;
            foreach (GameObject NoteTimeLine in pages[pageShown].content)
            {
                NoteTimeLine.SetActive(true);
            }
            nbFourth = pages[pageShown].nbFourth;
        }
    }

    public void AddNote(NoteScript Note, int Octave, string addon)
    {
        if (selected)
        {
            showPage(pages.Count - 1);

            GameObject newNote = Instantiate(NoteTimeLineRef, transform, false);
            newNote.transform.localPosition = new Vector3(startX + spaceX * (nbFourth + (selected.value * 2) - 1), Ypos,Z1);
            newNote.transform.localScale = new Vector3(newNote.transform.localScale.x * (selected.value * 4), newNote.transform.localScale.y, newNote.transform.localScale.z);
            if(addon != "")
                newNote.GetComponentInChildren<TextMesh>().text = "" + Octave + "\n" + addon;
            else
                newNote.GetComponentInChildren<TextMesh>().text = "" + Octave;
            newNote.GetComponent<Renderer>().material = Note.idleMaterial;

            if(nbFourth + selected.value * 4 > nbFourthMax)
            {
                foreach(GameObject NoteTimeLine in pages[pages.Count - 1].content)
                {
                    NoteTimeLine.SetActive(false);
                }
                // add new page
                pages.Add(new Page());
                nbFourth = 0;
                pageToShow++;
                pageShown++;
                newNote.transform.localPosition = new Vector3(startX + spaceX * (nbFourth + (selected.value * 2) - 1), Ypos, Z1);
            }
            pages[pages.Count - 1].content.Add(newNote);
            nbFourth += (int)(selected.value * 4);
            pages[pages.Count - 1].nbFourth = nbFourth;
            partition.Add(new KeyValuePair<NoteScript, float>(Note, selected.value));
        }   
    }

    public void AddRest()
    {
        if (selected)
        {
            showPage(pages.Count - 1);

            GameObject newNote = Instantiate(NoteTimeLineRef, transform, false);
            newNote.transform.localPosition = new Vector3(startX + spaceX * (nbFourth + (selected.value * 2) - 1), Ypos, Z1);
            newNote.transform.localScale = new Vector3(newNote.transform.localScale.x * (selected.value * 4), newNote.transform.localScale.y, newNote.transform.localScale.z);
            newNote.GetComponentInChildren<TextMesh>().text = "";

            if (nbFourth + selected.value * 4 > nbFourthMax)
            {
                foreach (GameObject NoteTimeLine in pages[pages.Count - 1].content)
                {
                    NoteTimeLine.SetActive(false);
                }
                // add new page
                pages.Add(new Page());
                nbFourth = 0;
                pageToShow++;
                pageShown++;
                newNote.transform.localPosition = new Vector3(startX + spaceX * (nbFourth + (selected.value * 2) - 1), Ypos, Z1);
            }
            pages[pages.Count - 1].content.Add(newNote);
            nbFourth += (int)(selected.value * 4);
            pages[pages.Count - 1].nbFourth = nbFourth;
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

public class Page
{
    public List<GameObject> content;
    public int nbFourth;

    public Page()
    {
        content = new List<GameObject>();
        nbFourth = 0;
    }
}
