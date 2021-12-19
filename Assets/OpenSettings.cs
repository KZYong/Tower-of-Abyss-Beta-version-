using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OpenSettings : MonoBehaviour
{
    public GameObject SettingCanvas;
    public GameObject SettingPanel;
    public Animator SettingAnim;

    public Button ReturnButton;
    public Button SettingButton;
    public Button MainMenuButton;
    public Button QuitButton;

    private StarterAssets.ThirdPersonController tpc;

    public TMPro.TMP_Dropdown ResolutionButton;

    public bool isSetting;
    // Start is called before the first frame update
    void Start()
    {
        tpc = FindObjectOfType<StarterAssets.ThirdPersonController>();

        SettingAnim = SettingPanel.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Settings()
    {
        SettingCanvas.SetActive(true);
        SettingPanel.SetActive(true);
        SettingAnim.Play("PopOut");

        ReturnButton.interactable = false;
        SettingButton.interactable = false;
        MainMenuButton.interactable = false;
        QuitButton.interactable = false;

        ResolutionButton.Select();

        isSetting = true;
        tpc.isSetting = true;
    }

    public void CloseSettings()
    {
        SettingCanvas.SetActive(false);
        SettingPanel.SetActive(false);
        SettingButton.Select();

        ReturnButton.interactable = true;
        SettingButton.interactable = true;
        MainMenuButton.interactable = true;
        QuitButton.interactable = true;

        isSetting = false;
        tpc.isSetting = false;
    }
}
