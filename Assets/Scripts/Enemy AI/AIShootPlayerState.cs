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
        agent.enemyGun.SwitchShootingMode(); //spawner is set to shoot bullets
    }

    public void Update(AIAgent agent){//rotates AI agent to look at player
        if(playerTransform != null ){
            Vector3 playerDirection = playerTransform.position - agent.transform.position;
            Vector3 directionForRotation = playerDirection;
            directionForRotation.y = 0f;
            Quaternion targetRotation = Quaternion.LookRotation(directionForRotation);
            agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, targetRotation, Time.deltaTime * agent.config.rotationSpeed);
            if(playerDirection.sqrMagnitude > (agent.config.minDistanceFromPlayer*agent.config.minDistanceFromPlayer)){
                agent.enemyGun.SwitchShootingMode();
                agent.stateMachine.ChangeState(AIStateID.ChasePlayer);
            }
        }
    }

    public void Exit(AIAgent agent){
        Debug.Log("exiting ShootPlayer");
    }

    
}
