using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HitBox : MonoBehaviour
{
    public GameObject hiteffect;

    private int _attackPower = 1;

    public float Lattack;
    public float Uattack;

    private bool _canAttack;

    private void OnEnable()
    {
        _canAttack = true;
    }

    public GameObject player;
    public GameObject FloatingTextPrefab;
    public GameObject FloatingTextPrefabCrit;

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

    public float damagetaken;

    public int combonumb;

    public AudioSource HitSound;
    public AudioSource CritSound;


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
        combonumb = combonum.combonumber;

        HisAttack = tpc.isAttack;
        HisJumpAttack = tpc.isJumpAttack;

        LAttack = PlayerS.LowerAttackDamage;
        UAttack = PlayerS.UpperAttackDamage;

        canDamage = tpc.isDamage;
    }

    private void OnTriggerEnter(Collider other)
    {
        

        if (other.gameObject.tag == "Enemy" && HisAttack == true && canDamage == true)
        {
            enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();

            //enemyRigidbody.AddForce(0, 0, 10, ForceMode.Impulse);

            float step = -30 * Time.deltaTime;

            //other.transform.position = Vector3.MoveTowards(other.transform.position, player.transform.position, step);




            Enemy = other.gameObject.GetComponent<Enemy1>();


            //take damage
            if (Enemy.isDeath == false)
            {
               

                GameObject hObject = Instantiate(hiteffect, new Vector3(other.gameObject.transform.position.x, (player.transform.position.y + 1), other.gameObject.transform.position.z), Quaternion.Euler(new Vector3(90, Random.Range(0, 360), 0))) as GameObject;
                Destroy(hObject, 1);

                damagetaken = Random.Range(LAttack, UAttack);

                enemyDef = Enemy.eDefense;
                DefPercent = 1 - (enemyDef / 100);

                damagetaken = damagetaken * DefPercent;

                //crit test
                CritRoll = Random.Range(1, 100);
                if (CritRoll <= PlayerS.CritRate)
                {
                    damagetaken = damagetaken * 2;
                    isCrit = true;
                }
                //

                if (tpc.isThirdHit == true)
                    damagetaken = damagetaken * 3;

                Enemy.eHealth = Enemy.eHealth - damagetaken;

                if (Enemy.isAttacking == false)
                {
                    Enemy.enemyanim.SetTrigger("Damaged");
                    Enemy.isDamage = true;
                }

                if (isCrit)
                    CritSound.Play();

                if (!isCrit)
                    HitSound.Play();
                //damage end

                //floating text
                if (isCrit == false)
                {
                    var go = Instantiate(FloatingTextPrefab, new Vector3((other.gameObject.transform.position.x), (player.transform.position.y + 2), other.transform.position.z), Quaternion.identity);
                    go.GetComponent<TextMeshPro>().text = damagetaken.ToString("F0");
                }

                if (isCrit == true)
                {
                    var go = Instantiate(FloatingTextPrefabCrit, new Vector3((other.gameObject.transform.position.x), (player.transform.position.y + 2), other.transform.position.z), Quaternion.identity);
                    go.GetComponent<TextMeshPro>().text = damagetaken.ToString("F0") + "<size=%25>critical!</size>";
                    isCrit = false;
                }

                combonum.combonumber = combonum.combonumber + 1;
                combonum.combotime = 0;
                combonum.comboAnim.SetTrigger("ComboNumTrigger");

                combonum.totaldamage = combonum.totaldamage + damagetaken;
            }
        }

        if (other.gameObject.tag == "Enemy" && HisJumpAttack == true && canDamage == true)
        {
            float step = -30 * Time.deltaTime;

            other.transform.position = Vector3.MoveTowards(other.transform.position, player.transform.position, step);


            Enemy = other.gameObject.GetComponent<Enemy1>();

            //take damage
            if (Enemy.isDeath == false)
            {
                HitSound.Play();

                GameObject hObject = Instantiate(hiteffect, new Vector3(other.gameObject.transform.position.x, (player.transform.position.y + 1), other.gameObject.transform.position.z), Quaternion.Euler(new Vector3(90, Random.Range(0, 360), 0))) as GameObject;
                Destroy(hObject, 1);


                damagetaken = Random.Range(LAttack, UAttack);

                enemyDef = Enemy.eDefense;
                DefPercent = 1 - (enemyDef / 100);

                damagetaken = damagetaken * DefPercent;

                if (tpc.isThirdHit == true)
                    damagetaken = damagetaken * 3;

                Enemy.eHealth = Enemy.eHealth - damagetaken;

                if (Enemy.isAttacking == false)
                {
                    Enemy.enemyanim.SetTrigger("Damaged");
                    Enemy.isDamage = true;
                }


                //damage end

                //floating text
                var go = Instantiate(FloatingTextPrefab, new Vector3((other.gameObject.transform.position.x), (player.transform.position.y - 1), other.gameObject.transform.position.z), Quaternion.identity);
                //go.transform.position = go.transform.position + (other.gameObject.transform.forward);
                go.GetComponent<TextMeshPro>().text = damagetaken.ToString("F0");

                combonum.combonumber = combonum.combonumber + 1;
                combonum.combotime = 0;
                combonum.comboAnim.SetTrigger("ComboNumTrigger");

                combonum.totaldamage = combonum.totaldamage + damagetaken;
            }
        }
    }
}
