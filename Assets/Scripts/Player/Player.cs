using UnityEngine;

public class Player
{
    private GameObject player;
    public GameObject currentPlayer;
    public GameObject currentGun;

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
        currentPlayer = Object.Instantiate(Resources.Load("Player") as GameObject, spawnpoint, Quaternion.identity);
        currentGun = Object.Instantiate(Resources.Load("Gunholder") as GameObject,  spawnpoint, Quaternion.identity);
        currentGun.transform.SetParent(currentPlayer.transform);

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