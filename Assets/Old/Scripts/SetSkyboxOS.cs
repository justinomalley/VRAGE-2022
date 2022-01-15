using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetSkyboxOS : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("OS"));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
