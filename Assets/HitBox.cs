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

    private bool HisAttack;

    private bool HisJumpAttack;

    private StarterAssets.ThirdPersonController tpc;

    public Rigidbody enemyRigidbody;

    Enemy1 Enemy;
    PlayerStats PlayerS;

    public float LAttack;
    public float UAttack;

    public bool canDamage;

    public float damagetaken;


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

        canDamage = tpc.isDamage;
    }

    private void OnTriggerEnter(Collider other)
    {
        

        if (other.gameObject.tag == "Enemy" && HisAttack == true && canDamage == true)
        {
            enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();

            //enemyRigidbody.AddForce(0, 0, 10, ForceMode.Impulse);

            float step = -30 * Time.deltaTime;

            other.transform.position = Vector3.MoveTowards(other.transform.position, player.transform.position, step);


            GameObject hObject = Instantiate(hiteffect, new Vector3(other.gameObject.transform.position.x, (player.transform.position.y + 1), other.gameObject.transform.position.z), Quaternion.Euler(new Vector3(90, Random.Range(0, 360), 0))) as GameObject;
            Destroy(hObject, 1);

            Enemy = other.gameObject.GetComponent<Enemy1>();

            damagetaken = Random.Range(LAttack, UAttack);

            Enemy.eHealth = Enemy.eHealth - damagetaken;

            //floating text
            var go = Instantiate(FloatingTextPrefab, new Vector3((other.gameObject.transform.position.x), (player.transform.position.y + 2), other.transform.position.z), Quaternion.identity);
            //go.transform.position = go.transform.position - ((player.transform.forward/10));
            go.GetComponent<TextMeshPro>().text = damagetaken.ToString("F0");
        }

        if (other.gameObject.tag == "Enemy" && HisJumpAttack == true && canDamage == true)
        {
            float step = -30 * Time.deltaTime;

            other.transform.position = Vector3.MoveTowards(other.transform.position, player.transform.position, step);

            GameObject hObject = Instantiate(hiteffect, new Vector3(other.gameObject.transform.position.x, (player.transform.position.y + 1), other.gameObject.transform.position.z), Quaternion.Euler(new Vector3(90, Random.Range(0, 360), 0))) as GameObject;
            Destroy(hObject, 1);

            Enemy = other.gameObject.GetComponent<Enemy1>();

            Enemy.eHealth = Enemy.eHealth - Random.Range(LAttack, UAttack);

            //floating text
            var go = Instantiate(FloatingTextPrefab, new Vector3((other.gameObject.transform.position.x), (player.transform.position.y - 1), other.gameObject.transform.position.z), Quaternion.identity);
            //go.transform.position = go.transform.position + (other.gameObject.transform.forward);
            go.GetComponent<TextMeshPro>().text = damagetaken.ToString("F0");
        }
    }
}
