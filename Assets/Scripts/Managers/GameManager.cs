using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    [Header("MenuSettings")]
    public KeyCode keyToStart = KeyCode.E;

    [Header("PlayerSettings")]
    public Camera playerCam;
    public float playerSpeed;

    [Header("GunSettings")]
    public List<GunModifier> gunModifiers;

    [Header("BulletSettings")]
    public GameObject bullet;
    public int bulletCount;

    [Header("GenerationSettings")] 
    public Vector2Int size = Vector2Int.one * 100;

    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tile ground;
    [SerializeField] private Tile wall;

    private StateMachine<GameManager> stateMachine;
    
    private void Start()
    {
        stateMachine = new StateMachine<GameManager>(this);

        var menuState = new MenuState(stateMachine, keyToStart.ToString());
        stateMachine.AddState(typeof(MenuState), menuState); 
        
        var inGameState = new InGameState(
            stateMachine,
            playerCam,
            playerSpeed,

            gunModifiers,

            size,
            tilemap,
            ground,
            wall );
        stateMachine.AddState(typeof(InGameState), inGameState);
        
        AddTransitionWithKey(menuState, keyToStart, typeof(InGameState));

        stateMachine.SwitchState(typeof(MenuState));

        Cursor.visible = false;
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

    private void AddTransitionWithKey(State<GameManager> _state, KeyCode _keyCode, System.Type _stateTo)
    {
        _state.AddTransition(new Transition<GameManager>(
            (_x) =>
            {
                return Input.GetKeyDown(_keyCode);
            }, _stateTo));
    }
}