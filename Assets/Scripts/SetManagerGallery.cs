using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetManagerGallery : MonoBehaviour
{
    /*Sets manager variable when new gallery scene is loaded*/

    private GameManager mgr;      //reference to game manager
    public string room;
    public AudioSource[] sounds;

    void Start()
    {
        sounds = FindObjectsOfType<AudioSource>();
        mgr = GameObject.Find("Mgr").GetComponent<GameManager>();//get game manager
        mgr.sounds = sounds;
        
        StartCoroutine(mgr.pA.GetPlanes()); //get planes for this floor

    }

}
