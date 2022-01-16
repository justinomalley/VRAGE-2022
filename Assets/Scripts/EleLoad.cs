using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EleLoad : MonoBehaviour {


    /*Placed in Lobby scene. Elevator scene loads and this gameobject instantly is destroyed*/

	void Awake () {
        SceneManager.LoadSceneAsync("E1", LoadSceneMode.Additive);
        
        Destroy(this.gameObject);
	}
	
}
