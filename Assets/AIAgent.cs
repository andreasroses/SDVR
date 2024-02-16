/*  AIAgent
*   This component manages the AIStateMachine, it initializes it to its initial state and applies the current behavior each update.
*   Not sure if we want to use the built-in AI library for pathfinding yet, so I commented out NavMeshAgent, and am using the transform of the GameObject directly.
*   To test, simply move a GameObject with the tag "Player", currently that object is the DummyPlayerForAITest in BasicScene
*   Code by TheKiwiCoder: https://youtu.be/1H9jrKyWKs0?si=PL0S4V7rFnJ5V1fc
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.AI;

public class AIAgent : MonoBehaviour
{
    public AIStateMachine stateMachine;
    public AIStateID initialState;

    // public NavMeshAgent navMeshAgent;

    void Start(){
        // navMeshAgent = GetComponent<NavMeshAgent>();
        stateMachine = new AIStateMachine(this);
        stateMachine.RegisterState(new AIChasePlayerState());
        stateMachine.ChangeState(initialState);
    }

    void Update(){
        stateMachine.Update();
    }
}
