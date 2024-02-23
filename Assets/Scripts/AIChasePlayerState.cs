/*  AIChasePlayerState
*   Currently this player state only rotates the GameObject passed in towards the player, but it can easily be changed to its NavMesh, or GameObject holding its animation parts (i don't know the term for that yet,,,)
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

    public void Update(AIAgent agent)
    {
        if(!agent.enabled){
            return;
        }
        timer -= Time.deltaTime;
        if(!agent.navMeshAgent.hasPath){
            agent.navMeshAgent.destination = playerTransform.position; 
            agent.navMeshAgent.stoppingDistance = agent.config.stopDistance;   
        }
        else if(!agent.navMeshAgent.pathPending && agent.navMeshAgent.remainingDistance <= agent.navMeshAgent.stoppingDistance){
            agent.stateMachine.ChangeState(AIStateID.ShootPlayer);
        }
        if(timer < 0.0f){
            Vector3 direction = playerTransform.position - agent.transform.position;
            direction.y= 0;
            if(direction.sqrMagnitude > agent.config.minDistanceFromPlayer *agent.config.minDistanceFromPlayer){
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
