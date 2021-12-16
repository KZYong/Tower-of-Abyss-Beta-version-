using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComboNumber : MonoBehaviour
{
    public TextMeshProUGUI ComboNumberUI;
    public TextMeshProUGUI TotalDamageUI;

    public int combonumber;

    public float combotime;
    public float combofinishtime = 5f;

    public float totaldamage;

    HitBox hitbox;

    public Animator comboAnim;

    public Animation Canim;

    //public GameObject Player;
    public GameObject ComboUI;

    // Start is called before the first frame update
    void Start()
    {
        hitbox = this.GetComponent<HitBox>();

        comboAnim = ComboNumberUI.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (combonumber >= 1)
        {
            ComboUI.SetActive(true);
            combotime += Time.deltaTime;

            ComboNumberUI.text = combonumber + "<size=35>COMBO!</size>";

            TotalDamageUI.text = "Total Damage: " + totaldamage.ToString("F0");
        }

        if (combonumber == 0)
        {
            ComboUI.SetActive(false);
            combotime = 0;
            totaldamage = 0;
        }

           if (combonumber < 0)
            {
                combonumber = 0;
            }

           if (combotime > combofinishtime)
           {
               combonumber = 0;
           }
    } 
}
