using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AIPatrolState : AIState{
    private Transform playerTransform;
    
    private List<Vector3> patrolPoints = new List<Vector3>();
    private Transform enemyTransform;
    private NavMeshAgent enemyNavMesh;
    private float maxDistanceFromPlayer;
    private float idleTime;
    private int destPoint = 0;
    private float waypointRange;
    private float agentHeight;
    private float timer;
    private NavMeshHit currHit;
    public AIStateID GetID(){
        return AIStateID.Patrol;
    }
    public void Enter(AIAgent agent){
        playerTransform = agent.playerTransform;
        enemyTransform = agent.enemyTransform;
        enemyNavMesh = agent.navMeshAgent;
        maxDistanceFromPlayer = agent.config.minDistanceFromPlayer;
        idleTime = agent.config.IdleTime;
        timer = idleTime;
        waypointRange= agent.config.WaypointRange;
        agentHeight = agent.config.AgentHeight;
        SetWaypoints();
    }

    public void Update(AIAgent agent){
        timer -= Time.deltaTime;
        Vector3 playerDistance = playerTransform.position - enemyTransform.position;
        playerDistance.y= 0;
        if(playerDistance.sqrMagnitude < (maxDistanceFromPlayer * maxDistanceFromPlayer) && !enemyNavMesh.Raycast(playerTransform.position, out currHit)){ 
            enemyNavMesh.ResetPath();
            agent.stateMachine.ChangeState(AIStateID.ChasePlayer);
        }
        if(timer < 0.0f){
            if (!enemyNavMesh.pathPending && enemyNavMesh.remainingDistance < 0.5f){
                Debug.Log("going to next point");
                GotoNextPoint();
            }
            timer = idleTime;
        }
    }
    public void Exit(AIAgent agent){
        return;
    }

    private void GotoNextPoint(){
        if (patrolPoints.Count == 0)
            return;

        // Set the agent's destination to the current patrol point
        enemyNavMesh.destination = patrolPoints[destPoint];

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % patrolPoints.Count;
    }
    
    private bool GetWaypoint(Vector3 center, float range, out Vector3 result){
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, agentHeight*2, NavMesh.AllAreas)){
            result = hit.position;
            return true;
        }
        result = Vector3.zero;
        return false;
    }

    private void SetWaypoints(){
        Debug.Log("Setting Waypoints");
        int counter = 0;
        Vector3 waypoint;
        while(counter < 3){
            if(GetWaypoint(enemyTransform.position,waypointRange,out waypoint)){
                Debug.Log(counter);
                patrolPoints.Add(waypoint);
                counter++;
            }
            Debug.Log(waypoint);
        }
        Debug.Log("AIPatrolState: " + counter);
    }
}
