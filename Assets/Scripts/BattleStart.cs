using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStart : MonoBehaviour
{
    private Enemy1 EnemyScript;

    private CountEnemy EnemyCounter;

    // Start is called before the first frame update
    void Start()
    {
        EnemyScript = this.GetComponent<Enemy1>();

        EnemyCounter = FindObjectOfType<CountEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
