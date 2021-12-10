using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox1 : MonoBehaviour
{
    public GameObject EnemyMod;

    public EnemyMech1 EM;

    public bool EnemyCanAttack;

    public bool Hit;

    private StarterAssets.ThirdPersonController tpc;

    // Start is called before the first frame update
    void Start()
    {
        EM = EnemyMod.GetComponent<EnemyMech1>();
        tpc = GameObject.FindGameObjectWithTag("Player").GetComponent<StarterAssets.ThirdPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        Hit = EM.isAttackHit;

        if (Hit == false)
        {
           EnemyCanAttack = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Enemy Collided with Player");

            if (Hit == true && tpc.isSkill == false)
            EnemyCanAttack = true;
        }
    }
}
