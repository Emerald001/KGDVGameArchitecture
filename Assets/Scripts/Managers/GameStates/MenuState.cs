using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuState : GameState
{
    public GameObject canvas;

    public MenuState(StateMachine<GameManager> _stateMachine) : base(_stateMachine) { }

    public override void OnEnter()
    {
        Debug.Log("Main Menu State");

        canvas = new GameObject();
        canvas.name = "Canvas";
        canvas.AddComponent<Canvas>();
        canvas.AddComponent<CanvasScaler>();
        canvas.AddComponent<GraphicRaycaster>();

        GameObject textGO = new GameObject();
        textGO.transform.parent = canvas.transform;
        Text text = textGO.AddComponent<Text>();

        text.fontSize = 48;
        text.text = "Press " + GetCondition() + " to start";
        text.alignment = TextAnchor.MiddleCenter;

        RectTransform rectTransform;
        rectTransform = text.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(0, 0, 0);
        rectTransform.sizeDelta = new Vector2(600, 200);
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
        GameObject.Destroy(canvas);
    }

    string GetCondition()
    {
        foreach (Transition<GameManager> transition in transitions)
        {
            return transition.condition.ToString();
        }
        return null;
    }
}
