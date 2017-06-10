using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPrefScript : MonoBehaviour {

    public NoteTimeLineScript target;
    public bool action = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.parent.parent.GetComponent<TimeLineScript>())
            target = transform.parent.GetComponent<MenuPrefScript>().target;
        if (action)
        {
            action = false;
            doAct();
        }
    }

    public void doAct()
    {
        TimeLineScript TLS = transform.parent.parent.GetComponent<TimeLineScript>();
        if (transform.name.Equals("Rmvbutton"))
            TLS.remove(target);
        if (transform.name.Equals("InsertLeftbutton"))
            TLS.indexToInsert = target.partitionIndex;
        if (transform.name.Equals("InsertRightbutton"))
            TLS.indexToInsert = target.partitionIndex + 1;
        closeMenu();
    }

    public void closeMenu()
    {
        target.hasMenuOpened = false;
        Destroy(transform.parent.gameObject);
    }
}
