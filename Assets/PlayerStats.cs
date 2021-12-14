using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float Health = 100f;
    public float MaxHealth = 100f;
    public float Stamina = 100f;

    public float HealthPercent;

    public float Defense = 5f;
    public float CritRate = 5f;

    public float LowerAttackDamage = 1f;
    public float UpperAttackDamage = 5f;

    public GameObject LowHPWarning;
    public GameObject StaminaWarning;
    private Animator StaminaAnim;

    private float StaminaTimer;
    private bool StaminaNotEnough;

    // Start is called before the first frame update
    void Start()
    {
        StaminaAnim = StaminaWarning.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
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
    }
}
