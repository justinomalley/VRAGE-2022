using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMgrInOpBt : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameManager mgr = GameObject.Find("Mgr").GetComponent<GameManager>();
        mgr.inOpBt = this.gameObject.GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
