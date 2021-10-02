using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class InGameState : GameState
{
    private Player player;
    private LevelGenerator levelGenerator;

    public ObjectPooler bulletPooler;

    public UImanager uiManager;

    private GameObject bullet;
    private int bulletCount;

    public List<GunModifier> gunModifiers;

    public Camera playerCam;
    public float playerSpeed;

    public Vector2Int size;
    private Tilemap tilemap;
    private Tile ground;
    private Tile wall;

    public InGameState(
        StateMachine<GameManager> _stateMachine,
        Camera _playerCam,
        float _playerSpeed,

        GameObject _bullet,
        int _bulletCount,

        List<GunModifier> _gunModifiers,

        Vector2Int _size,
        Tilemap _tilemap,
        Tile _ground,
        Tile _wall
        ) : base(_stateMachine) 
    {
        this.playerCam = _playerCam;
        this.playerSpeed = _playerSpeed;

        this.bullet = _bullet;
        this.bulletCount = _bulletCount;

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

        bulletPooler = new ObjectPooler("Bullet", bullet, bulletCount);
        bulletPooler.OnStart();

        levelGenerator = new LevelGenerator(size);
        levelGenerator.OnEnter();
        levelGenerator.SetTilemap(tilemap, ground, wall);

        player = new Player(this, playerCam, gunModifiers, levelGenerator.spawnPoint, playerSpeed);
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
