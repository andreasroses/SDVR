using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITankAttackPlayerState : AIShootPlayerState
{
    private Transform playerTransform;
    private Transform enemyTransform;
    private Transform weaponTransform;
    private Transform aimTransform;
    private float rotationSpeed;
    private UnityEngine.AI.NavMeshAgent enemyNavMesh;
    private float minDistanceFromPlayer;
    private float maxTime;
    private float stopDistance;
    private float timer = 5.0f;
    private Vector3 playerDirection;
    public override void Enter(AIAgent agent){
        if(playerTransform == null){
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
        aimTransform = agent.enemyGun.GetMuzzle();
        enemyTransform = agent.enemyTransform;
        enemyNavMesh = agent.navMeshAgent;
        minDistanceFromPlayer = agent.config.minDistanceFromPlayer;
        rotationSpeed = agent.config.rotationSpeed;
        maxTime = agent.config.maxTime;
        stopDistance = agent.config.stopDistance;
        agent.enemyGun.SwitchShootingMode();
    }

    public override void Update(AIAgent agent){
        playerDirection = playerTransform.position - enemyTransform.position;
        playerDirection.y= 0;
        Aim();
        if(!agent.enabled){
            Debug.Log("AIChasePlayerState: agent not enabled");
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

    private void Aim(){
         //aims muzzle
        Vector3 aimDirection = aimTransform.forward;
        Quaternion targetRotation = Quaternion.FromToRotation(aimDirection, playerDirection);
        weaponTransform.rotation = targetRotation * weaponTransform.rotation;

        //rotates enemy
        Vector3 enemyRotate = playerTransform.position - enemyTransform.position;
        enemyRotate.y = 0;
        Quaternion enemyRotation = Quaternion.LookRotation(enemyRotate);
        enemyTransform.rotation = Quaternion.Slerp(enemyTransform.rotation, enemyRotation, Time.deltaTime * rotationSpeed);
    }
}
