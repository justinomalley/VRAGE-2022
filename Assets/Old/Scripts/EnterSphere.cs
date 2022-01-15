namespace VRTK.Examples
{
    using UnityEngine;

    public class EnterSphere : MonoBehaviour
    {

        public float speed = 10f;
        private Interact use;

        public void StartUsing(GameObject usingObject)
        {
            //base.StartUsing(usingObject);

            use.Disappear();
        }

        public void StopUsing(GameObject usingObject)
        {
            //base.StopUsing(usingObject);
        }

        protected void Start()
        {
            DontDestroyOnLoad(this);

            use = GetComponent<Interact>();
        }

        protected void Update()
        {

        }
    }
}