using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

public class ElevatorDisplay : MonoBehaviour {

    [SerializeField]
    private Sprite[] floorImages;

    private Image image;

    private TransformScaleFader fader;

    private bool loading;

    private void Awake() {
        image = GetComponent<Image>();
        fader = GetComponent<TransformScaleFader>();
    }

    public void SetRoom(GalleryLoader.Room room) {
        loading = true;
        fader.Fade(Vector3.zero, () => {
            image.sprite = floorImages[(int)room];
            fader.Fade(Vector3.one, () => {
                loading = false;
            });
        });
    }

    public bool IsLoading() {
        return loading;
    }
}
