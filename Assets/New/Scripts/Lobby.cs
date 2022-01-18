using UnityEngine;

public class Lobby : MonoBehaviour {

    private AudioSource lobbyMusicSource;
    
    private void Awake() {
        lobbyMusicSource = GetComponent<AudioSource>();
    }
    
}
