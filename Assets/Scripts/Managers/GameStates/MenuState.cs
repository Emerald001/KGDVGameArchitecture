using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuState : GameState
{
    public Canvas canvasInstance;
    public Canvas tmpCanvas;
    public string key;

    public MenuState(StateMachine<GameManager> _stateMachine, Canvas _canvas, string _key) : base(_stateMachine) {
        this.canvasInstance = _canvas;
        this.key = _key;
    }

    public override void OnEnter()
    {
        tmpCanvas = GameObject.Instantiate(canvasInstance);
        Text text = tmpCanvas.GetComponentInChildren<Text>();
        text.text = "Press " + key + " to start";
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
