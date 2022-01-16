using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using VRTK;

public class GameManager : MonoBehaviour {

    public int floorNumber;      //the number of the floor we're on/ if we're in the elevator, the one we want to go to
    public bool enter;           //turned to false after entering the elevator

    public GameObject elePlane, left, right; //reference to the playspace and elevator teleport plane
    public Animator anim;     //to animate doors

    public QuadUI ui;    //user interface
    public OpenDoor opn; //outside open button (NEED TO SET VARIABLES!!!)
    public PlaneAppear pA;

    public string[] scenes; //list of galleries
    public Scene scn;

    public Renderer inOpBt;
    public Material rdy, wt;

    public MusicClass mus;
    public AudioSource[] sounds;

    private RotateElevator rt;

    

    void Awake()
    {
        DontDestroyOnLoad(this); //this gameobject will exist throughout the entire build, through scenes
    }

	void Start () {
        mus = GameObject.Find("MediaPlayer").GetComponent<MusicClass>();
        mus.PlayMusic("Lobby");

        enter = true;
        floorNumber = 0; //0 is the index for the lobby, where we 'start'
        pA = GetComponent<PlaneAppear>();
        StartCoroutine(GetRt());
    }

    public void SetFloorNumber(int n) //set when elevator buttons are pressed, controls which floor will be loaded
    {
        if (floorNumber != n)
        {
            floorNumber = n;

            if (!ui.closing || !ui.loading) {
                ui.floorNumber = floorNumber;
                ui.closing = true;
            }
        } 
    }

    public void SetScene() //Load the scene indicated by the manager and open the elevator doors
    {

        if (ui.closing == false && ui.loading == false && !opn.buttonPressed)
        {
            rt.ResetRotation();
            StartCoroutine(rt.RotateForGallery());

            SceneManager.LoadSceneAsync(scenes[floorNumber], LoadSceneMode.Additive); //load the current selected floor
            StartCoroutine(opn.Wait());
            StartCoroutine(pA.GetPlanes());
            
            scn = SceneManager.GetSceneByName(scenes[floorNumber]);
            
        }    
    }

    public void UnloadGallery(string gallery)
    {
        if (SceneManager.sceneCount > 1)
            SceneManager.UnloadSceneAsync(gallery);
    }

    private IEnumerator GetRt()
    {
        yield return new WaitForSeconds(2);
        rt = GameObject.Find("Elevator").GetComponent<RotateElevator>();
    }
    
        
    

}
