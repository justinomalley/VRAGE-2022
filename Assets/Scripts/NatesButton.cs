﻿namespace VRTK
{
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using System.Collections;
    using System.Collections.Generic;
    using VRTK;

    public class NatesButton: VRTK_InteractableObject
    {

        private GameObject wall;

        public override void StartUsing(GameObject usingObject)
        {

            base.StartUsing(usingObject);
            Destroy(wall);

        }

        protected void Start()
        {
            wall = GameObject.Find("Nate's Wall");

        }

        protected override void Update()
        {
            base.Update();
        }


    }
}