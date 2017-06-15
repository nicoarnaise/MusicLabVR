using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteTimeLineScript : MonoBehaviour {

    public bool activate = false;
    public int menuZ = 10;
    public GameObject menuPref;
    public bool hasMenuOpened = false;
    public int partitionIndex = 0;

    private GameObject childMenu;

    // Update is called once per frame
    void Update () {
		if (activate)
        {
            activate = false;
            openMenu();
        }

        if (childMenu)
            hasMenuOpened = true;
        else
            hasMenuOpened = false;
	}

    public void openMenu()
    {
        if (transform.parent.GetComponentInChildren<MenuPrefScript>())
            Destroy(transform.parent.GetComponentInChildren<MenuPrefScript>().gameObject);

        if (hasMenuOpened)
            Destroy(childMenu);
        else
        {
            if (transform.parent.name.Equals("EditMenu"))
            {
                childMenu = Instantiate(menuPref, transform.parent, false);
                childMenu.transform.localPosition = new Vector3(transform.localPosition.x, 0.003f, menuZ);
                childMenu.GetComponent<MenuPrefScript>().target = this;
            }
        }
    }
}
