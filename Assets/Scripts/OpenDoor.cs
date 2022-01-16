namespace VRTK
{
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using System.Collections;
    using System.Collections.Generic;
    using VRTK;

    public class OpenDoor : VRTK_InteractableObject
    {

        private bool l1unload = false;    //so that the button cannot be used when the door is already opened
        private int num;                       //current gallery floor number
        private Animator anim;                 //animator attached to 'doors' gameobject
        private RotateElevator rt;

        public bool buttonPressed;
        public GameObject doors, vrtk, elevatorPlane; 
        public GameManager mgr;

        public AudioSource eleSound;
        public MusicClass mus;

        public override void StartUsing(GameObject usingObject)
        {

            base.StartUsing(usingObject);
            StartCoroutine(Wait());

        }

        protected void Start()
        {
            mus = GameObject.Find("MediaPlayer").GetComponent<MusicClass>();

            mgr = GameObject.Find("Mgr").GetComponent<GameManager>();
            mgr.opn = this;
            elevatorPlane = mgr.elePlane;

            anim = doors.GetComponent<Animator>();

            elevatorPlane = GameObject.Find("Elevator Plane");

            rt = GameObject.Find("Elevator"). GetComponent<RotateElevator>();

        }

        protected override void Update()
        {
            base.Update();
        }

        public IEnumerator Wait() //Functionality for opening elevator door
        {

            if (!buttonPressed) //if the button hasn't already been pressed
            {
                mgr.opn.eleSound.Play();
                mgr.inOpBt.material = mgr.wt;

                num = mgr.floorNumber; //get current floorNumber 

                buttonPressed = true; //then it has!
                anim.SetBool("buttonPressed", true); //animate the doors opening

                yield return new WaitForSeconds(12);

                if (!elevatorPlane.activeSelf)
                {
                    StartCoroutine(mus.FadeOutOldMusic());
                    if(mgr.sounds != null)
                        foreach (AudioSource x in mgr.sounds)
                            if(x.gameObject.name != "Elevator")
                                x.Stop();
                }
                    


                anim.SetBool("buttonPressed", false); //stop the door opening animation

                yield return new WaitForSeconds(0.5f);

                if (!l1unload && !elevatorPlane.activeSelf)
                {
                    l1unload = true;
                    SceneManager.UnloadSceneAsync("L1");
                    
                }

                if (SceneManager.GetSceneByName(mgr.scenes[num])!=null && SceneManager.GetSceneByName(mgr.scenes[num]).isLoaded && !elevatorPlane.activeSelf)
                {
                    SceneManager.UnloadSceneAsync(mgr.scenes[num]);
                    mgr.scn = SceneManager.GetSceneByName("E1");
                }

                buttonPressed = false; //so you can press the button again if not in the elevator yet

                mgr.inOpBt.material = mgr.rdy;

            }

            yield return null;
            base.StopUsing(usingObject);
        }

    }
}