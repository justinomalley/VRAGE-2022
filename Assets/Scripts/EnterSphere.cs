namespace VRTK.Examples
{
    using UnityEngine;

    public class EnterSphere : VRTK_InteractableObject
    {

        public float speed = 10f;
        private Interact use;

        public override void StartUsing(GameObject usingObject)
        {
            base.StartUsing(usingObject);

            use.Disappear();
        }

        public override void StopUsing(GameObject usingObject)
        {
            base.StopUsing(usingObject);
        }

        protected void Start()
        {
            DontDestroyOnLoad(this);

            use = GetComponent<Interact>();
        }

        protected override void Update()
        {
            base.Update();
            
        }
    }
}