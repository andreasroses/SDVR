using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDroneShootPlayerState : AIShootPlayerState
{
    private Transform playerTransform;
    private Transform enemyTransform;
    private Transform aimTransform;
    private Vector3 agentDestination;
    private float rotationSpeed;
    private float radius = 2f;
    private float speed = 2f;
    private float angle = 0f;

    public override void Enter(AIAgent agent){
        if(playerTransform == null){
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
        aimTransform = agent.enemyGun.GetMuzzle();
        enemyTransform = agent.transform;
        rotationSpeed = agent.config.rotationSpeed;
        agent.enemyGun.SwitchShootingMode(); //turns on shooting
        radius = agent.config.droneRadius;
        speed = agent.config.droneSpeed;
        angle = agent.config.droneAngle;
        agent.navMeshAgent.ResetPath();
    }

    public override void Update(AIAgent agent){//circles player and shoots
        if(playerTransform != null ){
            Aim();
            float xOffset = Mathf.Cos(angle) * radius;
            float zOffset = Mathf.Sin(angle) * radius;

            agentDestination = playerTransform.position + new Vector3(xOffset, 0f, zOffset);
            enemyTransform.position = Vector3.Slerp(enemyTransform.position, agentDestination, Time.deltaTime * speed);

            angle += speed * Time.deltaTime;

            angle %= Mathf.PI * 2f;
        }
        
    }

    private void Aim(){
        Vector3 aimDirection = aimTransform.forward;
        Vector3 playerDirection = playerTransform.position - aimTransform.position;
        Quaternion targetRotation = Quaternion.FromToRotation(aimDirection, playerDirection);
        enemyTransform.rotation = targetRotation * enemyTransform.rotation;
    }
}
