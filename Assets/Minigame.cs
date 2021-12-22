using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Minigame : MonoBehaviour
{
    public bool GameStart;

    public GameObject TimerText;

    public TextMeshProUGUI TimerTM;

    float seconds;
    float milliseconds;

    bool lose;
    float losetimer;
    public GameObject LoseText;
    public GameObject Lever2;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (lose)
        {
            losetimer += Time.deltaTime;
            if (losetimer >= 3)
            {
                lose = false;
                LoseText.SetActive(false);
            }
        }
        if (!lose)
            losetimer = 0;

        if (milliseconds <= 0)
        {
            seconds--;

            milliseconds = 99;
        }

        if (GameStart)
        milliseconds -= Time.deltaTime * 100;

        if (GameStart)
        {
            TimerText.SetActive(true);

            
            TimerTM.text = string.Format("{0}:{1}", seconds, (int)milliseconds);

            if ((int)milliseconds <= 9)
                TimerTM.text = string.Format("{0}:0{1}", seconds, (int)milliseconds);
            if ((int)seconds <= 9)
                TimerTM.text = string.Format("0{0}:{1}", seconds, (int)milliseconds);
            if ((int)seconds <= 9 && (int)milliseconds <= 9)
                TimerTM.text = string.Format("0{0}:0{1}", seconds, (int)milliseconds);

            if (seconds <= 0)
            {
                GameStart = false;
                LoseText.SetActive(true);
                TimerText.SetActive(false);
                lose = true;
            }
        }
    }

    public void StartMiniGame()
    {
        GameStart = true;
        seconds = 25;
        Lever2.SetActive(true);
    }
}
