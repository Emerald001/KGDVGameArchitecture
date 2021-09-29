using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

public class PlayerMovement : IPhysicsComponent
{
    private GameObject player;
    private SphereCollider playerCollider = new SphereCollider();
    private Camera playerCam;
    private CameraFollow cameraFollow;
    private LayerMask collidingLayer;

    public float speed = .2f;

    public PlayerMovement( GameObject _newPlayer, Camera _playerCam, float _speed)
    {
        this.player = _newPlayer;
        this.speed = _speed;
        this.playerCam = _playerCam;
    }

    public void OnEnter()
    {
        playerCollider = player.AddComponent(typeof(SphereCollider)) as SphereCollider;
        playerCollider.radius = 1;
        playerCollider.center = player.transform.position;

        cameraFollow = new CameraFollow(playerCam, player);
        cameraFollow.OnEnter();
    }

    public void OnUpdate()
    {
        player.transform.position += Move();
        ScanSurroundings();

        cameraFollow.OnUpdate();
    }

    private Vector3 Move()
    {
        var input = InputManager.instance;

        return new Vector3(input.GetButton(KeyBindingActions.Right) - input.GetButton(KeyBindingActions.Left), input.GetButton(KeyBindingActions.Up) - input.GetButton(KeyBindingActions.Down), 0) * (speed * Time.deltaTime);
    }

    public void ScanSurroundings()
    {
        var overlaps = new Collider[10];
        var num = Physics.OverlapSphereNonAlloc(player.transform.TransformPoint(player.transform.position), 1, overlaps, collidingLayer);

        for (var i = 0; i < num; i++)
        {
            var t = overlaps[i].transform;
            if (t == null)
            {
                continue;
            }
            if (!Physics.ComputePenetration(playerCollider, player.transform.position, player.transform.rotation, overlaps[i], t.position, t.rotation, out var dir, out var dist))
            {
                continue;
            }
            player.transform.position += dir * dist;
        }
    }
}