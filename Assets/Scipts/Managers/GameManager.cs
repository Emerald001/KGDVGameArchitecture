using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //The only one with MonoBehaviour

    public GameObject PlayerInstance;

    private PlayerMovement playerMovement;
    private StateMachine<GameManager> stateMachine;
    private void Start()
    {
        stateMachine = new StateMachine<GameManager>(this);

        playerMovement = new PlayerMovement(PlayerInstance, transform);
        playerMovement.OnEnter();
    }

    // Update is called once per frame
    private void Update()
    {
        playerMovement.OnUpdate();
    }
}