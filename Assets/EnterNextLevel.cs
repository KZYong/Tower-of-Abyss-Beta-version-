using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterNextLevel : MonoBehaviour
{
    SaveNPC Save;

    public GameObject Saver;

    public GameObject blackscreen;
    public Animator blackscreenanim;
    public float blacktimer;
    public bool blackstart;

    public GameObject NextUI;

    // Start is called before the first frame update
    void Start()
    {
        Save = Saver.GetComponent<SaveNPC>();
        blackscreenanim = blackscreen.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        blacktimer += Time.deltaTime;

        if (!blackstart)
            blacktimer = 0;

        if (blackstart)
        {
            if (blacktimer >= 3)
            {
                MainMenuManager.instance.MoveLevel2();
            }
        }
    }

    public void NextLevel()
    {
        Debug.Log("Entering Level 2!");
        SavedData.NextLevel = true;
        Save.Saving = true;
        blackstart = true;
        blackscreenanim.Play("FadeToBlack");
        NextUI.SetActive(false);
    }
}
