using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    private Image HealthBars;
    public float CurrentHealth;
    private float MaxHP;
    PlayerStats Player;

    public TextMeshProUGUI PlayerHPNum;


    // Start is called before the first frame update
    void Start()
    {
        HealthBars = GetComponent<Image>();
        Player = FindObjectOfType<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        CurrentHealth = Player.Health;
        MaxHP = Player.MaxHealth;
        HealthBars.fillAmount = CurrentHealth / MaxHP;
        PlayerHPNum.text = "<size=50>" + CurrentHealth.ToString("F0") + "</size>|" + MaxHP.ToString("F0");
    }
}
