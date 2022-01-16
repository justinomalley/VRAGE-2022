using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ElevatorFallCM : MonoBehaviour
{

    public OpenAndDisappear opn;
    public RotateElevator rt;
    public GameObject cPlane1, cPlane2, mPlane1, mPlane2, elePlane;

    private Animator anim;

    public bool rtd;

    // Use this for initialization
    void Start()
    {
        anim = GameObject.Find("Chelsee").GetComponent<Animator>();
        anim.SetBool("animate", true);

        rt = GameObject.Find("Elevator").GetComponent<RotateElevator>();
        opn = GameObject.Find("SetManager").GetComponent<OpenAndDisappear>();

        elePlane = opn.elevatorPlane;
        StartCoroutine(opn.WaitAndDisappear());

    }

    // Update is called once per frame
    void Update()
    {
        if (cPlane1.activeSelf && cPlane2.activeSelf && elePlane.activeSelf && !rtd) //if we are not on chelsea's planes & we are not in the elevator
        {
            anim.SetBool("animate", false);
            rtd = true; //first spin
            rt.Rotate(); //spin it to pick us up in Makenna's gallery
            opn.startPos += new Vector3(0f, 0f, 2f);
            opn.endPos += new Vector3(0f, 0f, 2f);
            rt.moved = true;

        }
        else if (mPlane1.activeSelf && mPlane2.activeSelf && elePlane.activeSelf && rtd) //if we are not on makenna's planes & we are not in the elevator
        {
            anim.SetBool("animate", true);
            rtd = false; //next spin if they go back into chelsea's
            rt.Rotate(); //spin it to pick us up in Chelsea's gallery
            opn.startPos += new Vector3(0f, 0f, -2f);
            opn.endPos += new Vector3(0f, 0f, -2f);
            rt.moved = false;

        }
    }
}
