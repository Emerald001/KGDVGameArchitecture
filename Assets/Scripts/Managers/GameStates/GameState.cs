using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : State<GameManager>
{
    public StateMachine<GameManager> owner;

    public GameState(StateMachine<GameManager> _stateMachine) : base(_stateMachine) { }

    public override void OnEnter()
    {
        
    }

    public override void OnExit()
    {
        
    }

    public override void OnUpdate()
    {
        foreach (Transition<GameManager> transition in transitions)
        {
            if (transition.condition.Invoke(stateMachine.Controller))
            {
                stateMachine.SwitchState(transition.toState);
                return;
            }
        }
    }

    public override void OnFixedUpdate()
    {

    }
}
