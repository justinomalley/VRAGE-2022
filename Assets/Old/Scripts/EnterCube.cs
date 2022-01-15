namespace VRTK.Examples
{
    using UnityEngine;

    public class EnterCube : MonoBehaviour
    {

        public float speed = 10f;
        private Interact use;

        protected void Start()
        {

            use = GetComponent<Interact>();
        }

        public void StartUsing(GameObject usingObject)
        {
            //base.StartUsing(usingObject);

            StartCoroutine(use.Disappear());
        }

        public void StopUsing(GameObject usingObject)
        {
            //base.StopUsing(usingObject);
        }



        protected void Update()
        {
            //base.Update();

        }
    }
}