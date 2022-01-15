using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goodbye : MonoBehaviour {

    public GameObject gb;
    public int num = 10;

	// Use this for initialization
	void Start () {
        StartCoroutine(WaitForGameToEnd(num));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator WaitForGameToEnd(int secs)
    {
        yield return new WaitForSeconds(num);
        gb.SetActive(true);
        yield return new WaitForSeconds(15);
        SceneManager.LoadScene("preT");
    }
}
