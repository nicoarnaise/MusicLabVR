using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LengthBtnScript : MonoBehaviour {

    public bool doSelect = false;
    public bool selected;
    public float value;
    public Material notSelectedMaterial;
    public Material selectedMaterial;

    public TimeLineScript TLS;

    private Renderer rend;

    // Use this for initialization
    void Start () {
        selected = false;
        rend = transform.GetComponent<Renderer>();
    }
	
	// Update is called once per frame
	void Update () {
        if (doSelect)
        {
            doSelect = false;
            Select();
        }
        if (selected)
            rend.material = selectedMaterial;
        else
            rend.material = notSelectedMaterial;
	}

    public void Select()
    {
        selected = !selected;
        if (TLS.selected)
        {
            TLS.selected.selected = false;
        }
        if (selected)
            TLS.selected = this;
        else
            TLS.selected = null;
    }
}
