using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDroneShootPlayerState : AIShootPlayerState
{
    private Vector3 agentDestination;
    private float radius = 2f;
    private float speed = 2f;
    private float angle = 0f;

    public override void Enter(AIAgent agent){
        base.Enter(agent);
        radius = agent.config.droneRadius;
        speed = agent.config.droneSpeed;
        angle = agent.config.droneAngle;
        if(!agent.enemyGun.ShootingMode()){
            agent.enemyGun.SwitchShootingMode();//turns on shooting
        }
    }

    public override void Update(AIAgent agent){//circles player and shoots
        if(playerTransform != null ){
            Aim();
            float xOffset = Mathf.Cos(angle) * radius;
            float zOffset = Mathf.Sin(angle) * radius;

            agentDestination = playerTransform.position + new Vector3(xOffset, 0f, zOffset);
            enemyTransform.position = agentDestination;

            angle += speed * Time.deltaTime;

            angle %= Mathf.PI * 2f;
        }
        
    }
}
