using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class PlaneAppearTutorial : MonoBehaviour
{

    public Color visible, highlight; //color for all planes and highlighted plane
    public Material pMaterial, hMaterial; //material for all planes and highlighted plane
    public VRTK.VRTK_SimplePointer left, right;

    /*

        VARIABLES FOR TUTORIAL

    */

    public GameObject playspace; //for transforms; to see if playspace has moved
    public GameObject startPlane;

    public Renderer tut;
    public Material[] imgs;

    public AudioSource aud;
    public AudioClip [] clips;
    
    private bool tele1, tele2; //messages for teleportation

    /*
    
         END VARIABLES FOR TUTORIAL
         
    */


    public Renderer[] pRenderers; //renderers for all the planes in the scene
    private Color opaque; //0 alpha version of 'visible'
    private bool fadeout; //set this to true when the pointer button is released

    void Start()
    {
        opaque = new Color(visible.r, visible.g, visible.b, 0f); //same as visible, except 0 for alpha
        hMaterial.color = highlight; //highlighted plane color

        for (int i = 0; i < pRenderers.Length; i++) //disable all plane renderers when not in use
            pRenderers[i].enabled = false;

    }

    void Update()
    {
        if ((left.pointerBeam && left.pointerBeam.activeSelf) || (right.pointerBeam && right.pointerBeam.activeSelf)) //if pointer is active
        {
            fadeout = false;
            StartCoroutine(FadeIn());

            if (!tele1)
            {
                tele1 = true;
                tut.material = imgs[0];
                if (!aud.isPlaying)
                {
                    aud.clip = clips[0];
                    aud.Play();
                }
                else
                {
                    StartCoroutine(WaitAndPlay(0));
                }
                
            }
        }
        else if (!fadeout)
        { //if pointer is released

            StartCoroutine(CheckIfTeleported());

            fadeout = true;
            for (int i = 0; i < pRenderers.Length; i++)
            { //resets material of all planes and turns off renderers
                pRenderers[i].material = pMaterial;
                pRenderers[i].enabled = false;
            }

            pMaterial.color = opaque; //sets plane material to opaque
        }
    }

    private IEnumerator FadeIn() //coroutine to fade in planes
    {
        for (int i = 0; i < pRenderers.Length; i++) //turn on plane renderers
            pRenderers[i].enabled = true;

        //this next chunk fades in the planes

        float elapsedTime = 0.0f;
        float totalTime = 0.2f;
        while (elapsedTime < totalTime)
        {
            elapsedTime += Time.deltaTime;
            pMaterial.color = Color.Lerp(opaque, visible, (elapsedTime / totalTime));
            yield return null;
            if (fadeout)
                break;

        }

    }
    private IEnumerator CheckIfTeleported()
    {
        yield return new WaitForSeconds(0.1f);
        if (playspace.transform.position != startPlane.transform.position && tele1 && !tele2)
        {
            tele2 = true;
            tut.material = imgs[1];
            if (!aud.isPlaying)
            {
                aud.clip = clips[1];
                aud.Play();
            }
            else
            {
                StartCoroutine(WaitAndPlay(1));
            }
        }
    }

    private IEnumerator WaitAndPlay(int num)
    {
        aud.Stop();

        yield return new WaitForSeconds(1);

        if (num == 0 && tele2)
            aud.clip = clips[1];
        else
            aud.clip = clips[num];

        aud.Play();

    }

}
