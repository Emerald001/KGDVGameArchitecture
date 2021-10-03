using UnityEngine;
using UnityEngine.UI;

public class MenuState : GameState
{
    private GameObject tmpCanvas;
    private string key;

    public MenuState(StateMachine<GameManager> _stateMachine, string _key) : base(_stateMachine) {
        this.key = _key;
    }

    public override void OnEnter()
    {
        tmpCanvas = Object.Instantiate(Resources.Load("MenuCanvas") as GameObject);
        var text = tmpCanvas.GetComponentInChildren<Text>();
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
