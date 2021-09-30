using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : GameState
{
    public MenuState(StateMachine<GameManager> _stateMachine) : base(_stateMachine) { }

    public override void OnEnter()
    {
        Debug.Log("Main Menu State");
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
    }
    public override void OnFixedUpdate()
    {

    }
    public override void OnExit()
    {

    }
}
