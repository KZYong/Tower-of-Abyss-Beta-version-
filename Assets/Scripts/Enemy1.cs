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

    public GameObject EnemyModel;
    public GameObject PlayerModel;
    public Animator enemyanim;


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

    public bool isDamage;
    public bool isAttacking;
    public bool ECanAttack;
    public bool isDeath;
    public bool PlayerDetected;

    public GameObject parryEffect;

    public GameObject EnemyWeapon;

    //Stats
    public float eHealth = 100;
    public float eMaxHealth = 100;
    public float eDefense = 40;
    public float OeDefense = 40;

    public float eLA = 5f;
    public float eUA = 15f;
    public float eAttack;

    public float amountDamage;

    public float damagetimer;

    public GameObject FloatingTextPrefab;

    public AudioSource ParrySound;
    public AudioSource EnemyHit;

    PlayerStats Player;
    public EnemyMech1 EM;
    public EnemyHitBox1 EH;

    private StarterAssets.ThirdPersonController tpc;
    private CountEnemy EnemyCounter;

    public float debugtimer;

    private float parrytimer;
    public bool parrytimesound;

    public GameObject DebuffUI;
    public bool Debuffed;
    public float DebuffTimer;

    public GameObject EnemyExplode;
    private float deadtimer;

    public GameObject Indicator;
    private Animator IndicatorAnim;
    public AudioSource AlertSound;

    private bool deadeffectdone;

    private float OriginalPositionX;
    private float OriginalPositionY;
    private float OriginalPositionZ;

    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<PlayerStats>();
        tpc = player.GetComponent<StarterAssets.ThirdPersonController>();

        enemyanim = EnemyModel.GetComponent<Animator>();
        EM = EnemyModel.GetComponent<EnemyMech1>();
        EH = EnemyWeapon.GetComponent<EnemyHitBox1>();

        EnemyCounter = FindObjectOfType<CountEnemy>();

        IndicatorAnim = Indicator.GetComponent<Animator>();

        OriginalPositionX = transform.position.x;
        OriginalPositionX = transform.position.y;
        OriginalPositionX = transform.position.z;
    }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Debuffed)
        {
            DebuffUI.SetActive(true);
            DebuffTimer += Time.deltaTime;
        }

        if (!Debuffed)
        {
            DebuffUI.SetActive(false);

            DebuffTimer = 0;
        }

        if (DebuffTimer > 10)
        {
            Debuffed = false;
            eDefense = OeDefense;
        }

        if (!parrytimesound)
            parrytimer = 0;
        if (parrytimesound)
            parrytimer += Time.deltaTime;
        if (parrytimer > 0.5)
            parrytimesound = false;

        if (PlayerDetected == true)
        {
            sightRange = 8;
        }

        if (PlayerDetected == false)
            sightRange = 8;


        /// debugtimer += Time.deltaTime;

        //  if (debugtimer >= 10)
        //  {
        //       isAttacking = false;
        //       debugtimer = 0;
        //    }

        ECanAttack = EH.EnemyCanAttack;

        if (isDamage == true && isAttacking == false && !isDeath)
        {
            agent.updateRotation = true;

            //transform.LookAt(player.transform);

            var rotationAngle = Quaternion.LookRotation(player.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationAngle, Time.deltaTime * 5);

            damagetimer += Time.deltaTime;
            if (damagetimer >= 0.10)
            {
                isDamage = false;
                damagetimer = 0;
            }
        }

        if (eHealth <= 0)
        {
            enemyanim.Play("Death");
            isDeath = true;

            deadtimer += Time.deltaTime;

            if (PlayerDetected)
            {
                EnemyCounter.EnemyCount -= 1;
                PlayerDetected = false;
            }
            Destroy(this.gameObject, 3);

            if (deadtimer > 2 && !deadeffectdone)
            { 
                GameObject hObject = Instantiate(EnemyExplode, new Vector3(transform.position.x, (player.transform.position.y + 1), transform.position.z), Quaternion.Euler(new Vector3(90, Random.Range(0, 360), 0)));
                Destroy(hObject, 1);
                deadeffectdone = true;
            }
        }

        isAttacking = EM.isAttack;

        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (isDamage == false && isAttacking == false && isDeath == false)
        {
            if (!playerInSightRange && !playerInAttackRange) Patroling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInSightRange && playerInAttackRange) AttackPlayer();
        }

        eAttack = Random.Range(eLA, eUA);

        //Attacking

        if (isAttacking == true && !isDeath)
        {
            var rotationAngle = Quaternion.LookRotation(player.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationAngle, Time.deltaTime * 5);
        }

        if (ECanAttack == true && isAttacking == true && isDeath == false)
        {

            if (tpc.isGuard == false && tpc.isHit == false && tpc.PlayerDeath == false)
            {
                amountDamage = Player.Defense / 100;
                amountDamage = eAttack * (1 - amountDamage);
                Player.Health = Player.Health - amountDamage;
                tpc.isHit = true;
                tpc.isHitAnim = true;

                var go = Instantiate(FloatingTextPrefab, new Vector3((player.position.x), (player.position.y + 1), player.position.z), Quaternion.identity);
                go.GetComponent<TextMeshPro>().text = amountDamage.ToString("F0");

                EnemyHit.Play();

                EH.EnemyCanAttack = false;
            }

            if (tpc.isGuard == true)
            {
                Debug.Log("PARRY!");
                GameObject pObject = Instantiate(parryEffect, new Vector3(player.position.x, (player.position.y + 1), player.position.z), Quaternion.Euler(new Vector3(90, Random.Range(0, 360), 0))) as GameObject;
                Destroy(pObject, 1);

                if (!parrytimesound)
                    ParrySound.Play();

                parrytimesound = true;

                EH.EnemyCanAttack = false;
            }
        }
    }

    private void Patroling()
    {
        if (PlayerDetected)
        {
            EnemyCounter.EnemyCount -= 1;
            PlayerDetected = false;

            eHealth = eMaxHealth;
        }

        enemyanim.Play("Walk");

        GetComponent<NavMeshAgent>().speed = 3;

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
        if (!PlayerDetected)
            EnemyCounter.EnemyCount += 1;

        PlayerDetected = true;


        if (!alreadyAttacked)
        {
            GetComponent<NavMeshAgent>().speed = 6;
            enemyanim.Play("Run");
        }

        if (alreadyAttacked)
        {
            enemyanim.Play("Walk");
        }

        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);


        if (alreadyAttacked)
        {
            GetComponent<NavMeshAgent>().speed = 3;
            isAttacking = false;
        }

        if (!alreadyAttacked)
        {
            //Attack code here

            //transform.LookAt(player);

            isAttacking = true;

            Indicator.SetActive(true);
            IndicatorAnim.Play("Enemyindicator");
            AlertSound.Play();

            enemyanim.Play("Attack");
            //agent.SetDestination(player.position);

            //var Renderer = this.GetComponent<Renderer>();

            //Renderer.material.SetColor("_Color", Color.magenta);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);

            //Debug.Log("Enemy is attacking!");


        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;

        var Renderer = this.GetComponent<Renderer>();

        Renderer.material.SetColor("_Color", Color.white);

        GetComponent<NavMeshAgent>().speed = 6;
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

    public void Attacking()
    {
        isAttacking = true;
    }
}
