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
    private Transform playerTransform;
    private Transform enemyTransform;
    private NavMeshAgent enemyNavMesh;
    private float minDistanceFromPlayer;
    private float maxTime;
    private float stopDistance;

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
        enemyTransform = agent.enemyTransform;
        enemyNavMesh = agent.navMeshAgent;
        minDistanceFromPlayer = agent.config.minDistanceFromPlayer;
        maxTime = agent.config.maxTime;
        stopDistance = agent.config.stopDistance;
    }

    //Timer is used so .destination isn't called so often as it's an expensive operation.
    public void Update(AIAgent agent)
    {
        if(!agent.enabled){
            Debug.Log("AIChasePlayerState: agent not enabled");
            return;
        }
        timer -= Time.deltaTime;
        if(!enemyNavMesh.hasPath){//if agent's path hasn't been set, it's set now
            enemyNavMesh.destination = playerTransform.position; 
            enemyNavMesh.stoppingDistance = stopDistance;   
        }
        else if(!enemyNavMesh.pathPending && (enemyNavMesh.remainingDistance <= stopDistance)){
            //if agent's position reaches destination of ~5 units away or has surpassed it, the AI state changes to shoot
            Debug.Log("ChaseState: no path pending, close to player, switching state!");
            agent.stateMachine.ChangeState(AIStateID.ShootPlayer);
        }
        if(timer < 0.0f){ //after timer runs out, destination is set again IF player is far enough away
            Vector3 direction = playerTransform.position - enemyTransform.position;
            direction.y= 0;
            if(direction.sqrMagnitude > (minDistanceFromPlayer * minDistanceFromPlayer)){ //checks distance away from player by taking the area of it
                enemyNavMesh.destination = playerTransform.position; 
                enemyNavMesh.stoppingDistance = stopDistance;   
            }
            timer = maxTime;
        }
    }

    public void Exit(AIAgent agent)
    {
        Debug.Log("exiting ChasePlayer");
    }



}
