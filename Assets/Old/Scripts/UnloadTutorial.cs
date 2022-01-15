using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnloadTutorial : MonoBehaviour
{


    /*Placed in Lobby scene. Elevator scene loads and this gameobject instantly is destroyed*/

    void Start()
    {
        if (SceneManager.GetSceneByName("T1").isLoaded)
            SceneManager.UnloadSceneAsync("T1");

        Destroy(this.gameObject);
    }

}
