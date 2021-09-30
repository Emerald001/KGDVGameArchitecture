using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private GameObject player;
    private GameObject currentPlayer;
    private Camera playerCam;
    private PlayerMovement playerMovement;
    private Transform spawnpoint;

    //player Settings
    private float speed;

    public Player( GameObject _player, Camera _playerCam, Transform _spawnPoint, float _speed)
    {
        this.playerCam = _playerCam;
        this.player = _player;
        this.spawnpoint = _spawnPoint;
        this.speed = _speed;
    }

    public void OnEnter()
    {
        currentPlayer = GameObject.Instantiate(player, spawnpoint);

        playerMovement = new PlayerMovement(currentPlayer, playerCam, speed);
        playerMovement.OnEnter();
    }

    public void OnUpdate() 
    {
        playerMovement.OnUpdate();
    }

    public void OnFixedUpdate() 
    {
        playerMovement.OnFixedUpdate();
    }

    public void OnExit()
    {
        GameObject.Destroy(currentPlayer);
        currentPlayer = null;
    }
}