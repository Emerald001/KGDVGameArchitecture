using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public InGameState owner;

    public GameObject currentPlayer;
    public GameObject currentGun;

    public Gun gun;
    public GameObject gunBarrel;
    public List<GunModifier> gunModifiers;

    private Camera playerCam;
    private PlayerMovement playerMovement;
    private Vector3 spawnpoint;

    //player Settings
    private float speed;

    public Player(InGameState _owner, Camera _playerCam, List<GunModifier> _gunModifiers, Vector3 _spawnPoint, float _speed)
    {
        this.owner = _owner;
        this.playerCam = _playerCam;
        this.gunModifiers = _gunModifiers;
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

        //gunBarrel = player.currentGun;
        gun = new Gun(currentGun, gunModifiers, owner.bulletPooler);
        gun.OnEnter();
    }

    public void OnUpdate() 
    {
        playerMovement.OnUpdate();
        gun.OnUpdate();
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