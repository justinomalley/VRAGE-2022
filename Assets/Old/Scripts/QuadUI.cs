namespace VRTK
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;
    using UnityEngine;
    using UnityEngine.Video;

    public class QuadUI : MonoBehaviour
    {
        //public Text display;
        //public LevelChange level;
        public GameObject vidQuad;
        private VideoPlayer vidPlayer;
        public VideoClip[] vids;
        
        public float scaleSpeed = 1f;
        public bool loadLevel, closing, loading; //'closing' set in game manager

        public int floorNumber; //set in game manager.

        private GameObject manager;
        private GameManager mgr;


        private float xScale, xMaxScale, minScale = 0f, yScale, yMaxScale;

        void Start()
        {

            manager = GameObject.Find("Mgr");
            mgr = manager.GetComponent<GameManager>();
            mgr.ui = this;

            vidPlayer = vidQuad.GetComponent<VideoPlayer>();

            xScale = vidQuad.transform.localScale.x;
            xMaxScale = vidQuad.transform.localScale.x;
            yScale = vidQuad.transform.localScale.y;
            yMaxScale = vidQuad.transform.localScale.y;

        }

        void Update()
        {
            if (loading)
            {
                xScale += scaleSpeed * Time.deltaTime;
                yScale += scaleSpeed * Time.deltaTime;

                if (xScale > xMaxScale)
                    xScale = xMaxScale;

                if (yScale > yMaxScale)
                    yScale = yMaxScale;

                // Limit the growth
                if (xScale >= xMaxScale && yScale >= yMaxScale)
                    loading = false;

                // Apply the new scale
                vidQuad.transform.localScale = new Vector3(xScale, yScale, 1);
            }

            if (closing)
            {
                xScale -= scaleSpeed * Time.deltaTime;
                yScale -= scaleSpeed * Time.deltaTime;
                // Limit the growth
                    
                if (xScale < minScale)
                    xScale = minScale;

                if (yScale < minScale)
                    yScale = minScale;

                if (xScale <= minScale && yScale <= minScale)
                {
                    closing = false;
                    vidPlayer.clip = vids[floorNumber];
                    loading = true;
                }

                // Apply the new scale
                vidQuad.transform.localScale = new Vector3(xScale, yScale, 1);
            }
        }
    }
}
