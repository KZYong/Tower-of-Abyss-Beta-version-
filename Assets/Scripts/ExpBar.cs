using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ExpBar : MonoBehaviour
{
    private Image EXPBars;
    public float CurrentEXP;
    private float MaxEXP;
    PlayerStats Player;


    // Start is called before the first frame update
    void Start()
    {
        EXPBars = GetComponent<Image>();
        Player = FindObjectOfType<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        CurrentEXP = Player.EXP;
        MaxEXP = Player.MaxEXP;
        EXPBars.fillAmount = CurrentEXP / MaxEXP;
    }
}
