using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AITankAgent : AIAgent
{
    protected override void Start(){
        enemyTransform = GetComponent<Transform>();
        enemyGun = GetComponent<EnemyWeapon>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        stateMachine = new AIStateMachine(this);
        stateMachine.RegisterState(new AIChasePlayerState());
        stateMachine.RegisterState(new AITankAttackPlayerState());
        stateMachine.ChangeState(initialState);
    }
}
