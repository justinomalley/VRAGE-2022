using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorSound : MonoBehaviour {

    public VRTK.OpenDoor opn;
    AudioSource aud;
    bool play = true;

	// Use this for initialization
	void Start () {

        aud = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!opn.buttonPressed & !play)
        {
            play = true;
            StartCoroutine(PlayDoorSound());
        }
	}

    private IEnumerator PlayDoorSound()
    {
        aud.Play();
        while (!opn.buttonPressed)
            yield return null;
        play = false;
    }
}
