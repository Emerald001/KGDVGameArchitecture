using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuState : GameState
{
    public Canvas canvasInstance;
    public Canvas tmpCanvas;

    public MenuState(StateMachine<GameManager> _stateMachine, Canvas _canvas) : base(_stateMachine) {
        this.canvasInstance = _canvas;
    }

    public override void OnEnter()
    {
        tmpCanvas = GameObject.Instantiate(canvasInstance);
        Text text = tmpCanvas.GetComponentInChildren<Text>();
        text.text = "Press E to start";
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
        Canvas.Destroy(tmpCanvas.gameObject);
    }
}
