using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public GameManager owner;

    private GameObject player;
    private PlayerMovement playerMovement;
    private Transform spawnpoint;

    public Player(GameManager _owner, GameObject _player, Transform _SpawnPoint)
    {
        this.owner = _owner;
        this.player = _player;
        this.spawnpoint = _SpawnPoint;
    }

    public void OnEnter()
    {
        playerMovement = new PlayerMovement(owner, GameObject.Instantiate(player, spawnpoint));

        playerMovement.OnEnter();
    }

    public void OnUpdate() 
    {
        playerMovement.OnUpdate();
    }
}