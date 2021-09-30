using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State<T>
{
    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnFixedUpdate();
    public abstract void OnExit();

    public StateMachine<T> stateMachine { get; protected set; }
    public List<Transition<T>> transitions = new List<Transition<T>>();

    public State(StateMachine<T> _stateMachine)
    {
        this.stateMachine = _stateMachine;
    }

    public virtual void AddTransition(Transition<T> _transition)
    {
        transitions.Add(_transition);
    }
}