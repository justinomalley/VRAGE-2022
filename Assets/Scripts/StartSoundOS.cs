using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class StartSoundOS: MonoBehaviour
{

    GameManager mgr;
    GameObject elePlane;
    MusicClass aud;
    bool play;

    // Use this for initialization
    void Start()
    {
        mgr = GameObject.Find("Mgr").GetComponent<GameManager>();
        elePlane = mgr.elePlane;
    }

    // Update is called once per frame
    void Update()
    {
        if (elePlane.activeSelf && !play)
        {
            play = true;
            mgr.mus.PlayMusic("OS");
            Destroy(this);
        }

    }
}
