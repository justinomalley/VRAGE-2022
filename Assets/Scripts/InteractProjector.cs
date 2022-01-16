namespace VRTK
{

    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.Video;
    using System.Collections;

    public class InteractProjector : VRTK_InteractableObject
    {
        private Animator anim;
        public VideoPlayer vid;
        private AudioSource aud;
        private GameObject txt;
        bool isPlaying;

        protected void Start()
        {
            aud = GetComponent<AudioSource>();
            txt = GameObject.Find("ScreenText");
            anim = GameObject.Find("Projector").GetComponent<Animator>();
            vid = GameObject.Find("Video").GetComponent<VideoPlayer>();
            isUsable = true;

        }

        public override void StartUsing(GameObject usingObject)
        {
            base.StartUsing(usingObject);

            if (!isPlaying)
            {
                isPlaying = true;
                txt.SetActive(false);
                anim.ResetTrigger("stopfilm");
                anim.SetTrigger("rollfilm");
                aud.Play();
                vid.Play();
            }
        }

        public void StopVideo()
        {
            isPlaying = false;
            aud.Stop();
            vid.Stop();
            txt.SetActive(true);
            anim.SetTrigger("stopfilm");
            
        }

    }
}
