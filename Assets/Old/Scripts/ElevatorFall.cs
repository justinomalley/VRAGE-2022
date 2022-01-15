using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ElevatorFall : MonoBehaviour {

    public OpenAndDisappear opn;
    public RotateElevator rt;
    public GameObject cPlane1, cPlane2, mPlane1, mPlane2, elePlane;

    private bool rtd;

	// Use this for initialization
	void Start () {
        rt = GameObject.Find("Elevator").GetComponent<RotateElevator>();
        opn = GameObject.Find("SetManager").GetComponent<OpenAndDisappear>();
        elePlane = opn.elevatorPlane;
        StartCoroutine(opn.WaitAndDisappear());
	}
	
	// Update is called once per frame
	void Update () {
        if (cPlane1.activeSelf && cPlane2.activeSelf && elePlane.activeSelf && !rtd) //if we are not on chelsea's planes & we are not in the elevator
        {

            rtd = true; //first spin
            rt.Rotate(); //spin it to pick us up in Makenna's gallery
            

        }
        else if (mPlane1.activeSelf && mPlane2.activeSelf && elePlane.activeSelf && rtd) //if we are not on makenna's planes & we are not in the elevator
        {

            rtd = false; //next spin if they go back into chelsea's
            rt.Rotate(); //spin it to pick us up in Chelsea's gallery
            
        }
	}
}
