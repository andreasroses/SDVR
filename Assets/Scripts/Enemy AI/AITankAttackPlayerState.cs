using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AITankAttackPlayerState : AIShootPlayerState
{
    private float maxTime;
    private float stopDistance;
    private float timer = 5.0f;
    private Vector3 playerDirection;
    public override void Enter(AIAgent agent){
        base.Enter(agent);
        maxTime = agent.config.maxTime;
        stopDistance = agent.config.stopDistance;
    }

    public override void Update(AIAgent agent){
        playerDirection = playerTransform.position - enemyTransform.position;
        if (enemyNavMesh.Raycast(playerTransform.position, out currHit) && agent.enemyGun.ShootingMode()){
            agent.enemyGun.SwitchShootingMode();
        }
        else if(!agent.enemyGun.ShootingMode() && playerDirection.sqrMagnitude < (maxDistanceFromPlayer * maxDistanceFromPlayer)){
            agent.enemyGun.SwitchShootingMode();
        }
        playerDirection.y = 0;
        Aim();
        if(!agent.enabled){
            return;
        }
        timer -= Time.deltaTime;
        if(!enemyNavMesh.hasPath ){//if agent's path hasn't been set, it's set now
            enemyNavMesh.destination = playerTransform.position; 
            enemyNavMesh.stoppingDistance = stopDistance;   
        }
        if(timer < 0.0f){ //after timer runs out, destination is set again IF player is far enough away
            if(playerDirection.sqrMagnitude > (minDistanceFromPlayer * minDistanceFromPlayer)){ //checks distance away from player by taking the area of it
                enemyNavMesh.destination = playerTransform.position; 
                enemyNavMesh.stoppingDistance = stopDistance;   
            }
            timer = maxTime;
        }
    }

}
