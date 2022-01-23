using UnityEngine;
using UnityEngine.Events;

public class Fader<T> : MonoBehaviour {
    private UnityEvent finishedFadingEvent { get; } = new UnityEvent();
    
    private IAnimatableProperty<T> property;

    private IAnimatableComponent<T> component;
    
    private float duration = 1f;
    
    private readonly AnimationTimer timer = new AnimationTimer();

    private bool finishedFadingEventFired;

    protected T startValue, endValue, currentValue;

    private bool initialized = true;

    private bool durationOverriden;

    protected void Initialize(IAnimatableComponent<T> comp, IAnimatableProperty<T> prop, T start, T end, float dur) {
        component = comp;
        property = prop;
        startValue = start;
        endValue = end;
        if (!durationOverriden) {
            duration = dur;
        }
        
        initialized = true;
    }

    public void Update () {
        if (!initialized) {
            return;
        }
        
        if (!finishedFadingEventFired && timer.Finished()) {
            finishedFadingEventFired = true;
            finishedFadingEvent.Invoke();
            return;
        }
        
        if (!timer.Running()) {
            return;
        }
        
        timer.Update();
        
        SetValue(property.Evaluate(startValue, endValue, timer.GetProgress()));
    }

    public T CurrentValue() {
        return currentValue;
    }
    
    public void Fade(UnityAction action = null) {
        finishedFadingEvent.RemoveAllListeners();

        if (action != null) {
            finishedFadingEvent.AddListener(action);
        }
        
        timer.Start(duration);
    }

    public void Fade(T end, UnityAction action = null) {
        startValue = currentValue;
        endValue = end;

        finishedFadingEvent.RemoveAllListeners();
        if (action != null) {
            finishedFadingEvent.AddListener(action);
        }
        
        finishedFadingEventFired = false;
        timer.Start(duration);
    }

    // Setting the value this way will stop any ongoing fades and remove listeners.
    public void SetValue(T value) {
        currentValue = value;
        component.Set(currentValue);
    }

    public void SetDuration(float dur) {
        durationOverriden = true;
        duration = dur;
    }

    public void CancelFade(T value) {
        finishedFadingEvent.RemoveAllListeners();
        finishedFadingEventFired = false;
        SetValue(value);
        timer.Stop();
    }

    public void SetAnimationCurve(AnimationCurve curve) {
        timer.SetAnimationCurve(curve);
    }
}
