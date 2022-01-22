using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// GalleryLoader is a singleton that manages the loading of gallery rooms.
/// </summary>
public class GalleryLoader : MonoBehaviour {
    [SerializeField]
    private GameObject elevatorPrefab;

    [SerializeField]
    private GameObject[] galleries;
    
    private static GameObject[] _galleries => instance.galleries;

    private static GalleryRoom galleryInstance;

    /// <summary>
    /// Gallery represents all of the possible galleries that can be visited.
    /// </summary>
    public enum Room {
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
    private static Room selectedRoom;
    
    // currentGallery is the gallery we are currently in.
    private static Room currentRoom = Room.None;
    
    private static GalleryLoader instance;

    private void Awake() {
        instance = this;
    }

    public static void InitializeSoundFadeForDoors(ElevatorDoors doors) {
        doors.doorsOpeningEvent.AddListener(FadeInGalleryAudioInElevator);
        doors.doorsClosingEvent.AddListener(FadeOutGalleryAudioInElevator);
    }

    /// <summary>
    /// EnterLobby is used when exiting the tutorial scene to enter the lobby.
    /// </summary>
    public static void EnterLobby() {
        Instantiate(instance.elevatorPrefab);
        
        selectedRoom = Room.Lobby;
        LoadGallery();

        Destroy(GameObject.Find("TutorialOnlyLogo"));
        var lobbyLogo = GameObject.Find("LobbyLogo");
        lobbyLogo.transform.SetParent(galleryInstance.transform);
        lobbyLogo.GetComponent<MaterialColorFader>().Fade();
        var lobby = galleryInstance.GetComponent<Lobby>();
        lobby.FirstTimeLobbyLoad();
        
        galleryInstance.FadeAudioInOnEntry();
    }

    /// <summary>
    /// SelectGalleryRoom sets the internal `selectedGallery` value, which determines which
    /// gallery will be loaded when the user opens the elevator.
    /// </summary>
    public static void SetGalleryRoom(Room room) {
        selectedRoom = room;
        Elevator.SelectFloor(room);
    }
    
    /// <summary>
    /// LoadGallery destroys the currently loaded gallery, loads the gallery represented by the
    /// `selectedGallery` enum value, and executes `action` (which is currently only used to open
    /// the elevator doors when the gallery has loaded).
    /// </summary>
    public static void LoadGallery(UnityAction action = null) {
        if ((int)selectedRoom >= _galleries.Length) {
            Debug.LogError("No gallery available for " + selectedRoom);
            return;
        }
        
        // The selected gallery is already loaded; avoid reload.
        if (selectedRoom == currentRoom) {
            action?.Invoke();
            return;
        }

        if (galleryInstance != null) {
            Destroy(galleryInstance.gameObject);
        }
        
        TeleportPadManager.DestroyAllPads();

        var galleryObj = selectedRoom switch {
            Room.Lobby => Instantiate(_galleries[(int)Room.Lobby]),
            Room.Sarah => Instantiate(_galleries[(int)Room.Sarah]),
            Room.James => Instantiate(_galleries[(int)Room.James]),
            Room.Kincaid => Instantiate(_galleries[(int)Room.Kincaid]),
            Room.CM => Instantiate(_galleries[(int)Room.CM]),
            Room.Mike => Instantiate(_galleries[(int)Room.Mike]),
            Room.NNA => Instantiate(_galleries[(int)Room.NNA]),
            Room.OS => Instantiate(_galleries[(int)Room.OS]),
            _ => throw new ArgumentOutOfRangeException(nameof(selectedRoom), selectedRoom, null)
        };

        if (selectedRoom == Room.OS) {
            Elevator.RotateForOS();
        } else {
            Elevator.ResetPositionAndRotation();
        }

        galleryInstance = galleryObj.GetComponent<GalleryRoom>();
        currentRoom = selectedRoom;
        action?.Invoke();
    }

    private static void FadeOutGalleryAudioInElevator() {
        if (Elevator.InElevator()) {
            galleryInstance.FadeAudioOutOnExit();
        } else {
            galleryInstance.CancelFadeOut();
        }
    }

    private static void FadeInGalleryAudioInElevator() {
        if (Elevator.InElevator()) {
            galleryInstance.FadeAudioInOnEntry();
        }
    }
}
