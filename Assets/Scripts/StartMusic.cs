using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMusic : MonoBehaviour {

    MusicClass mus;

	// Use this for initialization
	void Start () {
        mus = GetComponent<MusicClass>();
        mus.PlayMusic("Tutorial");
        Destroy(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
