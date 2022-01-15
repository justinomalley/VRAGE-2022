using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Tutorial : MonoBehaviour {

    public GameObject preElePlane;

    private GameManager mgr;

    private AudioSource aud;
    public AudioClip[] clips;

    private Renderer tut;
    public Material img;

    bool tele1, tele2;
   
	// Use this for initialization
	void Start () {
        mgr = GameObject.Find("Mgr").GetComponent<GameManager>();
        aud = GetComponent<AudioSource>();
        tut = GetComponent<Renderer>();
        
	}
	
	// Update is called once per frame
	void Update () {
        if (preElePlane != null)
            if (!preElePlane.activeSelf && !tele1)
            {
                tele1 = true;
                tut.material = img;

                if (!aud.isPlaying)
                {
                    aud.clip = clips[0];
                    aud.Play();
                }
                else
                    StartCoroutine(WaitAndPlay(0));
            }
            else if (tele1 && !mgr.elePlane.activeSelf)
                StartCoroutine(WaitAndDestroy());
            
	}

    private IEnumerator WaitAndPlay(int num)
    {
        while (aud.isPlaying)
            yield return null;
        aud.clip = clips[num];
        aud.Play();

        if (num == 1)
            Destroy(this);

    }

    private IEnumerator WaitAndDestroy()
    {
        while (aud.isPlaying)
            yield return null;

        Destroy(this.gameObject);

    }
}
