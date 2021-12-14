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
 

    // Start is called before the first frame update
    void Start()
    {

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
        MainMenuManager.instance.BackToMainMenu();
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
