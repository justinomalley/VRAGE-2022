namespace VRTK.Examples
{
    using UnityEngine;

    public class EnterCube : VRTK_InteractableObject
    {

        public float speed = 10f;
        private Interact use;

        protected void Start()
        {

            use = GetComponent<Interact>();
        }

        public override void StartUsing(GameObject usingObject)
        {
            base.StartUsing(usingObject);

            StartCoroutine(use.Disappear());
        }

        public override void StopUsing(GameObject usingObject)
        {
            base.StopUsing(usingObject);
        }



        protected override void Update()
        {
            base.Update();

        }
    }
}