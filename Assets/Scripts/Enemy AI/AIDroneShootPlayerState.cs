using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDroneShootPlayerState : AIState
{
    private Transform playerTransform;
    private Transform enemyTransform;
    private Vector3 playerLoc;
    private Vector3 agentDestination;
    private float rotationSpeed;
    private float radius = 2f;
    private float speed = 2f;
    private float angle = 0f;
    public AIStateID GetID()
    {
        return AIStateID.DroneShootPlayer;
    }
    public void Enter(AIAgent agent){
        if(playerTransform == null){
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
        enemyTransform = agent.enemyTransform;
        agent.enemyGun.SwitchShootingMode(); //turns on shooting
        playerLoc = playerTransform.position;
        radius = agent.config.droneRadius;
        speed = agent.config.droneSpeed;
        angle = agent.config.droneAngle;
    }

    public void Update(AIAgent agent){//circles player and shoots
        Aim();
        float xOffset = Mathf.Cos(angle) * radius;
        float zOffset = Mathf.Sin(angle) * radius;

        agentDestination = playerTransform.position + new Vector3(xOffset, 0f, zOffset);
        enemyTransform.position = Vector3.Slerp(enemyTransform.position, agentDestination, Time.deltaTime * speed);

        angle += speed * Time.deltaTime;

        angle %= Mathf.PI * 2f;
    }

    public void Exit(AIAgent agent){
        Debug.Log("exiting ShootPlayer");
    }

    private void Aim(){
        Vector3 playerDirection = playerTransform.position - enemyTransform.position;
        Quaternion targetRotation = Quaternion.LookRotation(playerDirection);
        enemyTransform.rotation = Quaternion.Slerp(enemyTransform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
}
