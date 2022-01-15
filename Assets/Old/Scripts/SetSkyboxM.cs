using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetSkyboxM : MonoBehaviour
{
    GameManager mgr;
    GameObject elePlane;
    bool play;

    // Use this for initialization
    void Start()
    {

        SceneManager.SetActiveScene(SceneManager.GetSceneByName("M"));

        mgr = GameObject.Find("Mgr").GetComponent<GameManager>();
        elePlane = mgr.elePlane;

    }

    // Update is called once per frame
    void Update()
    {
        if (elePlane.activeSelf && !play)
        {
            play = true;
            mgr.mus.PlayMusic("M");
            Destroy(this);
        }
    }
}
