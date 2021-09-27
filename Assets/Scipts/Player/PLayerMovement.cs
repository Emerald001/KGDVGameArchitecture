using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement
{
    private GameObject player;
    private Transform spawnpoint;

    public PlayerMovement(GameObject player, Transform SpawnPoint)
    {
        this.player = player;
        this.spawnpoint = SpawnPoint;
    }

    public void OnEnter()
    {
        //set player.transform.position = spawnpoint.position;
    }
    public void OnExit()
    {

    }

    public void OnUpdate()
    {
        
    }
}