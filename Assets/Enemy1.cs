using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class Enemy1 : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatisGround, whatIsPlayer;

    public float health;


    //Patrol
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange;
    public bool playerInAttackRange;

    public GameObject parryEffect;

    //Stats
    public float eHealth = 100;
    public float eMaxHealth = 100;

    public float eLA = 5f;
    public float eUA = 15f;
    public float eAttack;

    public GameObject FloatingTextPrefab;

    PlayerStats Player;

    private StarterAssets.ThirdPersonController tpc;

    

    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<PlayerStats>();
        tpc = player.GetComponent<StarterAssets.ThirdPersonController>();
    }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();

        eAttack = Random.Range(eLA, eUA);
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatisGround))
            walkPointSet = true;
    }


    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);



 

        if (!alreadyAttacked)
        {
            //Attack code here

            transform.LookAt(player);

            var Renderer = this.GetComponent<Renderer>();

            Renderer.material.SetColor("_Color", Color.magenta);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);

            Debug.Log("Enemy is attacking!");

            if (tpc.isGuard == false && tpc.isHit == false)
            {
                Player.Health = Player.Health - eAttack;
                tpc.isHit = true;
                tpc.isHitAnim = true;

                var go = Instantiate(FloatingTextPrefab, new Vector3((player.position.x), (player.position.y + 1), player.position.z), Quaternion.identity);
                go.GetComponent<TextMeshPro>().text = eAttack.ToString("F0");
            }

            if (tpc.isGuard == true)
            {
                Debug.Log("PARRY!");
                GameObject pObject = Instantiate(parryEffect, new Vector3(player.position.x, (player.position.y + 1), player.position.z), Quaternion.Euler(new Vector3(90, Random.Range(0, 360), 0))) as GameObject;
                Destroy(pObject, 1);
            }
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;

        var Renderer = this.GetComponent<Renderer>();

        Renderer.material.SetColor("_Color", Color.white);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), .5f);

    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

}
