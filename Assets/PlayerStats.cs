using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float Health = 100f;
    public float Stamina = 100f;

    public float LowerAttackDamage = 1f;
    public float UpperAttackDamage = 5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
