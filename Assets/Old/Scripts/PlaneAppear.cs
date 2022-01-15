using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneAppear : MonoBehaviour {

    public Color visible, highlight;                    //color for all planes; color for highlighted plane
    public Material pMaterial, hMaterial;               //material for all planes and highlighted plane
    //public VRTK.VRTK_SimplePointer left, right;         //VRTK pointers
    //public VRTK.VRTK_HeightAdjustTeleport tele;         //Tele script; to set start plane when loading new gallery
    public List<Renderer> pRenderers;                   //List of plane renderers in current traversable scene
    public bool canTele;

    private GameObject planeObj; //empty gameobject to store tele planes; gallery-specific elevator plane
    private GameObject[] planes; //array for planes
    private GameManager mgr;

    private Color opaque; //0 alpha version of 'visible'
    private bool fadeout; //set this to true when the pointer button is released

	void Start () {
        //tele = GameObject.Find("PlayArea").GetComponent<VRTK.VRTK_HeightAdjustTeleport>();
        mgr = GameObject.Find("Mgr").GetComponent<GameManager>();

        DontDestroyOnLoad(this);                                 //We will reload the pRenderers list with whatever planes are in the current scene; this object will persist
        opaque = new Color(visible.r, visible.g, visible.b, 0f); //same as visible, except 0 for alpha
        hMaterial.color = highlight;                             //highlighted plane color

        StartCoroutine(GetPlanes());
        
    }
	
	void Update () {
        if(true/*(left.pointerBeam && left.pointerBeam.activeSelf) || (right.pointerBeam && right.pointerBeam.activeSelf) && canTele*/) //if pointer is active
        {
            fadeout = false;
            StartCoroutine(FadeIn());

        }else if(!fadeout && canTele){ //if pointer is released

            fadeout = true; 
            foreach (Renderer x in pRenderers)   //resets material of all planes and turns off renderers
            {
                if (x != null)
                {
                    x.material = pMaterial;
                    x.enabled = false;
                }
                
            }

            pMaterial.color = opaque; //sets plane material to opaque
        }
	}

    private IEnumerator FadeIn() //coroutine to fade in planes
    {
        foreach (Renderer x in pRenderers)
        { //turn on plane renderers
            if (x != null)
                x.enabled = true;
        }

        //This next chunk fades in the planes

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

    public IEnumerator GetPlanes() //coroutine to fade in planes
    {
        yield return new WaitForSeconds(1);

        planeObj = GameObject.Find("Planes");

        if(pRenderers.Count > 0)
            ClearRendArray(); //clear plane renderers from last gallery 

        pRenderers.Add(mgr.elePlane.GetComponent<Renderer>());

        for (int i = 0; i < planeObj.transform.childCount; i++)
        {
            if(planeObj.transform.GetChild(i).tag == "Plane")
                pRenderers.Add(planeObj.transform.GetChild(i).GetComponent<Renderer>());
        }

        foreach (Renderer x in pRenderers) //disable all plane renderers when not in use
            x.enabled = false;

        canTele = true;

    }

    public void ClearRendArray() //To clear renderer array when loading new scene
    {
        pRenderers.Clear();
    }

}
