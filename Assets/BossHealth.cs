using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossHealth : MonoBehaviour
{
    private Image HealthBars;
    public float CurrentHealth;
    private float MaxHP;
    Boss Player;
    private float Percentage;

    public TextMeshProUGUI BossHPNum;

    // Start is called before the first frame update
    void Start()
    {
        HealthBars = GetComponent<Image>();
        Player = FindObjectOfType<Boss>();
    }

    // Update is called once per frame
    void Update()
    {
        CurrentHealth = Player.eHealth;
        MaxHP = Player.eMaxHealth;
        HealthBars.fillAmount = CurrentHealth / MaxHP;
        Percentage = (CurrentHealth / MaxHP) * 100;
        BossHPNum.text = CurrentHealth.ToString("F0") + "/" + MaxHP.ToString("F0") + " (" + Percentage.ToString("F0") + "%)";

        if (Player.eHealth <= 0)
            Player.eHealth = 0;
    }
}
