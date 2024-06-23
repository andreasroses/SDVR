using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using System;

public class PickUpHand : MonoBehaviour
{
    [Header("Hand Settings")]
    public float distToPickup = 0.5f;  
    bool isHolding = false;
    public LayerMask pickupLayer;

    [Header("Controller References")]
    public InputActionReference gripAction;
    public InputActionReference triggerAction;

    private float gripValue;
    private float triggerValue;

    Rigidbody holdingTarget;

    private void OnEnable()
    {
        gripAction.action.performed += GripAction_performed;
        triggerAction.action.performed += TriggerAction_performed;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
            // Drop the object
        if(gripValue < 0.5f)
            isHolding = false;
        else
            isHolding = true;


        //checks to see if there are any objects in the area to pick up, if so we pick up an object that is a rigidbody
        if(!isHolding)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, distToPickup, pickupLayer);
            if(colliders.Length > 0)
                    holdingTarget = colliders[0].transform.root.GetComponent<Rigidbody>();
            else
                holdingTarget = null;
        }
        else
        {
            if(holdingTarget)
            {
                //move the rigidbody to our hand using the angular velocity
                holdingTarget.velocity = (transform.position - holdingTarget.position)/Time.fixedDeltaTime;

                //rotate the rigidbody to match the hand
                holdingTarget.maxAngularVelocity = 20;
                Quaternion deltaRotation = transform.rotation * Quaternion.Inverse(holdingTarget.transform.rotation);
                Vector3 eulerRotation = new Vector3(Mathf.DeltaAngle(0, deltaRotation.eulerAngles.x), 
                    Mathf.DeltaAngle(0, deltaRotation.eulerAngles.y), Mathf.DeltaAngle(0, deltaRotation.eulerAngles.z));
                eulerRotation *= 0.95f;
                eulerRotation *= Mathf.Deg2Rad;
                holdingTarget.angularVelocity = eulerRotation / Time.fixedDeltaTime;
            }
        }
        
    }


    //Gets input from the controller    
    private void TriggerAction_performed(InputAction.CallbackContext context)
    {
        triggerValue = context.ReadValue<float>();
    }

    private void GripAction_performed(InputAction.CallbackContext context)
    {
        gripAction.action.ReadValue<float>();
    }
}
