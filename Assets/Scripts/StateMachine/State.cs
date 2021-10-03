using System.Collections.Generic;

public abstract class State<T>
{
    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnFixedUpdate();
    public abstract void OnExit();

    protected StateMachine<T> stateMachine { get; }
    protected List<Transition<T>> transitions = new List<Transition<T>>();

    protected State(StateMachine<T> _stateMachine)
    {
        this.stateMachine = _stateMachine;
    }

    public virtual void AddTransition(Transition<T> _transition)
    {
        transitions.Add(_transition);
    }
}