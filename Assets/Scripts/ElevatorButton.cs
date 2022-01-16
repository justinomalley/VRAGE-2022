namespace VRTK
{

    using UnityEngine;

    public class ElevatorButton : VRTK_InteractableObject
    {
        
        public int num;             //this number is the same as the name of the button
        private GameManager mgr;     //GAME MANAGER
        private GameObject manager;  //~~~~~~~~~~~~
        private QuadUI quad;         //reference to UI
        private RotateElevator rt;

        AudioSource aud;


        protected void Start()
        {
            isUsable = true;
            holdButtonToUse = false;
            isGrabbable = false;

            aud = GetComponent<AudioSource>();

            rt = GameObject.Find("Elevator").GetComponent<RotateElevator>();
            num = int.Parse(name); //the name of this button is the index of the floor it is assigned to

            manager = GameObject.Find("Mgr");
            mgr = manager.GetComponent<GameManager>();
            quad = GameObject.Find("UI").GetComponent<QuadUI>();

        }

        public override void StartUsing(GameObject usingObject)
        {
            base.StartUsing(usingObject);

            if (quad.closing == false && quad.loading == false)
            {
                mgr.SetFloorNumber(num);
                if(!mgr.opn.buttonPressed)
                    aud.Play();
            }
        }

    }
}
