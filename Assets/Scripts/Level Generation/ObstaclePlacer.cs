using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePlacer : MonoBehaviour
{
    public GameObject[] obstacles;
    public float raycastDistance = 8f;
    public int hitThreshold = 2;

    void Start()
    {
        // CheckBlockingPaths(this.transform);
        // SpawnItems(obstacles);
    }

    private void SpawnItems(GameObject[] _items)
    {
        int randomValue = Random.Range(0, _items.Length);
        GameObject instObj = GameObject.Instantiate(_items[randomValue], this.transform.position, Quaternion.identity);

        instObj.transform.parent = this.transform;
        instObj.transform.localRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
        
    }

    private void CheckBlockingPaths(Transform obstacle)
    {
        int hitCountUp = Physics.RaycastAll(obstacle.position, obstacle.forward, raycastDistance).Length;
        int hitCountDown = Physics.RaycastAll(obstacle.position, -obstacle.forward, raycastDistance).Length;
        int hitCountRight = Physics.RaycastAll(obstacle.position, obstacle.right, raycastDistance).Length;
        int hitCountLeft = Physics.RaycastAll(obstacle.position, -obstacle.right, raycastDistance).Length;

        if (hitCountUp >= hitThreshold || hitCountDown >= hitThreshold || hitCountRight >= hitThreshold || hitCountLeft >= hitThreshold)
        {
            Debug.Log($"Destroyed obstacle at {obstacle.position}");
            Destroy(gameObject);
        }
    }
    
}
