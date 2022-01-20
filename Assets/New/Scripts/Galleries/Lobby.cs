using UnityEngine;

public class Lobby : Gallery {
    
    private GameObject lobbyLogo;
    
    private void Awake() {
        lobbyLogo = transform.Find("LobbyLogo").gameObject;
    }

    /// <summary>
    /// DisableLobbyLogo is used to hide the logo attached to the 'Lobby' gameobject
    /// so we can use the one from the tutorial (so the tutorial one can fade in).
    /// </summary>
    public void DisableLobbyLogo() {
        lobbyLogo.SetActive(false);
    }
}
