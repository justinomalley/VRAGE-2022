using UnityEngine;
using UnityEngine.Events;

public class Fader<T> : MonoBehaviour {
    private UnityEvent finishedFadingEvent { get; } = new UnityEvent();
    
    private IAnimatableProperty<T> property;

    private IAnimatableComponent<T> component;
    
    private float duration = 1f;
    
    private readonly AnimationTimer timer = new AnimationTimer();

    private bool finishedFadingEventFired;

    private T startValue, endValue, currentValue;

    private bool initialized = true;

    protected void Initialize(IAnimatableComponent<T> comp, IAnimatableProperty<T> prop, T start, T end, float dur) {
        component = comp;
        property = prop;
        startValue = start;
        endValue = end;
        if (duration >= 10) {
            Debug.LogError("wattupppp");
        }
        duration = dur;
        initialized = true;
    }

    public void Update () {
        if (!initialized) {
            return;
        }
        
        if (!finishedFadingEventFired && timer.Finished()) {
            finishedFadingEvent.Invoke();
            finishedFadingEventFired = true;
            return;
        }
        
        if (!timer.Running()) {
            return;
        }
        
        timer.Update();
        
        currentValue = property.Evaluate(startValue, endValue, timer.GetProgress());
        component.Set(currentValue);
    }

    public T CurrentValue() {
        return currentValue;
    }
    
    public void Fade() {
        timer.Start(duration);
    }

    public void Fade(T end) {
        startValue = currentValue;
        endValue = end;
        timer.Start(duration);
        finishedFadingEvent.RemoveAllListeners();
        finishedFadingEventFired = false;
    }

    public void AddCallback(UnityAction callback) {
        finishedFadingEvent.AddListener(callback);
    }
}
