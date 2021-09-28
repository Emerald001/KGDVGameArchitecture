using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

public class PlayerMovement : IPhysicsComponent
{
    private GameManager owner;

    private GameObject player;
    private SphereCollider playerCollider;

    public float speed = .2f;

    public PlayerMovement(GameManager _owner, GameObject _newPlayer)
    {
        this.owner = _owner;
        this.player = _newPlayer;
    }

    public void OnEnter()
    {
        playerCollider = new SphereCollider();
        playerCollider.transform.position = player.transform.position;
        playerCollider.transform.parent = player.transform;
    }

    public void OnExit()
    {

    }

    public void OnUpdate()
    {
        player.transform.position = new Vector3();
        ScanSurroundings();
    }

    private Vector3 Move()
    {
        var input = InputManager.instance;

        return new Vector3((input.GetButtonDown(KeyBindingActions.Left) - input.GetButtonDown(KeyBindingActions.Right)) * speed, 0 , (input.GetButtonDown(KeyBindingActions.Up) - input.GetButtonDown(KeyBindingActions.Down)) * speed);
    }

    public void ScanSurroundings()
    {
        var overlaps = new Collider[10];
        var num = Physics.OverlapSphereNonAlloc(player.transform.TransformPoint(player.transform.position), 1, overlaps);

        for (var i = 0; i < num; i++)
        {
            var t = overlaps[i].transform;
            if (!Physics.ComputePenetration(playerCollider, player.transform.position, player.transform.rotation, overlaps[i], t.position, t.rotation, out var dir, out var dist))
            {
                continue;
            }
            player.transform.position += dir * dist;
        }
    }
}