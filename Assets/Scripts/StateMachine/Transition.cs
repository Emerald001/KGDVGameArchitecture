public class Transition<T>
{
    public Transition(System.Predicate<T> _condition, System.Type _toState)
    {
        this.condition = _condition;
        this.toState = _toState;
    }
    public System.Predicate<T> condition;
    public System.Type toState;
}