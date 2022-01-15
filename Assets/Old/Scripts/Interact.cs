using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interact : MonoBehaviour
{

    public float speed = 45f; //speed box rotates at

    private bool dspr; //prompts interactable object to disappear
    public float scaleSpeed = 1f;
    private float scale = 0.2f, minScale = 0f;

    void Start()
    {
        //EnterSphere includes DontDestroyOnLoad(this)
    }

    void Update()
    {
        transform.Rotate(Vector3.up, speed * Time.deltaTime);
        transform.Rotate(Vector3.left, speed * Time.deltaTime);

        if (dspr == true)
        {


            scale -= scaleSpeed * Time.deltaTime;
            
            // Limit the shrinkage
            if (scale < minScale)
            {
                scale = minScale;
            }

            // Apply the new scale
            transform.localScale = new Vector3(scale, scale, scale);
        }
    }

    public IEnumerator Disappear() //coroutine to make cube disappear
    {
        dspr = true;
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync("L1");
       
        
    }

}
