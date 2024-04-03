using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [HideInInspector]
    public int current_level = 1, next_level = 2;
    [SerializeField]
    public RoguelikeGeneratorPro.RoguelikeGeneratorPro roguelikeGeneratorPro;
    [SerializeField]
    public int roomIncreaseFrequency, roomIncreaseNumber;
    [SerializeField] public GameObject playerSpawnPoint, randomWallParent, enemySpawnPoint;
    public int raycastDistance = 100; // Distance to cast the ray for spawn
    public LayerMask obstacleLayer;

    private List<GameObject> eSpawnPoints;

    private void Start()
    {
        
    }

    void Update()
    {
        // Tempoprary keybind for level generation while testing
        if (Input.GetKeyDown(KeyCode.P))
        {
            GoToNextLevel();
        }else if (Input.GetKeyDown(KeyCode.G)){
            Debug.Log(roguelikeGeneratorPro.FindSpawnRoomLocation());
        }
            
    }

    
    public void GoToNextLevel()
    {
        Debug.Log($"Level {current_level} Completed. Generating Level {next_level}");
        
        // *temp* Increase room size each level
        if (next_level % roomIncreaseFrequency-1 == 0)
        {
            Debug.Log($"Progressing to {next_level}th level, increasing room dimensions by {roomIncreaseNumber}");
            roguelikeGeneratorPro.levelSize.x+=roomIncreaseNumber;
            roguelikeGeneratorPro.levelSize.y+=roomIncreaseNumber;
        }

        roguelikeGeneratorPro.RigenenerateLevel();

        current_level = next_level;
        next_level++;
    }
}
