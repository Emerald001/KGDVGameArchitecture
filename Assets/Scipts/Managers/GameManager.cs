using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    //The only one with MonoBehaviour

    [Header("PlayerSettings")]
    public GameObject playerInstance;
    public Camera playerCam;
    public float playerSpeed;
    private Player player;

    [Header("GenerationSettings")] 
    public Vector2Int size = Vector2Int.one * 50;

    private LevelGenerator levelGenerator;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tile ground;
    [SerializeField] private Tile wall;

    private StateMachine<GameManager> stateMachine;
    
    private void Start()
    {
        stateMachine = new StateMachine<GameManager>(this);

        player = new Player(playerInstance, playerCam, transform, playerSpeed);
        player.OnEnter();

        levelGenerator = new LevelGenerator(size);
        levelGenerator.OnEnter();
        levelGenerator.SetTilemap(tilemap, ground, wall);
    }

    private void Update()
    {
        player.OnUpdate();
    }
}