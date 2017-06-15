//======= Copyright (c) Valve Corporation, All rights reserved. ===============
using UnityEngine;
using System.Collections;
using Valve.VR;
using UnityEngine.SceneManagement;

public struct PointerEventArgs
{
    public uint controllerIndex;
    public uint flags;
    public float distance;
    public Transform target;
}

public delegate void PointerEventHandler(object sender, PointerEventArgs e);


public class SteamVR_LaserPointer : MonoBehaviour
{
    public bool active = true;
    public Color color;
    public float thickness = 0.002f;
    public GameObject holder;
    public GameObject pointer;
    bool isActive = false;
    public bool addRigidBody = false;
    public Transform reference;
    public TimeLineScript TLS;
    public CreateFiles CFS;

    public event PointerEventHandler PointerIn;
    public event PointerEventHandler PointerOut;

    private bool triggerPressedBefore;
    private bool touchedBefore;

    Transform previousContact = null;

	

	// Use this for initialization
	void Start ()
    {
        triggerPressedBefore = false;
        touchedBefore = false;

        holder = new GameObject();
        holder.transform.parent = this.transform;
        holder.transform.localPosition = Vector3.zero;
		holder.transform.localRotation = Quaternion.identity;

		pointer = GameObject.CreatePrimitive(PrimitiveType.Cube);
        pointer.transform.parent = holder.transform;
        pointer.transform.localScale = new Vector3(thickness, thickness, 100f);
        pointer.transform.localPosition = new Vector3(0f, 0f, 50f);
		pointer.transform.localRotation = Quaternion.identity;
		BoxCollider collider = pointer.GetComponent<BoxCollider>();
        if (addRigidBody)
        {
            if (collider)
            {
                collider.isTrigger = true;
            }
            Rigidbody rigidBody = pointer.AddComponent<Rigidbody>();
            rigidBody.isKinematic = true;
        }
        else
        {
            if(collider)
            {
                Object.Destroy(collider);
            }
        }
        Material newMaterial = new Material(Shader.Find("Unlit/Color"));
        newMaterial.SetColor("_Color", color);
        pointer.GetComponent<MeshRenderer>().material = newMaterial;
	}

    public virtual void OnPointerIn(PointerEventArgs e)
    {
        if (PointerIn != null)
            PointerIn(this, e);
    }

    public virtual void OnPointerOut(PointerEventArgs e)
    {
        if (PointerOut != null)
            PointerOut(this, e);
    }


    // Update is called once per frame
	void Update ()
    {
        if (!isActive)
        {
            isActive = true;
            this.transform.GetChild(0).gameObject.SetActive(true);
        }

        float dist = 100f;

        SteamVR_TrackedController controller = GetComponent<SteamVR_TrackedController>();

        Ray raycast = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        bool bHit = Physics.Raycast(raycast, out hit);

        if(previousContact && previousContact != hit.transform)
        {
            PointerEventArgs args = new PointerEventArgs();
            if (controller != null)
            {
                args.controllerIndex = controller.controllerIndex;
            }
            args.distance = 0f;
            args.flags = 0;
            args.target = previousContact;
            OnPointerOut(args);
            previousContact = null;
        }
        if(bHit && previousContact != hit.transform)
        {
            PointerEventArgs argsIn = new PointerEventArgs();
            if (controller != null)
            {
                argsIn.controllerIndex = controller.controllerIndex;
            }
            argsIn.distance = hit.distance;
            argsIn.flags = 0;
            argsIn.target = hit.transform;
            OnPointerIn(argsIn);
            previousContact = hit.transform;
        }
        if(!bHit)
        {
            previousContact = null;
        }
        if (bHit && hit.distance < 100f)
        {
            dist = hit.distance;
        }

        // user can play notes by keeping trigger pressed if they have nothing selected for note duration.

        NoteScript NS = hit.transform.GetComponent<NoteScript>();
		if (hit.transform) {

			if (NS && !NS.transform.parent.parent.GetComponent<OctaveScript> ().TimeLine.GetComponent<TimeLineScript> ().selected)
				triggerPressedBefore = false;

			if (controller != null && controller.triggerPressed && !triggerPressedBefore) {
				triggerPressedBefore = true;
				LengthBtnScript LB = hit.transform.GetComponent<LengthBtnScript> ();
				MusicBtnScript MB = hit.transform.GetComponent<MusicBtnScript> ();
				NoteTimeLineScript NTL = hit.transform.GetComponent<NoteTimeLineScript> ();
				MenuPrefScript MP = hit.transform.GetComponent<MenuPrefScript> ();
				LevelSelectScript LS = hit.transform.GetComponent<LevelSelectScript> ();

                if (NS)
                    NS.Play();
				if (LB)
					LB.Select ();
				if (MB)
					MB.DoAction ();
				if (NTL)
					NTL.openMenu ();
				if (MP)
					MP.doAct ();
				if (LS)
					LS.EnterLevel ();
				pointer.transform.localScale = new Vector3 (thickness * 5f, thickness * 5f, dist);
			} else {
				if (controller != null && !controller.triggerPressed)
					triggerPressedBefore = false;
			}
		}
		if (!(controller != null && controller.triggerPressed && !triggerPressedBefore))
			pointer.transform.localScale = new Vector3 (thickness, thickness, dist);

        if (controller != null && controller.gripped)
            SceneManager.LoadScene(0);

        SteamVR_Controller.Device device = SteamVR_Controller.Input((int)controller.controllerIndex);
        //If finger is on touchpad
        if (device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad) && !touchedBefore)
        {
            touchedBefore = true;
            //Read the touchpad values
            Vector2 touchpad = device.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);

            // handle rotation via touchpad
            if (TLS)
            {
                if (touchpad.x > 0.3f)
                {
                    TLS.pageToShow++;
                }
                else
                {
                    if (touchpad.x < -0.3f)
                    {
                        TLS.pageToShow--;
                    }
                }
            }else if (CFS)
            {
                if (touchpad.y > 0.3f)
                {
                    CFS.showPrev();
                }
                else
                {
                    if (touchpad.y < -0.3f)
                    {
                        CFS.showNext();
                    }
                }
            }
        }
        else
        {
            if (!device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
            {
                touchedBefore = false;
            }
        }
        pointer.transform.localPosition = new Vector3(0f, 0f, dist/2f);
    }
}
