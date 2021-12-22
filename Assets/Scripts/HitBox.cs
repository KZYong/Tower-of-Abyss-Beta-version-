using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HitBox : MonoBehaviour
{
    public GameObject hiteffect;

    public float Lattack;
    public float Uattack;

    public GameObject player;
    public GameObject FloatingTextPrefab;
    public GameObject FloatingTextPrefabCrit;
    public GameObject FloatingTextPrefabDefDown;

    private bool HisAttack;

    private bool HisJumpAttack;

    private StarterAssets.ThirdPersonController tpc;

    public Rigidbody enemyRigidbody;

    Enemy1 Enemy;
    PlayerStats PlayerS;

    ComboNumber combonum;

    public float LAttack;
    public float UAttack;
    public float CritRoll;

    public float enemyDef;
    public float DefPercent;

    public bool canDamage;
    public bool isCrit;

    public bool isSkill;

    public float damagetaken;

    public int combonumb;

    public AudioSource HitSound;
    public AudioSource CritSound;
    public AudioSource DebuffSound;

    public bool AttackDone;
    public float AttackCD;

    public GameObject TargetEnemy;
    public GameObject Boss;

    Boss Bosser;
    ExplosionOrb Orb;

    // Start is called before the first frame update
    void Start()
    {
        tpc = player.GetComponent<StarterAssets.ThirdPersonController>();

        combonum = this.GetComponent<ComboNumber>();

        PlayerS = FindObjectOfType<PlayerStats>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (AttackDone)
            AttackCD += Time.deltaTime;
        if (!AttackDone)
            AttackCD = 0;
        if (AttackCD > 0.15) 
            AttackDone = false;

       // if (tpc.isDamage == true)
           // AttackDone = false;

        combonumb = combonum.combonumber;

        HisAttack = tpc.isAttack;
        HisJumpAttack = tpc.isJumpAttack;

        LAttack = PlayerS.LowerAttackDamage;
        UAttack = PlayerS.UpperAttackDamage;

        canDamage = tpc.isDamage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyInRange" && HisAttack == true && canDamage == true)
        {
            TargetEnemy = other.gameObject;
            DamageEnemy();
        }

        if (other.gameObject.tag == "EnemyInRange" && HisJumpAttack == true && canDamage == true)
        {
            TargetEnemy = other.gameObject;
            DamageEnemy();
        }

        if (other.gameObject.tag == "Orb" && HisAttack == true && canDamage == true)
        {
            TargetEnemy = other.gameObject;
            DamageOrb();
            Debug.Log("Collided with Orb!");
        }
        if (other.gameObject.tag == "Orb" && HisJumpAttack == true && canDamage == true)
        {
            TargetEnemy = other.gameObject;
            DamageOrb();
        }
    }

    public void DamageEnemy()
    {
        enemyRigidbody = TargetEnemy.GetComponent<Rigidbody>();

        float step = -30 * Time.deltaTime;

        Enemy = TargetEnemy.GetComponent<Enemy1>();

        if (TargetEnemy == Boss)
            Bosser = TargetEnemy.GetComponent<Boss>();

        //take damage
        if (Enemy.isDeath == false && !AttackDone)
        {
            GameObject hObject = Instantiate(hiteffect, new Vector3(TargetEnemy.transform.position.x, (player.transform.position.y + 1), TargetEnemy.transform.position.z), Quaternion.Euler(new Vector3(90, Random.Range(0, 360), 0))) as GameObject;
            Destroy(hObject, 1);

            if (TargetEnemy == Boss)
                hObject.transform.localScale = new Vector3(2f, 2f, 2f);

            damagetaken = Random.Range(LAttack, UAttack);

            //crit test
            CritRoll = Random.Range(1, 100);
            if (CritRoll <= PlayerS.CritRate)
            {
                damagetaken = damagetaken * 2;
                isCrit = true;
            }
            //

            if (tpc.isThirdHit == true)
                damagetaken = damagetaken * 2;

            if (tpc.isSkillHit == true)
            {
                damagetaken = damagetaken * 2;
                Debug.Log("Skill Hit!");

                if (TargetEnemy != Boss)
                {
                    Enemy.Debuffed = true;
                    Enemy.eDefense -= 35;
                }

                if (TargetEnemy == Boss)
                {
                    Bosser.Debuffed = true;
                    Bosser.eDefense -= 35;
                }

                DebuffSound.Play();

                var wo = Instantiate(FloatingTextPrefabDefDown, new Vector3((TargetEnemy.transform.position.x), (player.transform.position.y + 1), TargetEnemy.transform.position.z), Quaternion.identity);
            }

            if (!tpc.isSkillHit)
            {
                if (TargetEnemy != Boss)
                    enemyDef = Enemy.eDefense;
                if (TargetEnemy == Boss)
                    enemyDef = Bosser.eDefense;

                DefPercent = 1 - (enemyDef / 100);
                damagetaken = damagetaken * DefPercent;
            }

            if (TargetEnemy != Boss)
            Enemy.eHealth = Enemy.eHealth - damagetaken;
            if (TargetEnemy == Boss && !Bosser.isImmune)
                Bosser.eHealth = Bosser.eHealth - damagetaken;

            if (Enemy.isAttacking == false && TargetEnemy != Boss)
            {
                Enemy.enemyanim.SetTrigger("Damaged");
                Enemy.isDamage = true;

                Enemy.Indicator.SetActive(false);
            }

            if (TargetEnemy != Boss)
            {
                if (isCrit)
                    CritSound.Play();
                if (!isCrit)
                    HitSound.Play();
            }
            if (TargetEnemy == Boss && !Bosser.isImmune)
            {
                if (isCrit)
                    CritSound.Play();
                if (!isCrit)
                    HitSound.Play();
            }
            //damage end

            //floating text
            if (isCrit == false)
            {
                if (TargetEnemy != Boss)
                {
                    var go = Instantiate(FloatingTextPrefab, new Vector3((TargetEnemy.transform.position.x), (player.transform.position.y + 2), TargetEnemy.transform.position.z), Quaternion.identity);
                    go.GetComponent<TextMeshPro>().text = damagetaken.ToString("F0");
                }
                if (TargetEnemy == Boss && !Bosser.isImmune)
                {
                    var go = Instantiate(FloatingTextPrefab, new Vector3((TargetEnemy.transform.position.x+1), (player.transform.position.y + 3), (TargetEnemy.transform.position.z+1)), Quaternion.identity);
                    go.GetComponent<TextMeshPro>().text = damagetaken.ToString("F0");
                }
            }

            if (isCrit == true)
            {
                if (TargetEnemy != Boss)
                {
                    var go = Instantiate(FloatingTextPrefabCrit, new Vector3((TargetEnemy.transform.position.x), (player.transform.position.y + 2), TargetEnemy.transform.position.z), Quaternion.identity);
                    go.GetComponent<TextMeshPro>().text = damagetaken.ToString("F0") + "<size=%25>critical!</size>";
                    isCrit = false;
                }
                if (TargetEnemy == Boss && !Bosser.isImmune)
                {
                    var go = Instantiate(FloatingTextPrefabCrit, new Vector3((TargetEnemy.transform.position.x+1), (player.transform.position.y + 3), (TargetEnemy.transform.position.z+1)), Quaternion.identity);
                    go.GetComponent<TextMeshPro>().text = damagetaken.ToString("F0") + "<size=%25>critical!</size>";
                    isCrit = false;
                }
            }

            if (TargetEnemy != Boss)
            {
                combonum.combonumber = combonum.combonumber + 1;
                combonum.combotime = 0;
                combonum.comboAnim.SetTrigger("ComboNumTrigger");

                combonum.totaldamage = combonum.totaldamage + damagetaken;
            }


            if (TargetEnemy == Boss && !Bosser.isImmune)
            {
                combonum.combonumber = combonum.combonumber + 1;
                combonum.combotime = 0;
                combonum.comboAnim.SetTrigger("ComboNumTrigger");

                combonum.totaldamage = combonum.totaldamage + damagetaken;
            }

            if (!tpc.isSkillHit)
            AttackDone = true;
        }
    }

    public void DamageOrb()
    {
        GameObject hObject = Instantiate(hiteffect, new Vector3(TargetEnemy.transform.position.x, (player.transform.position.y + 1), TargetEnemy.transform.position.z), Quaternion.Euler(new Vector3(90, Random.Range(0, 360), 0))) as GameObject;
        Destroy(hObject, 1);

        Orb = TargetEnemy.GetComponent<ExplosionOrb>();

        damagetaken = Random.Range(LAttack, UAttack);

        if (tpc.isThirdHit == true)
            damagetaken = damagetaken * 2;

        if (tpc.isSkillHit == true)
        {
            damagetaken = damagetaken * 2;
            Debug.Log("Skill Hit!");
        }

        Orb.OrbHealth = Orb.OrbHealth - damagetaken;

        HitSound.Play();

        //damage end
            var go = Instantiate(FloatingTextPrefab, new Vector3((TargetEnemy.transform.position.x), (player.transform.position.y + 2), TargetEnemy.transform.position.z), Quaternion.identity);
            go.GetComponent<TextMeshPro>().text = damagetaken.ToString("F0");
        //floating text

            combonum.combonumber = combonum.combonumber + 1;
            combonum.combotime = 0;
            combonum.comboAnim.SetTrigger("ComboNumTrigger");

            combonum.totaldamage = combonum.totaldamage + damagetaken;

        if (!tpc.isSkillHit)
            AttackDone = true;
    }
}