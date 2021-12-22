using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    public GameObject ConfirmPanel;
    public GameObject ConfirmPanelQuit;

    public Button NoButton1;
    public Button NoButton2;
    public Button ReturnButton;

    public PlayerStats PlayerS;


    // Start is called before the first frame update
    void Start()
    {
        PlayerS = FindObjectOfType<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowConfirm()
    {
        ConfirmPanel.SetActive(true);
        NoButton1.Select();
        
    }
    public void ShowConfirmQuit()
    {
        ConfirmPanelQuit.SetActive(true);
        NoButton2.Select();
    }

    public void Confirm()
    {
        Time.timeScale = 1;

        if (PlayerS.ThisStage == 1)
            MainMenuManager.instance.BackToMainMenu();

        if (PlayerS.ThisStage == 2)
            MainMenuManager.instance.BackToMainMenu2();
    }

    public void Cancel()
    {
        ConfirmPanel.SetActive(false);
        ConfirmPanelQuit.SetActive(false);
        ReturnButton.Select();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SelectReturn()
    {
        ReturnButton.Select();
    }
}
