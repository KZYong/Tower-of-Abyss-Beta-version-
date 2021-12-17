using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSound : MonoBehaviour
{
    public AudioSource ButtonSound;
    public AudioSource StartButtonSound;

    LoadGame game;

    // Start is called before the first frame update
    void Start()
    {
        game = FindObjectOfType<LoadGame>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonSFX()
    {
        ButtonSound.Play();
    }

    public void StartButtonSFX()
    {
        StartButtonSound.Play();
    }

    public void NewGameSFX()
    {
        if (game.SaveExist)
        {
            ButtonSound.Play();
        }

        if (!game.SaveExist)
        {
            StartButtonSound.Play();
        }
    }
}
