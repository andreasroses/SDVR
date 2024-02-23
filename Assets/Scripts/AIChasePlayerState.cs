/*  AIChasePlayerState
*   AI chases the player until they're about 5 units away, after which the AI enters the shooting state.
*   
*   References code by TheKiwiCoder: https://youtu.be/1H9jrKyWKs0?si=PL0S4V7rFnJ5V1fc
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AIChasePlayerState : AIState
{
    public Transform playerTransform;

    private float timer = 5.0f;
    public AIStateID GetID()
    {
        return AIStateID.ChasePlayer;
    }
    public void Enter(AIAgent agent)
    {
        if(playerTransform == null){
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    //Timer is used so .destination isn't called so often as it's an expensive operation.
    public void Update(AIAgent agent)
    {
        if(!agent.enabled){
            return;
        }
        timer -= Time.deltaTime;
        if(!agent.navMeshAgent.hasPath){//if agent's path has been set, it's set now
            agent.navMeshAgent.destination = playerTransform.position; 
            agent.navMeshAgent.stoppingDistance = agent.config.stopDistance;   
        }
        else if(!agent.navMeshAgent.pathPending && agent.navMeshAgent.remainingDistance <= agent.navMeshAgent.stoppingDistance){
            //if agent's position reaches destination of ~5 units away or has surpassed it, the AI state changes to shoot
            agent.stateMachine.ChangeState(AIStateID.ShootPlayer);
        }
        if(timer < 0.0f){ //after timer runs out, destination is set again IF player is far enough away
            Vector3 direction = playerTransform.position - agent.transform.position;
            direction.y= 0;
            if(direction.sqrMagnitude > agent.config.minDistanceFromPlayer *agent.config.minDistanceFromPlayer){ //checks distance away from player by taking the area of it
                agent.navMeshAgent.destination = playerTransform.position; 
                agent.navMeshAgent.stoppingDistance = agent.config.stopDistance;   
            }
            timer = agent.config.maxTime;
        }
    }

    public void Exit(AIAgent agent)
    {
        Debug.Log("exiting ChasePlayer");
    }



}
