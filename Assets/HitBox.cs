using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private bool HisAttack;

    private bool HisJumpAttack;

    private StarterAssets.ThirdPersonController tpc;

    public Rigidbody enemyRigidbody;

    Enemy1 Enemy;
    PlayerStats PlayerS;

    public float LAttack;
    public float UAttack;


    // Start is called before the first frame update
    void Start()
    {
        tpc = player.GetComponent<StarterAssets.ThirdPersonController>();

        PlayerS = FindObjectOfType<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        HisAttack = tpc.isAttack;
        HisJumpAttack = tpc.isJumpAttack;

        LAttack = PlayerS.LowerAttackDamage;
        UAttack = PlayerS.UpperAttackDamage;
    }

    private void OnTriggerEnter(Collider other)
    {
        

        if (other.gameObject.tag == "Enemy" && HisAttack == true)
        {
            enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();

            //enemyRigidbody.AddForce(0, 0, 10, ForceMode.Impulse);

            float step = -30 * Time.deltaTime;

            other.transform.position = Vector3.MoveTowards(other.transform.position, player.transform.position, step);


            GameObject hObject = Instantiate(hiteffect, new Vector3(other.gameObject.transform.position.x, (player.transform.position.y + 1), other.gameObject.transform.position.z), Quaternion.Euler(new Vector3(90, Random.Range(0, 360), 0))) as GameObject;
            Destroy(hObject, 1);
            //hit.Damage(attackPower);
            //_canAttack = false;

            Enemy = other.gameObject.GetComponent<Enemy1>();

            Enemy.eHealth = Enemy.eHealth - Random.Range(LAttack, UAttack);
        }

        if (other.gameObject.tag == "Enemy" && HisJumpAttack == true)
        {

            GameObject hObject = Instantiate(hiteffect, new Vector3(other.gameObject.transform.position.x, (player.transform.position.y + 1) ,other.gameObject.transform.position.z), Quaternion.Euler(new Vector3(90, Random.Range(0, 360), 0))) as GameObject;
            Destroy(hObject, 1);
            //hit.Damage(attackPower);
            //_canAttack = false;
        }
    }
}
