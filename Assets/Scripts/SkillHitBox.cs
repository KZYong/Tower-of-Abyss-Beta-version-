using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillHitBox : MonoBehaviour
{

    public GameObject hiteffect;
    public GameObject player;
    public GameObject FloatingTextPrefab;
    public GameObject FloatingTextPrefabCrit;

    public bool HisSkillAttack;

    private StarterAssets.ThirdPersonController tpc;

    public Rigidbody enemyRigidbody;

    Enemy1 Enemy;
    PlayerStats PlayerS;
    HitBox hitb;
    ComboNumber combonum;

    public bool SkilLStart;

    public float LAttack;
    public float UAttack;
    public float CritRoll;

    public float enemyDef;
    public float DefPercent;

    public bool canDamage;
    public bool isCrit;

    public float damagetaken;

    public int combonumb;

    public AudioSource HitSound;
    public AudioSource CritSound;

    public bool SkillAttackDone;
    public float AttackCD;

    public GameObject playerweapon;

    public float SkillAttackCD;
    public bool SkillDone;

    public GameObject[] EnemiesInRange;
    private int i;


    // Start is called before the first frame update
    void Start()
    {
        tpc = player.GetComponent<StarterAssets.ThirdPersonController>();

        combonum = playerweapon.GetComponent<ComboNumber>();

        hitb = playerweapon.GetComponent<HitBox>();

        PlayerS = FindObjectOfType<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemiesInRange = GameObject.FindGameObjectsWithTag("EnemyInRange");

        if (SkillDone)
            SkillAttackCD += Time.deltaTime;

        if (!SkillDone)
            SkillAttackCD = 0;

        if (SkillAttackCD > 0.20)
            SkillDone = false;

        HisSkillAttack = tpc.isSkillHit;
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.tag = "EnemyInRange";

        }

        if (other.gameObject.tag == "EnemyInRange" && HisSkillAttack == true && SkillDone == false)
        {
            foreach (GameObject enemy in EnemiesInRange)
            {
                hitb.TargetEnemy = enemy;
                hitb.DamageEnemy();
                SkillDone = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "EnemyInRange")
        {
            other.gameObject.tag = "Enemy";
        }
    }
}
