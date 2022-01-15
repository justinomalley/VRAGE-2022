using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMsgJ : MonoBehaviour
{

    GameManager mgr;
    GameObject elePlane;
    AudioSource aud;
    bool play;

    // Use this for initialization
    void Start()
    {
        mgr = GameObject.Find("Mgr").GetComponent<GameManager>();
        elePlane = mgr.elePlane;
        aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (elePlane.activeSelf && !play)
        {
            play = true;
            aud.Play();
            Destroy(this);
        }
        else if (!elePlane.activeSelf)
        {
            aud.Stop();
            play = false;
        }
            
    }
}
