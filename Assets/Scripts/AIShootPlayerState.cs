using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShootPlayerState : AIState
{
    public Transform playerTransform;
    public AIStateID GetID()
    {
        return AIStateID.ShootPlayer;
    }
    public void Enter(AIAgent agent){
        if(playerTransform == null){
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
        Debug.Log("Setting canSpawn to true in Enter method.");
        Debug.Log("Spawner reference: " + agent.spawner);
        agent.spawner.canSpawn = true;
        agent.spawner.DebugCurrentWave();
    }

    public void Update(AIAgent agent){
        if(playerTransform != null ){
            Vector3 playerDirection = playerTransform.position - agent.transform.position;
            playerDirection.y = 0f;
            agent.transform.rotation = Quaternion.LookRotation(playerDirection);
        }
    }

    public void Exit(AIAgent agent){
        Debug.Log("exiting ShootPlayer");
    }

    
}
