using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using VRTK;

public class RoomTracker : MonoBehaviour
{
    private GameManager mgr;
    AudioSource a, na;
    InteractProjector ni;
    public GameObject [] aPlane, niPlane, naPlane;
    private GameObject elePlane;

    // Use this for initialization
    void Start()
    {
        a = GameObject.Find("aSound").GetComponent<AudioSource>();
        na = GameObject.Find("naSound").GetComponent<AudioSource>();
        ni = GameObject.Find("proj").GetComponent<InteractProjector>();

        mgr = GameObject.Find("Mgr").GetComponent<GameManager>();
        elePlane = mgr.elePlane;

    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < niPlane.Length; i++)
            if (!niPlane[i].activeSelf && elePlane.activeSelf)
            {
                na.Stop();
                a.Stop();
            }
            else
                for (int j = 0; j < aPlane.Length; j++)
                    if (!aPlane[j].activeSelf && elePlane.activeSelf && !a.isPlaying)
                    {
                        a.Play();
                        ni.StopVideo();
                        na.Stop();
                    }
                    else
                        for(int k = 0; k < naPlane.Length; k++)
                            if(!naPlane[k].activeSelf && elePlane.activeSelf && !na.isPlaying)
                            {
                                na.Play();
                                a.Stop();
                                ni.StopVideo();
                            }
    }
}
