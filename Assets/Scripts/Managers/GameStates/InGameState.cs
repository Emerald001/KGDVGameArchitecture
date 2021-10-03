using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class InGameState : GameState
{
    private Player player;
    private LevelGenerator levelGenerator;

    public ObjectPool<Bullet> bulletPool;
    public ObjectPool<EnemyAI> enemyPool;
    private List<Vector3> enemySpawnPoints;

    private UImanager uiManager;

    private int enemyAmount = 20;

    private List<GunModifier> gunModifiers;

    private Camera playerCam;
    private float playerSpeed;

    private Vector2Int size;
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

        enemyPool = new ObjectPool<EnemyAI>();
        enemySpawnPoints = levelGenerator.GetRandomFloorPositions(enemyAmount);

        foreach (var spawnPoint in enemySpawnPoints)
        {
            var enemyObject = enemyPool.RequestObject();
            enemyObject.OnStart(spawnPoint, player.currentPlayer.transform, this);
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
        if (paused) return;
        
        player.OnFixedUpdate();
        bulletPool.RunFixedUpdate();
        enemyPool.RunFixedUpdate();
    }

    public override void OnExit()
    {
        levelGenerator = null;
        player.OnExit();
        player = null;
    }
}
