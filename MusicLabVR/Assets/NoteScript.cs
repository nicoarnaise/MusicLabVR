using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteScript : MonoBehaviour {

    public Material idleMaterial;
    public Material playMaterial;
    public bool play = false;

    public int Octave;
    private TimeLineScript TimeLine;
    private AudioSource audioS;
    private Renderer rend;

    private int refresh;

    public GameObject gameState;
    private GameState gs;

    // Use this for initialization
    void Start () {
        refresh = 0;
        audioS = transform.GetComponent<AudioSource>();
        rend = transform.GetComponent<Renderer>();
        TimeLine = transform.parent.parent.GetComponent<OctaveScript>().TimeLine;
        Octave = transform.parent.parent.GetComponent<OctaveScript>().Octave;

        gameState = GameObject.Find("GameState");
        gs = gameState.GetComponent<GameState>();
    }

    void Update()
    {
        if (play)
        {
            play = false;
            Play();
        }
        if (!audioS.isPlaying)
        {
            rend.material = idleMaterial;
        }
        else
        {
            rend.material = playMaterial;
        }
    }

    public void Play(){
        if (gs.tutoInc == 2)
        {
            gs.audioSource.Stop();
            gs.audioSource.PlayOneShot(gs.audioClip[2]);
            gs.tutoInc = 3;
        }
        audioS.Play();
        string addon = "";
        if (transform.name.EndsWith("b"))
            addon = "b";
        if (transform.name.EndsWith("d"))
            addon = "#";
        TimeLine.AddNote(this, Octave, addon);
    }

    // play the note for time seconds
    public void PlayT(float time)
    {
        refresh++;
        audioS.Play();
        Invoke("Stop", time);
    }

    private void Stop()
    {
        refresh--;
        if(refresh == 0)
            audioS.Stop();
    }

	public int getNoteName(){
		
		if (transform.name.Contains ("do")) {
			if (transform.name.EndsWith ("d")) {
				return 1;
			} else {
				return 0;
			}
		}

		if (transform.name.Contains ("re")) {
			if (transform.name.EndsWith ("d")) {
				return 3;
			} else {
				if (transform.name.EndsWith ("b")) {
					return 1;
				} else {
					return 2;
				}	
			}
		}

		if (transform.name.Contains ("mi")) {
			if (transform.name.EndsWith ("d")) {
				return 5;
			} else {
				if (transform.name.EndsWith ("b")) {
					return 3;
				} else {
					return 4;
				}	
			}
		}

		if (transform.name.Contains ("fa")) {
			if (transform.name.EndsWith ("d")) {
				return 6;
			} else {
				if (transform.name.EndsWith ("b")) {
					return 4;
				} else {
					return 5;
				}	
			}
		}

		if (transform.name.Contains ("sol")) {
			if (transform.name.EndsWith ("d")) {
				return 8;
			} else {
				if (transform.name.EndsWith ("b")) {
					return 6;
				} else {
					return 7;
				}	
			}
		}

		if (transform.name.Contains ("la")) {
			if (transform.name.EndsWith ("d")) {
				return 10;
			} else {
				if (transform.name.EndsWith ("b")) {
					return 8;
				} else {
					return 9;
				}	
			}
		}

		if (transform.name.Contains ("si")) {
			if (transform.name.EndsWith ("b")) {
				return 10;
			} else {
				return 11;
			}
		}

		return 100;
	}
		/*switch (transform.name)
		{
		case "do"+Octave:
			return 0;
			break;
		case "do"+Octave+"d":
		case "re"+Octave+"b":
			return 1;
			break;
		case "re"+Octave:
			return 2;
			break;
		case "re"+Octave+"d":
		case "mi"+Octave+"b":
			return 3;
			break;
		case "mi"+Octave:
		case "fa"+Octave+"b":
			return 4;
			break;
		case "mi"+Octave+"d":
		case "fa"+Octave:
			return 5;
			break;
		case "fa"+Octave+"d":
		case "sol"+Octave+"b":
			return 6;
			break;
		case "sol"+Octave:
			return 7;
			break;
		case "sol"+Octave+"d":
		case "la"+Octave+"b":
			return 8;
			break;
		case "la"+Octave:
			return 9;
			break;
		case "la"+Octave+"d":
		case "si"+Octave+"b":
			return 10;
			break;
		case "si"+Octave:
			return 11;
			break;
		default:
			return -1;
			break;
		}
	}*/
}
