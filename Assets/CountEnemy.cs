using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountEnemy : MonoBehaviour
{
    public int EnemyCount;

    public AudioSource BattleBGMSource;
    public AudioSource ExploreBGMSource;

    public bool BattlePlaying;
    public bool ExplorePlaying;

    public float WaitTimer;

    public bool BattleMode;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyCount > 0)
        {
            BattleMode = true;

            Debug.Log("Battle Start!");

            if (BattlePlaying == false)
            {
                WaitTimer += Time.deltaTime;

                if (WaitTimer > 0.25)
                {
                    ExploreBGMSource.Pause();

                    BattleBGMSource.Play();

                    BattlePlaying = true;
                    WaitTimer = 0;
                }
            }
        }

        if (EnemyCount == 0)
        {
            BattleMode = false;

            Debug.Log("Battle End!");

            if (BattlePlaying == true)
            {
                WaitTimer += Time.deltaTime;
                

                if (WaitTimer > 3)
                {
                    BattleBGMSource.Stop();
                    ExploreBGMSource.UnPause();

                    BattlePlaying = false;
                    WaitTimer = 0;
                }
            }
        }

        if (EnemyCount < 0)
        {
            EnemyCount = 0;
        }
    }
}
