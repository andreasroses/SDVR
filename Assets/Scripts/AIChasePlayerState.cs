/*  AIChasePlayerState
*   Currently this player state only rotates the GameObject passed in towards the player, but it can easily be changed to its NavMesh, or GameObject holding its animation parts (i don't know the term for that yet,,,)
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChasePlayerState : AIState
{
    public Transform playerTransform;
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
        if(playerTransform != null ){
            Vector3 playerDirection = playerTransform.position - agent.transform.position;
            playerDirection.y = 0f;
            agent.transform.rotation = Quaternion.LookRotation(playerDirection);
        }
        
    }

    public void Exit(AIAgent agent)
    {
        //Debug.Log("exiting");
    }



}
