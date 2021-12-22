using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.IO;
using UnityEngine.UI;

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

    public AudioSource AnyButtonSound;

    public bool AnyKeyDone;

    public GameObject ContinueButton;
    public GameObject ContinueDisabled;
    public GameObject ConfirmNewGame;
    public Animator ConfirmNewGameAnim;

    public Button NewButton;
    public Button SettingsButton;
    public Button ContButton;
    public Button QuitButton;
    public Button YesButton;
    public Button NoButton;
    public TMPro.TMP_Dropdown ResolutionButton;
    public GameObject SettingCanvas;
    public GameObject SettingPanel;
    public Animator SettingAnim;
    public bool isSetting;

    public bool SaveExist;

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
        ConfirmNewGameAnim = ConfirmNewGame.GetComponent<Animator>();
        SettingAnim = SettingPanel.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = 1f;

        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if (File.Exists(destination))
        {
            ContinueButton.SetActive(true);
            ContinueDisabled.SetActive(false);
            SaveExist = true;
        }
        else
        {
            ContinueButton.SetActive(false);
            ContinueDisabled.SetActive(true);
            SaveExist = false;
        }

        StartTimer += Time.deltaTime;
        blacktimer += Time.deltaTime;

        if (!blackStart)
            blacktimer = 0;

        if (blackStart)
        {
            if (blacktimer > 2.25)
            {
                blackStart = false;


                if (SavedData.MenuLoad)
                {
                    if (SavedData.LoadStage == 1)
                    {
                        SavedData.NewGame = false;
                        Debug.Log("I am loading Saved Game!");
                        MainMenuManager.instance.LoadGame();
                    }

                    if (SavedData.LoadStage == 2)
                    {
                        SavedData.NewGame = false;
                        Debug.Log("I am loading Saved Game!");
                        MainMenuManager.instance.LoadLevel2();
                    }
                }

                if (!SavedData.MenuLoad)
                {
                    SavedData.NewGame = true;

                    Debug.Log("I am loading New Game!");
                    MainMenuManager.instance.LoadGame();
                }

                SavedData.MenuLoad = false;

            }
        }

        if (MenuAction.actions["PressAnyKey"].ReadValue<float>() >= 1f && !AnyKeyDone)
        {
            if (StartTimer > 2.25)
            {
                Debug.Log("any key pressed!");
                //LoadGameScene();
                AnyKey.SetActive(false);
                Buttons.SetActive(true);
                ButtonsAnim.Play("Fade");
                AnyButtonSound.Play();
                AnyKeyDone = true;
            }
            
        }

        if (MenuAction.actions["ESC"].ReadValue<float>() >= 1f)
        {
            if (isSetting)
            {
                CloseSettings();
            }

        }
    }

    public void LoadGameScene()
    {
        blackStart = true;

        blackscreenanimator.Play("FadeToBlack");
    }

    public void ContinueGameScene()
    {
        blackStart = true;

        blackscreenanimator.Play("FadeToBlack");

        SavedData.Load = true;
        SavedData.LoadStats = true;
        SavedData.MenuLoad = true;
        SavedData.NewGame = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ConfirmButton()
    {
        ConfirmNewGame.SetActive(true);
        ConfirmNewGameAnim.Play("UIPopOut");
        YesButton.Select();

        NewButton.interactable = false;
        ContButton.interactable = false;
        SettingsButton.interactable = false;
        QuitButton.interactable = false;
    }

    public void YesPressed()
    {
        LoadGameScene();
    }

    public void NoPressed()
    {
        ConfirmNewGame.SetActive(false);
       

        NewButton.interactable = true;
        ContButton.interactable = true;
        SettingsButton.interactable = true;
        QuitButton.interactable = true;

        NewButton.Select();
    }

    public void NewGame()
    {
        if (!SaveExist)
        {
            LoadGameScene();
        }

        if (SaveExist)
        {
            ConfirmButton();
        }
    }

    public void Settings()
    {
        SettingCanvas.SetActive(true);
        SettingPanel.SetActive(true);
        SettingAnim.Play("PopOut");

        NewButton.interactable = false;
        ContButton.interactable = false;
        SettingsButton.interactable = false;
        QuitButton.interactable = false;

        ResolutionButton.Select();

        isSetting = true;
    }

    public void CloseSettings()
    {
        SettingCanvas.SetActive(false);
        SettingPanel.SetActive(false);
        SettingsButton.Select();

        NewButton.interactable = true;
        ContButton.interactable = true;
        SettingsButton.interactable = true;
        QuitButton.interactable = true;

        isSetting = false;
    }
}
