using UnityEngine;

public class Lobby : GalleryRoom {
    [SerializeField]
    private TeleportPad startPad;
    
    private GameObject lobbyLogo;

    protected override void Awake() {
        base.Awake();
        lobbyLogo = transform.Find("LobbyLogo").gameObject;
    }

    /// <summary>
    /// DisableLobbyLogo is used to hide the logo attached to the 'Lobby' gameobject
    /// so we can use the one from the tutorial (so the tutorial one can fade in).
    /// </summary>
    public void FirstTimeLobbyLoad() {
        lobbyLogo.SetActive(false);
        TeleportPadManager.ForceTeleport(startPad);
    }
}
