using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Manager : MonoBehaviour
{
    public PlayerStats PlayerS;

    public GameObject KeyboardUI;
    public GameObject GamepadUI;

    // Start is called before the first frame update
    void Start()
    {
        PlayerS = FindObjectOfType<PlayerStats>();

        PlayerS.ThisStage = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (SavedData.KeyboardMode)
        {
            KeyboardUI.SetActive(true);
            GamepadUI.SetActive(false);
        }

        if (SavedData.GamepadMode)
        {
            KeyboardUI.SetActive(false);
            GamepadUI.SetActive(true);
        }
    }
}
