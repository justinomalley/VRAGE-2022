using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ElevatorFallK : MonoBehaviour
{

    private OpenAndDisappear opn;

    private GameObject elePlane;


    // Use this for initialization
    void Start()
    {
        opn = GameObject.Find("SetManager").GetComponent<OpenAndDisappear>();
        StartCoroutine(opn.WaitAndDisappear());
    }

    // Update is called once per frame
    void Update()
    {
    }
}
