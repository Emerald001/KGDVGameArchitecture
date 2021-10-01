using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    //The only one with MonoBehaviour
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    [Header("MenuSettings")]
    public Canvas canvas;
    public KeyCode keyToStart = KeyCode.E;

    [Header("PlayerSettings")]
    public GameObject playerInstance;

    public Camera playerCam;
    public Vector3 spawnpoint;
    public float playerSpeed;

    private Player player;
    [Header("GunSettings")]
    public GameObject gunBarrel;
    public List<GunModifier> gunModifiers;


    [Header("BulletSettings")]
    public GameObject bullet;
    public int bulletCount;

    [Header("GenerationSettings")] 
    public Vector2Int size = Vector2Int.one * 100;

    private LevelGenerator levelGenerator;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tile ground;
    [SerializeField] private Tile wall;

    private StateMachine<GameManager> stateMachine;
    
    private void Start()
    {
        stateMachine = new StateMachine<GameManager>(this);

        MenuState menuState = new MenuState(stateMachine, canvas, keyToStart.ToString());
        stateMachine.AddState(typeof(MenuState), menuState); 
        
        InGameState inGameState = new InGameState(
            stateMachine,
            playerInstance,
            playerCam,
            spawnpoint,
            playerSpeed,

            bullet,
            bulletCount,

            gunBarrel,
            gunModifiers,

            size,
            tilemap,
            ground,
            wall );
        stateMachine.AddState(typeof(InGameState), inGameState);
        
        AddTransitionWithKey(menuState, keyToStart, typeof(InGameState));

        stateMachine.SwitchState(typeof(MenuState));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            stateMachine.ReloadState();
        }

        stateMachine.RunUpdate();
    }

    private void FixedUpdate()
    {
        stateMachine.RunFixedUpdate();
    }

    public void AddTransitionWithKey(State<GameManager> _state, KeyCode _keyCode, System.Type _stateTo)
    {
        _state.AddTransition(new Transition<GameManager>(
            (_x) => {
                if (Input.GetKeyDown(_keyCode))
                    return true;
                return false;
            }, _stateTo));
    }
}