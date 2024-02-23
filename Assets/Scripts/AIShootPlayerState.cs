/*  AIShootPlayerState
*   AI stops movement and shoots. 
*/
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
        agent.spawner.canSpawn = true; //spawner is set to shoot bullets
    }

    public void Update(AIAgent agent){//rotates AI agent to look at player
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
