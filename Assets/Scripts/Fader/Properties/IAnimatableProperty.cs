/// <summary>
/// IAnimatableProperty represents a property `T` that can be animated. `Evaluate` should
/// return a value between `startValue` and `endValue` determined by time value `progress`.
/// </summary>
public interface IAnimatableProperty<T> {
    public T Evaluate(T startValue, T endValue, float progress);
}
