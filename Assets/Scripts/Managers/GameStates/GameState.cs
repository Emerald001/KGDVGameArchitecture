public class GameState : State<GameManager>
{
    public StateMachine<GameManager> owner;

    protected GameState(StateMachine<GameManager> _stateMachine) : base(_stateMachine) { }

    public override void OnEnter() { }

    public override void OnExit() { }

    public override void OnUpdate()
    {
        foreach (var transition in transitions)
        {
            if (!transition.condition.Invoke(stateMachine.controller)) continue;
            
            stateMachine.SwitchState(transition.toState);
            return;
        }
    }

    public override void OnFixedUpdate() { }
}
