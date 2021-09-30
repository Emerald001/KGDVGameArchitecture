using UnityEngine;

public class Player
{
    private GameObject player;
    private GameObject currentPlayer;
    private Camera playerCam;
    private PlayerMovement playerMovement;
    private Vector3 spawnpoint;

    //player Settings
    private float speed;

    public Player( GameObject _player, Camera _playerCam, Vector3 _spawnPoint, float _speed)
    {
        this.playerCam = _playerCam;
        this.player = _player;
        this.spawnpoint = _spawnPoint;
        this.speed = _speed;
    }

    public void OnEnter()
    {
        currentPlayer = Object.Instantiate(player, spawnpoint, Quaternion.identity);

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
        Object.Destroy(currentPlayer);
        currentPlayer = null;
    }
}