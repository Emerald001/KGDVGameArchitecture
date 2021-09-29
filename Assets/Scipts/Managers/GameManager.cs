using UnityEngine;

public class GameManager : MonoBehaviour
{
    //The only one with MonoBehaviour

    [Header("PlayerSettings")]
    public GameObject playerInstance;
    public Camera playerCam;
    public float playerSpeed;
    private Player player;


    private StateMachine<GameManager> stateMachine;
    
    private void Start()
    {
        stateMachine = new StateMachine<GameManager>(this);

        player = new Player(playerInstance, playerCam, transform, playerSpeed);
        player.OnEnter();
    }

    private void Update()
    {
        player.OnUpdate();
    }
}