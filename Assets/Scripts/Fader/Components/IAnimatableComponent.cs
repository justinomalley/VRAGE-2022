/// <summary>
/// IAnimatableComponent represents some GameObject component that can be animated by a value of type `T`.
/// </summary>
public interface IAnimatableComponent<T> {
    public void Set(T property);
}