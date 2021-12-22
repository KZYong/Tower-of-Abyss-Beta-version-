using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionOrb : MonoBehaviour
{
    public float OrbHealth;
    public float OrbMaxHealth;
    public bool Broke;

    BossMechanics boss;

    // Start is called before the first frame update
    void Start()
    {
        boss = FindObjectOfType<BossMechanics>();
    }

    // Update is called once per frame
    void Update()
    {
        if (OrbHealth <= 0 && !Broke)
        {
            Broke = true;
            boss.OrbCount--;
            gameObject.SetActive(false);
        }
    }
}
