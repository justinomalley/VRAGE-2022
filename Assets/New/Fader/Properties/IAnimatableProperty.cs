public interface IAnimatableProperty<T> {
    public T Evaluate(T startValue, T endValue, float progress);
}
