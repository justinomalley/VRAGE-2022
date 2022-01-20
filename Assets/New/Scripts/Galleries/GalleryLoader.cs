using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// GalleryLoader is a singleton that manages the loading of gallery rooms.
/// </summary>
public class GalleryLoader : MonoBehaviour {
    [SerializeField]
    private GameObject elevatorPrefab;

    private static GameObject elevatorInstance;

    [SerializeField]
    private GameObject[] galleries;
    
    private static GameObject[] _galleries => instance.galleries;

    private static GameObject galleryInstance;

    /// <summary>
    /// Gallery represents all of the possible galleries that can be visited.
    /// </summary>
    public enum Gallery {
        None = -1,
        Lobby = 0,
        Sarah = 1,
        James = 2,
        Kincaid = 3,
        CM = 4,
        Mike = 5,
        NNA = 6,
        OS = 7,
    }

    // selectedGallery is the gallery that was last selected (typically via an elevator button).
    private static Gallery selectedGallery;
    
    // currentGallery is the gallery we are currently in.
    private static Gallery currentGallery = Gallery.None;
    
    private static GalleryLoader instance;

    private void Awake() {
        instance = this;
    }
    
    /// <summary>
    /// EnterLobby is used when exiting the tutorial scene to enter the lobby.
    /// </summary>
    public static void EnterLobby() {
        selectedGallery = Gallery.Lobby;
        LoadGallery();
        
        elevatorInstance = Instantiate(instance.elevatorPrefab);
        
        Destroy(GameObject.Find("TutorialOnlyLogo"));
        var lobbyLogo = GameObject.Find("LobbyLogo");
        lobbyLogo.transform.SetParent(galleryInstance.transform);
        lobbyLogo.GetComponent<MaterialColorFader>().Fade();
        galleryInstance.GetComponent<Lobby>().DisableLobbyLogo();
    }

    /// <summary>
    /// SelectGalleryRoom sets the internal `selectedGallery` value, which determines which
    /// gallery will be loaded when the user opens the elevator.
    /// </summary>
    public static void SetGalleryRoom(Gallery gallery) {
        selectedGallery = gallery;
    }
    
    /// <summary>
    /// LoadGallery destroys the currently loaded gallery, loads the gallery represented by the
    /// `selectedGallery` enum value, and executes `action` (which is currently only used to open
    /// the elevator doors when the gallery has loaded).
    /// </summary>
    public static void LoadGallery(UnityAction action = null) {
        // The selected gallery is already loaded; avoid reload.
        if (selectedGallery == currentGallery) {
            action?.Invoke();
            return;
        }
        
        Destroy(galleryInstance);
        TeleportPadManager.DestroyAllPads();

        galleryInstance = selectedGallery switch {
            Gallery.Lobby => Instantiate(_galleries[(int)Gallery.Lobby]),
            Gallery.Sarah => Instantiate(_galleries[(int)Gallery.Sarah]),
            Gallery.James => Instantiate(_galleries[0]),
            Gallery.Kincaid => Instantiate(_galleries[0]),
            Gallery.CM => Instantiate(_galleries[0]),
            Gallery.Mike => Instantiate(_galleries[0]),
            Gallery.NNA => Instantiate(_galleries[0]),
            Gallery.OS => Instantiate(_galleries[0]),
            _ => throw new ArgumentOutOfRangeException(nameof(selectedGallery), selectedGallery, null)
        };

        currentGallery = selectedGallery;
        action?.Invoke();
    }
}
