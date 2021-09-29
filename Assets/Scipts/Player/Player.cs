using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private GameObject player;
    private Camera playerCam;
    private PlayerMovement playerMovement;
    private CameraFollow cameraFollow;
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
        playerMovement = new PlayerMovement(GameObject.Instantiate(player, spawnpoint), playerCam, speed);
        playerMovement.OnEnter();
    }

    public void OnUpdate() 
    {
        playerMovement.OnUpdate();
    }
}