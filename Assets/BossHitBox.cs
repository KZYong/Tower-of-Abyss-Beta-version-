using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHitBox : MonoBehaviour
{
    public GameObject EnemyMod;

    public BossMech1 EM;

    public bool EnemyCanAttack;

    public bool Hit;

    private StarterAssets.ThirdPersonController tpc;

    // Start is called before the first frame update
    void Start()
    {
        EM = EnemyMod.GetComponent<BossMech1>();
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

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && !tpc.isSkill)
        {
            Debug.Log("Enemy Collided with Player");

            if (Hit == true && tpc.isSkill == false)
                EnemyCanAttack = true;
        }
    }
}
