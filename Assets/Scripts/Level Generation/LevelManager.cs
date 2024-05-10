using System.Collections;
using UnityEngine;
using Unity.AI.Navigation;
using TMPro;


public class LevelManager : MonoBehaviour
{
    [HideInInspector] public int current_level = 0, next_level = 1;
    [SerializeField] public RoguelikeGeneratorPro.RoguelikeGeneratorPro roguelikeGeneratorPro;
    [SerializeField] public int roomIncreaseFrequency, roomIncreaseNumber;
    [SerializeField] public GameObject spawnPortal, exitPortal;
    [SerializeField] public TextMeshProUGUI tempEnemyCounter;
    private GameObject[] enemies;
    public NavMeshSurface _navMesh;
    public static int enemiesRemaining = 0;
    public GameObject player;

    private void Awake()
    {
        //tempEnemyCounter.SetText(enemiesRemaining.ToString());
        //GoToNextLevel();
    }

    void Update()
    {
          
    }


    public void GoToNextLevel()
    {
        Debug.Log($"Level {current_level} Completed. Generating Level {next_level}");
        
        // *temp* Increase room size each level
        if (next_level % roomIncreaseFrequency-1 == 0 && current_level!=0)
        {
            Debug.Log($"Progressing to {next_level}th level, increasing room dimensions by {roomIncreaseNumber}");
            roguelikeGeneratorPro.levelSize.x+=roomIncreaseNumber;
            roguelikeGeneratorPro.levelSize.y+=roomIncreaseNumber;
            roguelikeGeneratorPro.minEnemies+=2;
            roguelikeGeneratorPro.maxEnemies+=2;
        }

        roguelikeGeneratorPro.RigenenerateLevel();

        current_level = next_level;
        next_level++;
        StartCoroutine(PostGeneration());
    }

    IEnumerator PostGeneration()
    {
        yield return new WaitForSeconds(.05f);
        GetAllEnemies();

        
        
        spawnPortal = GameObject.Find("SpawnPortal(Clone)");
        exitPortal = GameObject.Find("ExitPortal(Clone)");

        exitPortal.SetActive(false);

        player.transform.position = spawnPortal.transform.position;
        player.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

        _navMesh.RemoveData();
        _navMesh.BuildNavMesh();
    }

    public void GetAllEnemies()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemiesRemaining = enemies.Length;
        tempEnemyCounter.SetText(enemiesRemaining.ToString());
    }

    public static void KillEnemy()
    {
        enemiesRemaining--;
    }
}
