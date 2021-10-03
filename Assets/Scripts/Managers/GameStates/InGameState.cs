using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class InGameState : GameState
{
    private Player player;
    private LevelGenerator levelGenerator;

    public ObjectPool<Bullet> bulletPool;
    public ObjectPool<EnemyAI> enemypool;
    private List<Vector3> enemySpawnPoints;

    public UImanager uiManager;

    public int enemyAmount = 20;

    public List<GunModifier> gunModifiers;

    public Camera playerCam;
    public float playerSpeed;

    public Vector2Int size;
    private Tilemap tilemap;
    private Tile ground;
    private Tile wall;

    private bool paused;

    public InGameState(
        StateMachine<GameManager> _stateMachine,
        Camera _playerCam,
        float _playerSpeed,

        List<GunModifier> _gunModifiers,

        Vector2Int _size,
        Tilemap _tilemap,
        Tile _ground,
        Tile _wall
        ) : base(_stateMachine) 
    {
        this.playerCam = _playerCam;
        this.playerSpeed = _playerSpeed;

        this.gunModifiers = _gunModifiers;

        this.size = _size;
        this.tilemap = _tilemap;
        this.ground = _ground;
        this.wall = _wall;
    }

    public override void OnEnter()
    {
        var tmpCanvas = GameObject.Instantiate(Resources.Load("AmmoCanvas") as GameObject);

        uiManager = new UImanager(tmpCanvas.GetComponentInChildren<Text>());
        uiManager.OnEnter();

        bulletPool = new ObjectPool<Bullet>();

        levelGenerator = new LevelGenerator(size);
        levelGenerator.OnEnter();
        levelGenerator.SetTilemap(tilemap, ground, wall);

        player = new Player(this, playerCam, gunModifiers, levelGenerator.spawnPoint, playerSpeed);
        player.OnEnter();

        enemypool = new ObjectPool<EnemyAI>();
        enemySpawnPoints = levelGenerator.GetRandomFloorPositions(enemyAmount);

        for (var i = 0; i < enemySpawnPoints.Count; i++)
        {
            var enemyObject = enemypool.RequestObject();
            enemyObject.OnStart(enemySpawnPoints[i], player.currentPlayer.transform, this);
        }
    }

    public override void OnUpdate()
    {
        if (!paused)
        {
            player.OnUpdate();
            base.OnUpdate();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            paused = !paused;
        }
    }

    public override void OnFixedUpdate()
    {
        if (!paused)
        {
            player.OnFixedUpdate();
            bulletPool.RunFixedUpdate();
            enemypool.RunFixedUpdate();
        }
    }

    public override void OnExit()
    {
        levelGenerator = null;
        player.OnExit();
        player = null;
    }
}
