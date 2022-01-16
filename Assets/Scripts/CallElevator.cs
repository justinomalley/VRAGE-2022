using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallElevator : MonoBehaviour {

    /*Press trigger to call elevator back. */

    private SteamVR_TrackedController controller;
    public OpenAndDisappear disp;
    private bool pressed;
    private GameManager mgr;

	// Use this for initialization
	void Start () {

        mgr = GameObject.Find("Mgr").GetComponent<GameManager>();
        disp = FindObjectOfType<OpenAndDisappear>();

        controller = GetComponent<SteamVR_TrackedController>();
        controller.Gripped += Grip;
        
    }

    void Grip(object sender, ClickedEventArgs e)
    {
        if (disp != null)

            disp.SetToRise();
        else
            Debug.LogError("disp is null");

    }


    // Update is called once per frame
    void Update () {

    }
}
