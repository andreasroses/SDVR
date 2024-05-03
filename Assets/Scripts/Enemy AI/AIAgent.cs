/*  AIAgent
*   This component manages the AIStateMachine, it initializes it to its initial state and applies the current behavior each update. It also
*   holds all the referenced components for AI states to use.
*   Testing object: DummyEnemy
*
*   IMPORTANT: When adding new state, make sure to call RegisterState() for it! And to add to enum list in AIState interface!
*   Code by TheKiwiCoder: https://youtu.be/1H9jrKyWKs0?si=PL0S4V7rFnJ5V1fc
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Kitbashery.Gameplay;

public class AIAgent : MonoBehaviour
{
    public AIStateMachine stateMachine;
    public AIStateID initialState;
    public AIAgentConfig config;
    public NavMeshAgent navMeshAgent;
    public Transform enemyTransform;
    public Transform weaponTransform;
    public EnemyWeapon enemyGun;
    public Transform playerTransform;

    protected virtual void Awake(){
        if(playerTransform == null){
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
        enemyGun = gameObject.GetComponent<EnemyWeapon>();
        enemyTransform = gameObject.GetComponent<Transform>();
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        
    }

    protected virtual void Start(){
        stateMachine = new AIStateMachine(this);
        RegisterAgentStates();
        stateMachine.StartMachine(initialState);
    }
    protected virtual void RegisterAgentStates(){
        stateMachine.RegisterState(new AIPatrolState());
        stateMachine.RegisterState(new AIChasePlayerState());
        stateMachine.RegisterState(new AIShootPlayerState());
    }
    protected virtual void Update(){
        stateMachine.Update();
    }

    public void Die(){
        gameObject.SetActive(false);
    }

    
}
