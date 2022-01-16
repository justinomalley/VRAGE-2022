using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class StartVideoAndSoundCM : MonoBehaviour
{

    ElevatorFallCM efcm;
    GameManager mgr;
    GameObject elePlane;
    VideoPlayer vid1, vid2, vid3;
    AudioSource ch, mk;
    bool play;

    // Use this for initialization
    void Start()
    {
        efcm = FindObjectOfType<ElevatorFallCM>();
        vid1 = GameObject.Find("left").GetComponent<VideoPlayer>();
        vid2 = GameObject.Find("front").GetComponent<VideoPlayer>();
        vid3 = GameObject.Find("right").GetComponent<VideoPlayer>();
        mgr = GameObject.Find("Mgr").GetComponent<GameManager>();
        ch = GameObject.Find("Chelsee").GetComponent<AudioSource>();
        mk = GameObject.Find("Makenna").GetComponent<AudioSource>();
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
            vid3.Play();
            ch.Play();
        }

        if(efcm.rtd && !mk.isPlaying)
        {
            ch.Stop();
            mk.Play();
        }else if (!efcm.rtd && !ch.isPlaying)
        {
            mk.Stop();
            ch.Play();
        }

    }
}
