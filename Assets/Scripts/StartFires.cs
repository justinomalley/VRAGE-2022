using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFires : MonoBehaviour {

    public ParticleSystem campfire, fireplace;
    Collider col;
    public bool camp;

    void Start () {
        col = GetComponent<Collider>();
	}

	void Update () {
		
	}


    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);

        if (other.gameObject.name == "Torch" && camp)
        {
            campfire.Play();
        }
        else if (other.gameObject.name == "Torch")
        {
            fireplace.Play();
        }
    }
}
