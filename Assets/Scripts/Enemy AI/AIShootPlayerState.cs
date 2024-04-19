/*  AIShootPlayerState
*   AI stops movement and shoots. 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShootPlayerState : AIState
{
    private Transform playerTransform;
    private Transform enemyTransform;
    private Transform weaponTransform;
    private Transform aimTransform;
    private float minDistanceFromPlayer;
    private float rotationSpeed;
    public virtual AIStateID GetID()
    {
        return AIStateID.ShootPlayer;
    }
    public virtual void Enter(AIAgent agent){
        playerTransform = agent.playerTransform;
        aimTransform = agent.enemyGun.GetMuzzle();
        minDistanceFromPlayer = agent.config.minDistanceFromPlayer;
        rotationSpeed = agent.config.rotationSpeed;
        enemyTransform = agent.enemyTransform;
        weaponTransform = agent.weaponTransform;
        agent.enemyGun.SwitchShootingMode(); //turns on shooting
    }

    public virtual void Update(AIAgent agent){//aims at and shoots player
        if(playerTransform != null ){
            Vector3 playerDirection = playerTransform.position - aimTransform.position;
            Aim(playerDirection);
            
            if(playerDirection.sqrMagnitude > (minDistanceFromPlayer*minDistanceFromPlayer)){
                agent.enemyGun.SwitchShootingMode();//turns off shooting
                agent.stateMachine.ChangeState(AIStateID.ChasePlayer);
            }
        }
    }

    public virtual void Exit(AIAgent agent){
        Debug.Log("exiting ShootPlayer");
    }

    private void Aim(Vector3 playerDirection){
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
