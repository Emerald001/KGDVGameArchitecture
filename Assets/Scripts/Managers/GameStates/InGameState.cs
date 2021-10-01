using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class InGameState : GameState
{
    private Player player;
    private LevelGenerator levelGenerator;

    public ObjectPooler bulletPooler;

    private GameObject bullet;
    private int bulletCount;

    public UImanager uiManager;
    public Text ammoText;

    public GameObject gunBarrel;
    public List<GunModifier> gunModifiers;

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

        Text _ammoText,

        GameObject _gunBarrel,
        List<GunModifier> _gunModifiers,

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

        this.ammoText = _ammoText;

        this.gunBarrel = _gunBarrel;
        this.gunModifiers = _gunModifiers;

        this.size = _size;
        this.tilemap = _tilemap;
        this.ground = _ground;
        this.wall = _wall;

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
