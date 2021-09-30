using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InGameState : GameState
{
    private Player player;
    private LevelGenerator levelGenerator;
    
    public GameObject playerInstance;
    public Camera playerCam;
    public Transform spawnpoint;
    public float playerSpeed;

    public Vector2Int size;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tile ground;
    [SerializeField] private Tile wall;

    public InGameState(
        StateMachine<GameManager> _stateMachine,
        GameObject _playerInstance,
        Camera _playerCam,
        Transform _spawnpoint,
        float _playerSpeed,

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

        player = new Player(playerInstance, playerCam, spawnpoint, playerSpeed);
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
