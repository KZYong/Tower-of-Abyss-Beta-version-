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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HealthPercent = Health / MaxHealth * 100;

        if (HealthPercent <= 20)
            LowHPWarning.SetActive(true);

        if (HealthPercent >= 20)
            LowHPWarning.SetActive(false);

        if (Stamina < 100f)
        {
            Stamina += 10f * Time.deltaTime;
        }

        if (Stamina < 0f)
        {
            Stamina = 0f;
        }
    }
}
