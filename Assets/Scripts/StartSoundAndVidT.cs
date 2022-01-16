using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class StartSoundAndVidT : MonoBehaviour
{

    GameManager mgr;
    GameObject elePlane;
    MusicClass aud;
    VideoPlayer vid1, vid2;
    bool play;

    // Use this for initialization
    void Start()
    {
        vid1 = GameObject.Find("movie plane").GetComponent<VideoPlayer>();
        vid2 = GameObject.Find("TV Static Sculpture").GetComponent<VideoPlayer>();
        mgr = GameObject.Find("Mgr").GetComponent<GameManager>();
        elePlane = mgr.elePlane;
    }

    // Update is called once per frame
    void Update()
    {
        if (elePlane.activeSelf && !play)
        {
            play = true;
            vid1.Play();
            vid2.Play();
            mgr.mus.PlayMusic("T");
            Destroy(this);
        }

    }
}
