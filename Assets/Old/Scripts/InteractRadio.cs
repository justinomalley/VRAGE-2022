namespace VRTK
{

    using UnityEngine;
    using System.Collections;

    public class InteractRadio : MonoBehaviour /*VRTK_InteractableObject*/
    {

        private MusicClass mus;
        bool isPlaying;



        protected void Start()
        {
            mus = GameObject.Find("MediaPlayer").GetComponent<MusicClass>();
            //isUsable = true;

        }

        public /*override*/ void StartUsing(GameObject usingObject)
        {
            //base.StartUsing(usingObject);

            if (!isPlaying)
            {
                isPlaying = true;
                mus.PlayMusic("J");
            }
        }

    }
}
