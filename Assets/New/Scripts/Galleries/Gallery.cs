using UnityEngine;

public class Gallery : MonoBehaviour {
    
    private AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }
}
