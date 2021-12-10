using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMech1 : MonoBehaviour
{
    public bool isAttack;
    public bool isAttackHit;

    public GameObject MainEnemy;
    public GameObject EnemyWeapon;

    Enemy1 Enemy;
    public EnemyHitBox1 EH;
    

    // Start is called before the first frame update
    void Start()
    {
        Enemy = MainEnemy.GetComponent<Enemy1>();
        EH = EnemyWeapon.GetComponent<EnemyHitBox1>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StartAttack()
    {
        isAttack = true;
        isAttackHit = false;
    }

    public void EndAttack()
    {
        isAttack = false;
        isAttackHit = false;
    }

    public void StartHit()
    {
        isAttackHit = true;
    }

    public void EndHit()
    {
        isAttackHit = false;
    }

    public void EndAttacking()
    {
        Enemy.isAttacking = false;
        isAttack = false;
    }

    public void DebugAttack()
    {
        EH.EnemyCanAttack = false;
    }
}
