using UnityEngine;

public class GameManager : MonoBehaviour
{
    //The only one with MonoBehaviour

    public GameObject PlayerInstance;

    private Player player;
    private StateMachine<GameManager> stateMachine;
    
    private void Start()
    {
        stateMachine = new StateMachine<GameManager>(this);

        player = new Player(this, PlayerInstance, transform);
        player.OnEnter();
    }

    private void Update()
    {
        player.OnUpdate();
    }
}