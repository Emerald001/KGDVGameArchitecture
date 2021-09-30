using UnityEngine;
using UnityEngine.Tilemaps;

public class InGameState : GameState
{
    private Player player;
    private LevelGenerator levelGenerator;
    private ObjectPooler objectPooler;
    private ObjectPool<Bullet> objectPoolers;

    private GameObject bullet;
    private int bulletCount;

    public GameObject playerInstance;
    public Camera playerCam;
    public Vector3 spawnpoint;
    public float playerSpeed;

    public Vector2Int size;
    private Tilemap tilemap;
    private Tile ground;
    private Tile wall;

    public InGameState(
        StateMachine<GameManager> _stateMachine,
        GameObject _playerInstance,
        Camera _playerCam,
        Vector3 _spawnpoint,
        float _playerSpeed,

        GameObject _bullet,
        int _bulletCount,

        Vector2Int _size,
        Tilemap _tilemap,
        Tile _ground,
        Tile _wall
        ) : base(_stateMachine) 
    {
        this.playerInstance = _playerInstance;
        this.playerCam = _playerCam;
        this.spawnpoint = _spawnpoint;
        this.playerSpeed = _playerSpeed;

        this.bullet = _bullet;
        this.bulletCount = _bulletCount;

        this.size = _size;
        this.tilemap = _tilemap;
        this.ground = _ground;
        this.wall = _wall;
    }

    public override void OnEnter()
    {
        levelGenerator = new LevelGenerator(size);
        levelGenerator.OnEnter();
        levelGenerator.SetTilemap(tilemap, ground, wall);

        objectPooler = new ObjectPooler("Bullet", bullet, bulletCount);
        objectPooler.OnStart();

        objectPoolers = new ObjectPool<Bullet>();
        
        Debug.Log(levelGenerator.spawnPoint);
        player = new Player(playerInstance, playerCam, levelGenerator.spawnPoint, playerSpeed);
        player.OnEnter();
    }

    public override void OnUpdate()
    {
        player.OnUpdate();
        base.OnUpdate();
    }

    public override void OnFixedUpdate()
    {
        player.OnFixedUpdate();
    }

    public override void OnExit()
    {
        levelGenerator = null;
        player.OnExit();
        player = null;
    }
}
