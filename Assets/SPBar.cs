using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SPBar : MonoBehaviour
{
    private Image SPBars;
    public float CurrentSP;
    private float MaxSP = 100f;
    PlayerStats Player;

    //public TextMeshProUGUI PlayerSPNum;


    // Start is called before the first frame update
    void Start()
    {
        SPBars = GetComponent<Image>();
        Player = FindObjectOfType<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        CurrentSP = Player.Stamina;
        SPBars.fillAmount = CurrentSP / MaxSP;
        //PlayerHPNum.text = CurrentHealth.ToString("F0") + "/" + MaxHealth.ToString("F0");
    }
}
