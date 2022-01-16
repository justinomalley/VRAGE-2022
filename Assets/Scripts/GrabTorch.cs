﻿namespace VRTK
{

    using UnityEngine;

    public class GrabTorch : VRTK_InteractableObject
    {

        private GameManager mgr;     //GAME MANAGER
        private GameObject manager;  //~~~~~~~~~~~~
        private Rigidbody rgb;
        private Collider col;


        protected void Start()
        {
            col = GetComponent<Collider>();
            rgb = GetComponent<Rigidbody>();

            isUsable = true;
            holdButtonToUse = false;
            isGrabbable = true;


        }

        public override void StartUsing(GameObject usingObject)
        {
            base.StartUsing(usingObject);
            rgb.useGravity = true;

        }


    }
}
