using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDeathState : AIState
{
    public AIStateID GetID(){
        return AIStateID.Dead;
    }
    public void Enter(AIAgent agent){
        throw new System.NotImplementedException();
    }

    public void Update(AIAgent agent){
        throw new System.NotImplementedException();
    }
    public void Exit(AIAgent agent)
    {
        throw new System.NotImplementedException();
    }
}
