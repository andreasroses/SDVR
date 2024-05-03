/*  AIStateMachine
*   This class stores and manages the states of each AI agent.
*   Code by TheKiwiCoder: https://youtu.be/1H9jrKyWKs0?si=PL0S4V7rFnJ5V1fc
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AIStateMachine
{
    public AIState[] states;
    public AIAgent agent;
    public AIStateID currentState;
    public AIStateMachine(AIAgent agent){
        this.agent = agent;
        int numStates = System.Enum.GetNames(typeof(AIStateID)).Length;
        states = new AIState[numStates];
    }

    public void RegisterState(AIState state){
        int index = (int)state.GetID();
        states[index] = state;
    }

    public AIState GetState(AIStateID stateID){
        int index = (int) stateID;
        return states[index];
    }
    public void Update(){
        GetState(currentState)?.Update(agent);
    }

    public void ChangeState(AIStateID newState){
        GetState(currentState)?.Exit(agent);
        currentState = newState;
        GetState(currentState)?.Enter(agent);
    }

    public void StartMachine(AIStateID initialState){
        GetState(initialState)?.Enter(agent);
        currentState = initialState;
    }
}
