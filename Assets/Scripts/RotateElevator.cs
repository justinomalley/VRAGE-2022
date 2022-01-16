using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateElevator : MonoBehaviour {

    private GameManager mgr;               //reference to Game Manager
    public GameObject lvl, cmr, elePlane; //lvl is Elevator, cmr is CameraRig, elevator plane
    public bool rotated;                  //to tell if the elevator is not in it's original position
    private Transform eTrans;              //original transform of elevator

    public bool moved;

    void Start () {
        
        lvl = this.gameObject;                                    //attached to elevator
        elePlane = GameObject.Find("Elevator Plane");             //elevator plane disabled when inside elevator
        eTrans = lvl.transform;                                   //elevator transform
        mgr = GameObject.Find("Mgr").GetComponent<GameManager>();
        cmr = GameObject.Find("[CameraRig]");
        
	}

    void Update()
    {
        

    }

    public IEnumerator RotateForGallery()
    {
        Debug.Log("RotateForGallery called");

        while (mgr.opn.buttonPressed || mgr.ui.loading || mgr.ui.closing)
        {
            yield return null;
            Debug.Log("No rotation");
        }

        if ((mgr.floorNumber == 3 || mgr.floorNumber == 7) && !rotated)
        {
            Rotate();
            Debug.Log("Rotation");

        }
        else if ((mgr.floorNumber != 3 || mgr.floorNumber != 7) && rotated)
        {
            Rotate();
            Debug.Log("Rotate back");
        }else if ((mgr.floorNumber == 3 || mgr.floorNumber == 7) && rotated)
        {
            yield return null;
        }
    }

    public void Rotate()
    {
        Debug.Log("Rotate called");

        if (cmr.transform.parent != lvl.transform && !elePlane.activeSelf)
            cmr.transform.SetParent(lvl.transform);

        if (moved)
        {
            lvl.transform.position += new Vector3(0f, 0f, -2f);
            moved = false;
        }

        lvl.transform.Rotate(0, 180, 0);

        rotated = !rotated;

        cmr.transform.parent = null;
    }

    public void ResetRotation()
    {
        Debug.Log("ResetRotation called");

        if (rotated)
        {
            Rotate();
            
        }
        //lvl.transform.position = eTrans.position;
    }


}
