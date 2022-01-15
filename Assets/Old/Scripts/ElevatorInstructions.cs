using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorInstructions : MonoBehaviour {

    private GameManager mgr;
    AudioSource aud;
    bool inside;

	// Use this for initialization
	void Start () {
        mgr = GameObject.Find("Mgr").GetComponent<GameManager>();
        aud = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!mgr.elePlane.activeSelf && !inside)
        {
            inside = true;

            StartCoroutine(WaitAndPlay());

        }
	}

    private IEnumerator WaitAndPlay()
    {
        while (mgr.elePlane.activeSelf)
            yield return null;

        yield return new WaitForSeconds(4);

        aud.Play();

        Destroy(this);

    }
}
