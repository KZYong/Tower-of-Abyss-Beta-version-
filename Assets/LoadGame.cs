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
    }

    // Update is called once per frame
    void Update()
    {
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
            Debug.Log("any key pressed!");
            //LoadGameScene();
            AnyKey.SetActive(false);
            Buttons.SetActive(true);
            ButtonsAnim.Play("Fade");
            
        }
    }

    public void LoadGameScene()
    {
        blackStart = true;

        blackscreenanimator.Play("FadeToBlack");
    }
}
