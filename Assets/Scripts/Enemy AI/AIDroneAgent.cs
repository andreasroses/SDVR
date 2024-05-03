using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AIDroneAgent : AIAgent
{
    protected override void RegisterAgentStates(){
        stateMachine.RegisterState(new AIPatrolState());
        stateMachine.RegisterState(new AIChasePlayerState());
        stateMachine.RegisterState(new AIDroneShootPlayerState());
        
    }

}
