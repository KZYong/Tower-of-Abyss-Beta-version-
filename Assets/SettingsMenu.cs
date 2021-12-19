using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public TMPro.TMP_Dropdown resolutionDropdown;
    public TMPro.TMP_Dropdown graphicDropdown;
    public TMPro.TMP_Dropdown uimodeDropdown;
    public Toggle FullscreenToggle;

    public Slider BGMSlider;
    public Slider SFXSlider;

    int Gsave = 2;
    int Rsave = 0;
    int UIsave = 0;
    int FSsave;
    float bgmSave;
    float sfxSave;

    public GameObject KeyboardIcon;
    public GameObject GamepadIcon;

    public void GraphicSave(int GraphicNum)
    {
        QualitySettings.SetQualityLevel(GraphicNum);
        Gsave = GraphicNum;
        PlayerPrefs.SetInt("Graphic", Gsave);
    }

    public void ResolutionSave()
    {
        Rsave = resolutionDropdown.value;
        PlayerPrefs.SetInt("Resolution", Rsave);
    }

    public void UIModeSave()
    {
        UIsave = uimodeDropdown.value;
        PlayerPrefs.SetInt("UIMode", UIsave);
    }

    // Start is called before the first frame update
    void Start()
    {
        bgmSave = 69;
        sfxSave = 69;

        Gsave = PlayerPrefs.GetInt("Graphic", Gsave);
        graphicDropdown.value = Gsave;
        QualitySettings.SetQualityLevel(Gsave);

        Rsave = PlayerPrefs.GetInt("Resolution", Rsave);
        resolutionDropdown.value = Rsave;
        SetResolution();

        UIsave = PlayerPrefs.GetInt("UIMode", UIsave);
        uimodeDropdown.value = UIsave;
        SetUIMode();

        FSsave = PlayerPrefs.GetInt("Fullscreen", FSsave);

        if (FSsave == 0)
        {
            Screen.fullScreen = true;
            FullscreenToggle.isOn = true;
        }
        if (FSsave == 1)
        {
            Screen.fullScreen = false;
            FullscreenToggle.isOn = false;
        }

        bgmSave = PlayerPrefs.GetFloat("BGMVolume", bgmSave);
        sfxSave = PlayerPrefs.GetFloat("SFXVolume", sfxSave);

        if (bgmSave != 69)
        {
            audioMixer.SetFloat("bgmvolume", bgmSave);
            BGMSlider.value = bgmSave;
        }

        if (sfxSave != 69)
        {
            audioMixer.SetFloat("sfxvolume", sfxSave);
            SFXSlider.value = sfxSave;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (uimodeDropdown.value == 0)
        {
            KeyboardIcon.SetActive(true);
            GamepadIcon.SetActive(false);
        }
        if (uimodeDropdown.value == 1)
        {
            KeyboardIcon.SetActive(false);
            GamepadIcon.SetActive(true);
        }
    }

    public void SwitchKeyboard()
    {
        SavedData.KeyboardMode = true;
        SavedData.GamepadMode = false;
    }

    public void SwitchGamepad()
    {
        SavedData.KeyboardMode = false;
        SavedData.GamepadMode = true;
    }

    public void SetBGMVolume (float volume)
    {
        audioMixer.SetFloat("bgmvolume", volume);
        PlayerPrefs.SetFloat("BGMVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("sfxvolume", volume);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;

        if (isFullscreen)
        {
            FSsave = 0;
            PlayerPrefs.SetInt("Fullscreen", FSsave);
        }

        if (!isFullscreen)
        {
            FSsave = 1;
            PlayerPrefs.SetInt("Fullscreen", FSsave);
        }
    }

    public void SetResolution()
    {
        if (resolutionDropdown.value == 0)
            Screen.SetResolution(1920, 1080, Screen.fullScreen);

        if (resolutionDropdown.value == 1)
            Screen.SetResolution(1366, 768, Screen.fullScreen);

        if (resolutionDropdown.value == 2)
            Screen.SetResolution(1280, 720, Screen.fullScreen);
    }

    public void SetUIMode()
    {
        if (uimodeDropdown.value == 0)
        {
            SavedData.KeyboardMode = true;
            SavedData.GamepadMode = false;
        }

        if (uimodeDropdown.value == 1)
        {
            SavedData.KeyboardMode = false;
            SavedData.GamepadMode = true;
        }
    }
}
