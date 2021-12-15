using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public float Health = 100f;
    public float MaxHealth = 100f;
    public float Stamina = 100f;

    public float HealthPercent;

    public float Defense = 5f;
    public float CritRate = 5f;

    public float EXP;
    public float MaxEXP;
    public int level;

    public float Timer;
    public float Minutes;
    public float Seconds;

    public TextMeshProUGUI LevelText;

    public float LowerAttackDamage = 1f;
    public float UpperAttackDamage = 5f;

    public GameObject LowHPWarning;
    public GameObject StaminaWarning;
    private Animator StaminaAnim;

    private float StaminaTimer;
    private bool StaminaNotEnough;

    public TextMeshProUGUI M_LV;
    public TextMeshProUGUI M_EXP;
    public TextMeshProUGUI M_HP;
    public TextMeshProUGUI M_Time;
    public TextMeshProUGUI M_Attack;
    public TextMeshProUGUI M_Defense;
    public TextMeshProUGUI M_CritRate;

    // Start is called before the first frame update
    void Start()
    {
        StaminaAnim = StaminaWarning.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        Seconds += Time.deltaTime;

        if (Seconds >= 60f)
        {
            Seconds -= 60f;
            Minutes += 1f;
        }

        M_LV.text = "LV." + level.ToString();
        M_EXP.text = "EXP " + EXP.ToString("F0") + "/" + MaxEXP.ToString("F0");
        M_HP.text = "HP " + Health.ToString("F0") + "/" + MaxHealth.ToString("F0");
        M_Attack.text = LowerAttackDamage.ToString("F0") + "~" + UpperAttackDamage.ToString("F0");
        M_Defense.text = Defense.ToString("F0");
        M_CritRate.text = CritRate.ToString("F0") + "%";
        M_Time.text = Minutes.ToString("F0") + "m " + Seconds.ToString("F0") + "s";

        if (Seconds <= 9f)
            M_Time.text = Minutes.ToString("F0") + "m 0" + Seconds.ToString("F0") + "s";


        StaminaTimer += Time.deltaTime;

        HealthPercent = Health / MaxHealth * 100;

        if (HealthPercent <= 20)
            LowHPWarning.SetActive(true);

        if (HealthPercent >= 20)
            LowHPWarning.SetActive(false);

        if (Stamina < 100f)
        {
            Stamina += 10f * Time.deltaTime;
        }

        if (StaminaNotEnough)
            StaminaTimer += Time.deltaTime;

        if (!StaminaNotEnough)
            StaminaTimer = 0;

        if (StaminaTimer > 1)
        {
            StaminaWarning.SetActive(false);
            StaminaNotEnough = false;
        }

        if (Stamina < 0f)
        {
            Stamina = 0f;
        }

        if (Stamina <= 0f && StaminaNotEnough == false)
        {
            StaminaNotEnough = true;
            StaminaWarning.SetActive(true);
            StaminaAnim.Play("StaminaWarningAnim");
        }

        MaxEXP = 25 * level;

        if (EXP >= MaxEXP)
        {
            level++;
            EXP = 0;

            MaxHealth = MaxHealth + Random.Range(40, 50);
            Health = MaxHealth;

            LowerAttackDamage += 4;
            UpperAttackDamage += 5;
            Defense += 1;
            CritRate += 1;
        }

        LevelText.text = "LV." + level.ToString();
    }
}
