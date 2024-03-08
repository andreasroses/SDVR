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
// <<<<<<< HEAD:Assets/Scripts/Enemy AI/AIShootPlayerState.cs
        agent.enemyGun.SwitchShootingMode(); //turns on shooting
// =======
        //agent.spawner.canSpawn = true; //spawner is set to shoot bullets
// >>>>>>> 039c73e5c0cf0f5cf9f16b87bf958c30069a13cd:Assets/Scripts/AIShootPlayerState.cs
    }

    public void Update(AIAgent agent){//rotates AI agent to look at player
        if(playerTransform != null ){
            Vector3 playerDirection = playerTransform.position - agent.transform.position;
            Vector3 directionForRotation = playerDirection;
            directionForRotation.y = 0f;
            Quaternion targetRotation = Quaternion.LookRotation(directionForRotation);
            agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, targetRotation, Time.deltaTime * agent.config.rotationSpeed);
            if(playerDirection.sqrMagnitude > (agent.config.minDistanceFromPlayer*agent.config.minDistanceFromPlayer)){
// <<<<<<< HEAD:Assets/Scripts/Enemy AI/AIShootPlayerState.cs
                agent.enemyGun.SwitchShootingMode();//turns off shooting
// =======
// >>>>>>> 039c73e5c0cf0f5cf9f16b87bf958c30069a13cd:Assets/Scripts/AIShootPlayerState.cs
                agent.stateMachine.ChangeState(AIStateID.ChasePlayer);
            }
        }
    }

    public void Exit(AIAgent agent){
        Debug.Log("exiting ShootPlayer");
    }

    
}
