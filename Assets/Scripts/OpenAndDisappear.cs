using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class OpenAndDisappear : MonoBehaviour {

    public float lerpTime = 5, currLerpTime = 0; 
    public Vector3 startPos, endPos;
    private GameManager mgr;
    private CallElevator l, r;

    public bool falling, rising, canCallBack;
    public GameObject elevator, elevatorPlane, target;


    void Start () {
        StartCoroutine(GetControllers());

        elevator = GameObject.Find("Elevator");
        mgr = GameObject.Find("Mgr").GetComponent<GameManager>();


        startPos = elevator.transform.position;        //set transforms to make elevator disappear if necessary.
        endPos = target.transform.position;

    }

   void Update()
    {
        if (falling) //elevator sinks through floor
        {
            currLerpTime += Time.deltaTime;

            if (currLerpTime >= lerpTime)
            {
                currLerpTime = lerpTime;
                falling = false;
                canCallBack = true;
            }

            float perc = currLerpTime / lerpTime;
            elevator.transform.position = Vector3.Lerp(startPos, endPos, perc);
            
        }

        if (rising) //elevator rises back up
        {
            currLerpTime += Time.deltaTime;

            if (currLerpTime >= lerpTime)
            {
                canCallBack = false;
                currLerpTime = lerpTime;
                rising = false;

            }

            float perc = currLerpTime / lerpTime;
            elevator.transform.position = Vector3.Lerp(endPos, startPos, perc);

        }
    }

    public void SetToRise()
    {
        if (canCallBack == true && !rising)
        {
            currLerpTime = 0;

            canCallBack = false;
            rising = true;
            StartCoroutine(WaitAndOpen());
            
        }
    }

   public IEnumerator WaitAndDisappear()
    {
        yield return new WaitForSeconds(14); //waits for elevator door to close: if still inside, reloads elevator scene. otherwise, elevator sinks through floor
        if (!elevatorPlane.activeSelf)
        {
            yield return null;
        }
        else
        {
            SetTarget();
            currLerpTime = 0;
            falling = true;
        }

    }
    

    public IEnumerator GetControllers()
    {
        yield return new WaitForSeconds(2);

        if(GameObject.Find("l") != null)
            l = GameObject.Find("l").GetComponent<CallElevator>();

        if (GameObject.Find("l") != null)
            r = GameObject.Find("r").GetComponent<CallElevator>();

        if(l == null || r == null)
        {
            StartCoroutine(GetControllers());
            yield break;
        }

        l.disp = this;
        r.disp = this;

    }

    public void SetTarget()
    {
        if (SceneManager.GetSceneByName("K").isLoaded)
        {
            startPos = elevator.transform.position;
            target = GameObject.Find("kTarget");
            endPos = target.transform.position;
        }
        else
        {
            startPos = elevator.transform.position;
            target = GameObject.Find("Target");
            endPos = target.transform.position;
        }
            
    }

    public IEnumerator WaitAndOpen()
    {
        yield return new WaitForSeconds(4.5f);
        StartCoroutine(WaitAndDisappear());
        StartCoroutine(mgr.opn.Wait());
      
    }

}
