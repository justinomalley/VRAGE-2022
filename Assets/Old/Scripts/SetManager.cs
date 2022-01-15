using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetManager : MonoBehaviour {

    /*Sets variables of GameManager when elevator scene loads*/

    private GameManager mgr;

	void Start () {
        mgr = GameObject.Find("Mgr").GetComponent<GameManager>(); //get manager
        //mgr.vrtk = GameObject.Find("[VRTK]");                //assign variables
        mgr.ui = GameObject.Find("UI").GetComponent<VRTK.QuadUI>();
        mgr.anim = GameObject.Find("doors").GetComponent<Animator>();
        mgr.elePlane = GameObject.Find("Elevator Plane");
	}

}
