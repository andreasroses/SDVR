using System.Collections;
using UnityEngine;
using Unity.AI.Navigation;
using TMPro;


public class LevelManager : MonoBehaviour
{
    [HideInInspector] public int current_level = 1, next_level = 2;
    [SerializeField] public RoguelikeGeneratorPro.RoguelikeGeneratorPro roguelikeGeneratorPro;
    [SerializeField] public int roomIncreaseFrequency, roomIncreaseNumber;
    [SerializeField] public GameObject spawnPortal, exitPortal;
    [SerializeField] public TextMeshProUGUI tempEnemyCounter;
    private GameObject[] enemies;
    public NavMeshSurface _navMesh;
    public static int enemiesRemaining = 0;
    public GameObject player;

    private void Start()
    {
        GetAllEnemies();
        spawnPortal = GameObject.Find("SpawnPortal(Clone)");
        exitPortal = GameObject.Find("ExitPortal(Clone)");
        exitPortal.SetActive(false);

        player.transform.position = spawnPortal.transform.position;
    }

    private void Update()
    {
        // Temporary keybind for level generation while testing
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (enemiesRemaining != 0)
            {
                KillEnemy();
                tempEnemyCounter.SetText(enemiesRemaining.ToString());
            }
            if (enemiesRemaining == 0)
            {
                exitPortal.SetActive(true);
            }
        }
    }

    public void GoToNextLevel()
    {
        Debug.Log($"Level {current_level} Completed. Generating Level {next_level}");

        // Temporary: Increase room size each level
        if (next_level % roomIncreaseFrequency - 1 == 0)
        {
            Debug.Log($"Progressing to {next_level}th level, increasing room dimensions by {roomIncreaseNumber}");
            roguelikeGeneratorPro.levelSize.x += roomIncreaseNumber;
            roguelikeGeneratorPro.levelSize.y += roomIncreaseNumber;
        }

        roguelikeGeneratorPro.RigenenerateLevel();

        current_level = next_level;
        next_level++;
        StartCoroutine(PostGeneration());
    }

    private IEnumerator PostGeneration()
    {
        yield return new WaitForSeconds(0.05f);
        GetAllEnemies();

        _navMesh.RemoveData();
        _navMesh.BuildNavMesh();

        spawnPortal = GameObject.Find("SpawnPortal(Clone)");
        exitPortal = GameObject.Find("ExitPortal(Clone)");

        exitPortal.SetActive(false);

        player.transform.position = spawnPortal.transform.position;
        player.transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    private void GetAllEnemies()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemiesRemaining = enemies.Length;
    }

    private static void KillEnemy()
    {
        enemiesRemaining--;
    }
}
