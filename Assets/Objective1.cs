using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Objective1 : MonoBehaviour
{
    public GameObject Player;
    public GameObject[] Enemy;

    public int EnemyCount;
    public int EnemyInRange;
    public int EnemyNotInRange;

    public GameObject blackscreen;
    public Animator blackscreenanim;
    public float blacktimer;
    public bool blackstart;

    public float cutscenetimer;

    public bool CutScene;
    public bool CutSceneDone;

    private StarterAssets.ThirdPersonController tpc;

    public GameObject PlayerCamera;
    public GameObject CutsceneCamera;
    public GameObject PlayerUI;
    public GameObject PlayerBar;
    public GameObject Portal;
    public GameObject Message;
    public TextMeshProUGUI Objective;

    public bool CanStart;

    // Start is called before the first frame update
    void Start()
    {
        tpc = FindObjectOfType<StarterAssets.ThirdPersonController>();
        blackscreenanim = blackscreen.GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player");
        EnemyCount = 15;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyNotInRange = GameObject.FindGameObjectsWithTag("Enemy").Length;
        EnemyInRange = GameObject.FindGameObjectsWithTag("EnemyInRange").Length;

        EnemyCount = EnemyNotInRange + EnemyInRange;
        //Debug.Log("Enemy Count = " + EnemyCount);

        blacktimer += Time.deltaTime;
        cutscenetimer += Time.deltaTime;

        if (blackstart)
        {
            if (blacktimer >= 1)
            {

                blackstart = false;
                blackscreenanim.Play("FadeIn");

                if (!CutSceneDone)
                {
                    Message.SetActive(true);
                    Portal.SetActive(true);
                }

                if (CutSceneDone)
                {
                    PlayerUI.SetActive(true);
                    PlayerBar.SetActive(true);
                    Objective.text = "Enter the next floor through the portal.";
                }
            }
        }

        if (EnemyCount <= 0 && !CanStart)
        {
            
            blackstart = true;
            blackscreenanim.Play("FadeFast");
            tpc.isCutScene = true;
            CanStart = true;

            CutScene = true;
            tpc.LockAction = true;
            PlayerCamera.SetActive(false);
            PlayerUI.SetActive(false);
            PlayerBar.SetActive(false);
            CutsceneCamera.SetActive(true);
            

            

        }

        if (CutScene == true)
        {
            if (cutscenetimer >= 5)
            {
                blackscreenanim.Play("FadeFast");
                blackstart = true;
                CutSceneDone = true;

                tpc.isCutScene = false;
                tpc.LockAction = false;
                CutScene = false;
                PlayerCamera.SetActive(true);

                CutsceneCamera.SetActive(false);

                Message.SetActive(false);

            }
        }

        if (!blackstart)
            blacktimer = 0;
        if (!CutScene)
            cutscenetimer = 0;
    }
}
