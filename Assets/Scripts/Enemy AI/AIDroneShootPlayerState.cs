using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDroneShootPlayerState : AIState
{
    public Transform playerTransform;
    private Vector3 playerLoc;
    private Vector3 agentDestination;
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
        agent.enemyGun.SwitchShootingMode(); //turns on shooting
        playerLoc = playerTransform.position;
        radius = agent.config.droneRadius;
        speed = agent.config.droneSpeed;
        angle = agent.config.droneAngle;
    }

    public void Update(AIAgent agent){//circles player and shoots
        Aim(agent);
        float xOffset = Mathf.Cos(angle) * radius;
        float zOffset = Mathf.Sin(angle) * radius;

        agentDestination = playerTransform.position + new Vector3(xOffset, 0f, zOffset);
        agent.transform.position = Vector3.Slerp(agent.transform.position, agentDestination, Time.deltaTime * speed);

        angle += speed * Time.deltaTime;

        angle %= Mathf.PI * 2f;
    }

    public void Exit(AIAgent agent){
        Debug.Log("exiting ShootPlayer");
    }

    private void Aim(AIAgent agent){
        Vector3 playerDirection = playerTransform.position - agent.transform.position;
        Vector3 directionForRotation = playerDirection;
        Quaternion targetRotation = Quaternion.LookRotation(directionForRotation);
        agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, targetRotation, Time.deltaTime * agent.config.rotationSpeed);
    }
}
