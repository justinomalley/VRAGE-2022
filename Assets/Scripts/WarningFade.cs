using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningFade : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(WaitAndFade());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator WaitAndFade()
    {
        yield return new WaitForSeconds(4);

        Destroy(this.gameObject);
        }

}
