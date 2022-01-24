using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Fader manages an AnimationTimer to animate some property `T` over time.
/// </summary>
public class Fader<T> : MonoBehaviour {
    
    /* Animatable property and component */

    private IAnimatableProperty<T> property;

    private IAnimatableComponent<T> component;
    
    /* Animation time */
    
    private readonly AnimationTimer timer = new AnimationTimer();
    
    private float duration = 1f;
    
    private bool durationOverriden;

    /* State */

    private T startValue, endValue;
    
    protected T currentValue;

    /* Finished animating event */
    
    private UnityEvent finishedFadingEvent { get; } = new UnityEvent();
    
    private bool finishedFadingEventFired;

    protected void Initialize(IAnimatableComponent<T> comp, IAnimatableProperty<T> prop, T start, T end, float dur) {
        component = comp;
        property = prop;
        startValue = start;
        endValue = end;
        
        // Don't overwrite duration if it's been set elsewhere. Set it again with `SetDuration`
        // if it needs to be overriden again.
        if (!durationOverriden) {
            duration = dur;
        }
    }

    public void Update () {
        // Check if animation is complete.
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

    /// <summary>
    /// SetAnimationCurve sets the curve to apply to this fade animation.
    /// </summary>
    /// <param name="curve"></param>
    public void SetAnimationCurve(AnimationCurve curve) {
        timer.SetAnimationCurve(curve);
    }
}
