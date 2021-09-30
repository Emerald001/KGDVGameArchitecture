using UnityEngine;
using Interfaces;

public class PlayerMovement : IPhysicsComponent
{
    private GameObject player;
    private Rigidbody2D rb;
    private BoxCollider2D playerCollider = new BoxCollider2D();
    private Camera playerCam;
    private CameraFollow cameraFollow;

    private Vector3 velocity;

    public float speed;

    public PlayerMovement(GameObject _newPlayer, Camera _playerCam, float _speed)
    {
        this.player = _newPlayer;
        this.speed = _speed;
        this.playerCam = _playerCam;
    }

    public void OnEnter()
    {
        playerCollider = player.AddComponent(typeof(BoxCollider2D)) as BoxCollider2D;
        playerCollider.size = new Vector2(1, 1);

        rb = player.AddComponent(typeof(Rigidbody2D)) as Rigidbody2D;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.WakeUp();

        cameraFollow = new CameraFollow(playerCam, player);
        cameraFollow.OnEnter();
    }

    public void OnUpdate()
    {
        cameraFollow.OnUpdate();
    }

    public void OnFixedUpdate()
    {
        rb.velocity = Move();
    }

    private Vector3 Move()
    {
        var input = InputManager.instance;

        var h = input.GetButton(KeyBindingActions.Right) - input.GetButton(KeyBindingActions.Left);
        var v = input.GetButton(KeyBindingActions.Up) - input.GetButton(KeyBindingActions.Down);

        var inputAxis = new Vector3(h, v, 0).normalized;
        
        return inputAxis * speed;
    }
}