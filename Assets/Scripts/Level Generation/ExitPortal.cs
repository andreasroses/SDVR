using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPortal : MonoBehaviour
{
    // Start is called before the first frame update
    private LevelManager levelManager;
    void Start()
    {
        levelManager = gameObject.GetComponentInParent<LevelManager>();     
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player")
        {
            levelManager.GoToNextLevel();
        }
    }

}
