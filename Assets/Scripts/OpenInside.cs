namespace VRTK
{

    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class OpenInside : VRTK_InteractableObject
    {

        public GameManager mgr;

        public override void StartUsing(GameObject usingObject)
        {
            base.StartUsing(usingObject);
            mgr.SetScene();

        }

        void Start()
        {
            mgr = GameObject.Find("Mgr").GetComponent<GameManager>(); //get a reference to the game manager;

        }
    }
}