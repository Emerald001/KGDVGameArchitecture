using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteOnTimer
{
    private GameObject objectToDestroy;
    private float timer = 2;

    public DeleteOnTimer (GameObject _objectToDestroy)
    {
        this.objectToDestroy = _objectToDestroy;
    }
    
    void OnUpdate()
    {
        if(timer > 0)
        {
            timer -= 1 * Time.deltaTime;
        }
        else
        {
            GameObject.Destroy(objectToDestroy.gameObject);
        }
    }
}