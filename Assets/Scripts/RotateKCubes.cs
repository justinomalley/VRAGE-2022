using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateKCubes : MonoBehaviour {

    private GameObject [] obj;

    public float rate = 10f, kRate = 10f;


    void Start () {

        if (transform.childCount > 0)
        {
            obj = new GameObject[transform.childCount];

            for (int i = 0; i < transform.childCount; i++)
            {
                obj[i] = transform.GetChild(i).gameObject;
                obj[i].AddComponent<RotateKCubes>();
                RotateKCubes rk = obj[i].GetComponent<RotateKCubes>();
                rk.rate = kRate;
            }
        }
        else
        {
            obj = new GameObject[0];
        }
	}


	
	// Update is called once per frame
	void FixedUpdate () {
        transform.Rotate(0, rate * Time.deltaTime, 0);
	}
}
