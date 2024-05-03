using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AITankAgent : AIAgent
{
    protected override void RegisterAgentStates(){
        stateMachine.RegisterState(new AIChasePlayerState());
        stateMachine.RegisterState(new AITankAttackPlayerState());
    }
}
