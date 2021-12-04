using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHP : MonoBehaviour
{
    private Image eHealthBars;
    public float eCurrentHealth;
    public float eMaxHealth;
    Enemy1 Enemy;

    public TextMeshProUGUI eHPNum;

    public GameObject EnemyObject;


    // Start is called before the first frame update
    void Start()
    {
        eHealthBars = GetComponent<Image>();
        Enemy = EnemyObject.GetComponent<Enemy1>();
    }

    // Update is called once per frame
    void Update()
    {


        eCurrentHealth = Enemy.eHealth;
        eMaxHealth = Enemy.eMaxHealth;

        eHealthBars.fillAmount = eCurrentHealth / eMaxHealth;
        //eHPNum.text = CurrentHealth.ToString("F0") + "/" + MaxHealth.ToString("F0");
    }

    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
    }
}