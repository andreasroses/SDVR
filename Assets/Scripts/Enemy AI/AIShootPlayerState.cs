/*  AIShootPlayerState
*   AI stops movement and shoots. 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AIShootPlayerState : AIState
{
    protected Transform playerTransform;
    protected Transform enemyTransform;
    protected Transform weaponTransform;
    protected Transform aimTransform;
    protected NavMeshAgent enemyNavMesh;
    protected NavMeshHit currHit;
    protected float minDistanceFromPlayer;
    protected float maxDistanceFromPlayer;
    protected float rotationSpeed;
    public virtual AIStateID GetID()
    {
        return AIStateID.ShootPlayer;
    }
    public virtual void Enter(AIAgent agent){
        playerTransform = agent.playerTransform;
        aimTransform = agent.enemyGun.GetMuzzle();
        minDistanceFromPlayer = agent.config.minDistanceFromPlayer;
        maxDistanceFromPlayer = agent.config.maxDistanceFromPlayer;
        rotationSpeed = agent.config.rotationSpeed;
        enemyTransform = agent.enemyTransform;
        enemyNavMesh = agent.navMeshAgent;
        weaponTransform = agent.weaponTransform;
        if(!agent.enemyGun.ShootingMode()){
            agent.enemyGun.SwitchShootingMode();//turns on shooting
        }
    }

    public virtual void Update(AIAgent agent){//aims at and shoots player
        if(playerTransform != null ){
            Vector3 playerDirection = playerTransform.position - aimTransform.position;
            if (enemyNavMesh.Raycast(playerTransform.position, out currHit) && agent.enemyGun.ShootingMode()){
                agent.enemyGun.SwitchShootingMode();
            }
            else if(!agent.enemyGun.ShootingMode() && playerDirection.sqrMagnitude < (maxDistanceFromPlayer * maxDistanceFromPlayer)){
                agent.enemyGun.SwitchShootingMode();
            }
            Aim();
            
            if(playerDirection.sqrMagnitude > (minDistanceFromPlayer*minDistanceFromPlayer)){
                agent.stateMachine.ChangeState(AIStateID.ChasePlayer);
            }
        }
    }

    public virtual void Exit(AIAgent agent){
        if(agent.enemyGun.ShootingMode()){
            agent.enemyGun.SwitchShootingMode();//turns off shooting
        }
        weaponTransform.rotation = Quaternion.identity;
    }

    protected virtual void Aim(){
        //aims muzzle
        Vector3 aimDirection = aimTransform.forward;
        Vector3 aimDistance = playerTransform.position - aimTransform.position;
        Quaternion targetRotation = Quaternion.FromToRotation(aimDirection, aimDistance);
        weaponTransform.rotation = targetRotation * weaponTransform.rotation;

        //rotates enemy
        Vector3 enemyRotate = playerTransform.position - enemyTransform.position;
        enemyRotate.y = 0;
        Quaternion enemyRotation = Quaternion.LookRotation(enemyRotate);
        enemyTransform.rotation = Quaternion.Slerp(enemyTransform.rotation, enemyRotation, Time.deltaTime * rotationSpeed);
    }
}
