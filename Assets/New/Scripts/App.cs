using UnityEngine;

public class App : MonoBehaviour {
    [SerializeField]
    private GameObject lobbyPrefab, elevatorPrefab;

    private static GameObject lobby, elevator;

    private static App instance;

    private void Awake() {
        instance = this;
    }

    public static void EnterLobby() {
        TeleportPadManager.DestroyAllPads();
        
        lobby = Instantiate(instance.lobbyPrefab);
        elevator = Instantiate(instance.elevatorPrefab);
        
        Destroy(GameObject.Find("TutorialOnlyLogo"));
        var lobbyLogo = GameObject.Find("LobbyLogo");
        lobbyLogo.transform.SetParent(lobby.transform);
        lobbyLogo.GetComponent<MaterialColorFader>().Fade();
        
    }
}
