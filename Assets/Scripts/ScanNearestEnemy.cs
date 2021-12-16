using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanNearestEnemy : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject player;
    public GameObject closestEnemy;

    private float minDistance = 0;
    private int count = 0;

    public GameObject FindNearestEnemy()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemyItem in enemies)
        {
            float dist = Vector3.Distance(player.transform.position, enemyItem.transform.position);

            if (count == 0)
            {
                minDistance = dist;
                closestEnemy = enemyItem;
                count++;
            }
            else
            {
                if (dist < minDistance)
                {
                    minDistance = dist;
                    closestEnemy = enemyItem;
                }
            }
        }
        return closestEnemy;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FindNearestEnemy();
    }
}
