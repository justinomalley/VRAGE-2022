using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartVRAGE : MonoBehaviour {

    public SteamVR_TrackedController l, r;
    bool good2go;

	// Use this for initialization
	void Start () {
        Destroy(GameObject.Find("[CameraRig]"));
        Destroy(GameObject.Find("MediaPlayer"));
        Destroy(GameObject.Find("Mgr"));
    }

	
	// Update is called once per frame
	void Update () {

        if(l.triggerPressed && r.triggerPressed)
        {
            good2go = true;
            SceneManager.LoadScene("T1");
        }
            
	}
}
