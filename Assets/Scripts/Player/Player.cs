using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private InGameState owner;

    public GameObject currentPlayer;
    private GameObject currentGun;

    private Gun gun;
    private List<GunModifier> gunModifiers;

    private Camera playerCam;
    private PlayerMovement playerMovement;
    private Vector3 spawnpoint;

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

        gun = new Gun(owner, currentGun, gunModifiers);
    }

    public void OnUpdate() 
    {
        playerMovement.OnUpdate();
        gun.OnUpdate();

        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(currentGun.transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        currentGun.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
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