using UnityEngine;
using UnityEngine.Video;

public class ProjectorControl : InteractableObject {
    [SerializeField]
    private VideoPlayer videoPlayer;

    [SerializeField]
    private GameObject text;

    private Vector3 upPosition, downPosition;

    private Animator animator;

    private NNAGallery gallery;

    private bool playing;
    
    private static readonly int RollFilm = Animator.StringToHash("rollFilm");
    private static readonly int StopFilm = Animator.StringToHash("stopFilm");

    protected override void Awake() {
        base.Awake();
        gallery = GetComponentInParent<NNAGallery>();
        animator = transform.parent.GetComponent<Animator>();
        upPosition = transform.position;
        downPosition = transform.Find("Down").position;
    }

    protected override void Touch() {
        Highlight();
    }
    
    protected override void Untouch() {
        Unhighlight();
    }

    public override void Interact() {
        if (playing) {
            videoPlayer.Stop();
            text.SetActive(true);
            animator.SetTrigger(StopFilm);
            Unpress();
            gallery.StopProjectorSound();
            return;
        }
        
        videoPlayer.Play();
        text.SetActive(false);
        animator.SetTrigger(RollFilm);
        Press();
        gallery.SetSound(Artist.Nicole);
    }

    private void Press() {
        playing = true;
        transform.position = downPosition;
    }

    private void Unpress() {
        playing = false;
        transform.position = upPosition;
    }

    public bool IsPlaying() {
        return playing;
    }
}
