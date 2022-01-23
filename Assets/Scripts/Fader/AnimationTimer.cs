using UnityEngine;

public class AnimationTimer {
    private float timer, startTime, duration, progress;

    private bool started, finished;
    
    private AnimationCurve animCurve = AnimationCurve.Linear(0, 0, 1, 1);
    
    // This is not a MonoBehaviour, so it will need to be updated from another script.
    public void Update () {
        if (!started || finished) {
            return;
        }

        if (progress < 1) {
            timer = Time.time - startTime;
            progress = animCurve.Evaluate(timer / duration);
        } else {
            finished = true;
            started = false;
            progress = 1;
        }
    }
    
    public void Start(float dur) {
        duration = dur;
        startTime = Time.time;
        timer = 0;
        progress = 0;
        started = true;
        finished = false;
    }

    public void Stop() {
        started = false;
        finished = true;
    }
    
    public bool Running() {
        return started && !finished;
    }
    
    public float GetProgress() {
        return progress;
    }

    public bool Finished() {
        return finished;
    }

    public void SetAnimationCurve(AnimationCurve curve) {
        animCurve = curve;
    }
}
