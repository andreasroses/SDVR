using Kitbashery.Gameplay;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public enum State
    {
        PATROL,
        CHASE,
        ATTACK
    }

    private NavMeshAgent agent;
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask whatIsGround, playerLayer;
    [SerializeField] private Vector3 walkPoint;
    [SerializeField] private float walkPointRange, timePerAttack, sightRange, attackRange, bulletSpeed;

    public Health health;
    [SerializeField] private State state;
    private bool walkPointSet, justAttacked;
    [SerializeField] private WeaponBase weapon;


    void Start()
    {
        state = State.PATROL;
        player = GameObject.Find("VR Player").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        health = GetComponent<Health>();
        weapon = GetComponentInChildren<WeaponBase>();
    }

    void Update()
    {
        if (health.hitPoints <= 0)
        {
            OnDeath();
        }
        bool playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        bool playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if (!playerInSightRange && !playerInAttackRange)
        {
            Patroling();
            state = State.PATROL;
        }

        if (playerInSightRange && !playerInAttackRange)
        {
            state = State.CHASE;
            ChasePlayer();
        }

        if (playerInAttackRange && playerInSightRange)
        {
            AttackPlayer();
            state = State.ATTACK;
        }

    }

    private void Patroling()
    {

        if (!walkPointSet)
            SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        if (!justAttacked)
        {
            // Attack code here
            transform.LookAt(player);

            weapon.Shoot();

            justAttacked = true;
            Invoke(nameof(ResetAttack), timePerAttack);
        }
    }

    private void ResetAttack()
    {
        justAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    public void OnDeath()
    {
        gameObject.SetActive(false);
    }
}
