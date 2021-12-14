using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LoadGame : MonoBehaviour
{
    public GameObject blackscreen;

    public Animator blackscreenanimator;

    public float blacktimer;

    public bool blackStart;

    public PlayerInput MenuAction;

    public GameObject Buttons;
    public GameObject AnyKey;

    private Animator ButtonsAnim;

    public float StartTimer;

    private void Awake()
    {
      //  MenuAction.MainMenu.Enable();

        MenuAction = GetComponent<PlayerInput>();
        //  MenuAction = new MainMenuAction();
        ButtonsAnim = Buttons.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;

        blackscreenanimator.Play("FadeIn");
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = 1f;

        StartTimer += Time.deltaTime;
        blacktimer += Time.deltaTime;

        if (!blackStart)
            blacktimer = 0;

        if (blackStart)
        {
            if (blacktimer > 1)
            {
                blackStart = false;
                MainMenuManager.instance.LoadGame();
            }
        }

        if (MenuAction.actions["PressAnyKey"].ReadValue<float>() >= 1f)
        {
            if (StartTimer > 2)
            {
                Debug.Log("any key pressed!");
                //LoadGameScene();
                AnyKey.SetActive(false);
                Buttons.SetActive(true);
                ButtonsAnim.Play("Fade");
            }
            
        }
    }

    public void LoadGameScene()
    {
        blackStart = true;

        blackscreenanimator.Play("FadeToBlack");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
