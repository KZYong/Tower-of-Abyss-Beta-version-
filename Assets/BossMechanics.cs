using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossMechanics : MonoBehaviour
{
    Boss boss;

    public GameObject BossMessage;
    public TextMeshProUGUI BossMessageText;
    public GameObject MechMessage;
    public TextMeshProUGUI MechMessageText;
    public Animator BossMAnim;
    public Animator MechMAnim;

    public bool messageOn;
    public float messagetimer;

    public bool First; public bool FirstStart; private bool FirstDone;
    public bool Second; public bool SecondStart; private bool SecondDone;

    public float HPPercent;

    public GameObject Enemy1;public GameObject Enemy2;public GameObject Enemy3;
    private bool EnemyA1; private bool EnemyA2; private bool EnemyA3;

    public GameObject ImmuneEffect;

    public GameObject[] Enemies;
    public GameObject[] EnemiesNotInRange;
    public int TotalEnemies;

    public GameObject Orb1; public GameObject Orb2; public GameObject Orb3; public GameObject Orb4;
    public int OrbCount = 4;
    public float OrbTimer = 30;

    private float amountDamage;
    private StarterAssets.ThirdPersonController tpc;
    PlayerStats Player;
    public GameObject FloatingTextPrefab;
    public Transform player;
    public AudioSource EnemyHit;

    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<PlayerStats>();
        boss = gameObject.GetComponent<Boss>();
        BossMAnim = BossMessage.GetComponent<Animator>();
        MechMAnim = BossMessage.GetComponent<Animator>();
        tpc = player.GetComponent<StarterAssets.ThirdPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        Enemies = GameObject.FindGameObjectsWithTag("EnemyInRange");
        EnemiesNotInRange = GameObject.FindGameObjectsWithTag("Enemy");

        TotalEnemies = Enemies.Length + EnemiesNotInRange.Length;

        HPPercent = boss.eHealth / boss.eMaxHealth * 100;

        if (messageOn)
            messagetimer += Time.deltaTime;
        if (!messageOn)
            messagetimer = 0;

        if (HPPercent <= 80 && !First && !boss.isAttacking)
            FirstMech();
        if (HPPercent <= 50 && !Second && !boss.isAttacking)
            SecondMech();

        if (First && !FirstDone)
        {
            if (messagetimer >= 1) FirstStart = true;
            if (messagetimer >= 10)
            {
                BossMessage.SetActive(false);
                messageOn = false;
            }
        }
        if (FirstStart)
        {
            if (TotalEnemies == 1) 
            {
                boss.isImmune = false;
                ImmuneEffect.SetActive(false);
                FirstStart = false;
                FirstDone = true;
            }
        }
        if (Second && !SecondDone)
        {
            OrbTimer -= Time.deltaTime;
            MechMessageText.text = "Destroy the orbs...! Orbs exploding in..." + OrbTimer.ToString("F0");

            if (messagetimer >= 5)
            {
                BossMessage.SetActive(false);
                messageOn = false;
                MechMessage.SetActive(true);
                MechMAnim.Play("BossMessageAnim");
            }
            if (OrbCount <= 0)
            {
                boss.isImmune = false;
                ImmuneEffect.SetActive(false);
                SecondDone = true;
                MechMessage.SetActive(false);
            }
            if (OrbTimer <= 0)
            {
                //Insert Damage Player Script
                boss.isImmune = false;
                ImmuneEffect.SetActive(false);
                SecondDone = true;
                MechMessage.SetActive(false);
                Orb1.SetActive(false); Orb2.SetActive(false); Orb3.SetActive(false); Orb4.SetActive(false);
                DamagePlayer();
            }
        }
    }

    public void FirstMech()
    {
        First = true;
        Enemy1.SetActive(true);
        Enemy2.SetActive(true);
        Enemy3.SetActive(true);

        BossMessage.SetActive(true);
        BossMessageText.text = "Come... my minions!!!";
        BossMAnim.Play("BossMessageAnim");
        messageOn = true;

        boss.isImmune = true;
        ImmuneEffect.SetActive(true);
    }

    public void SecondMech()
    {
        Second = true;
        Orb1.SetActive(true);
        Orb2.SetActive(true);
        Orb3.SetActive(true);
        Orb4.SetActive(true);

        BossMessage.SetActive(true);
        BossMessageText.text = "Can you survive this? Hahaha";
        BossMAnim.Play("BossMessageAnim");
        messageOn = true;

        boss.isImmune = true;
        ImmuneEffect.SetActive(true);
    }

    public void DamagePlayer()
    {
        amountDamage = Player.MaxHealth / 2;
        Player.Health = Player.Health - amountDamage;
        tpc.isHit = true;
        tpc.isHitAnim = true;

        var go = Instantiate(FloatingTextPrefab, new Vector3((player.position.x), (player.position.y + 1), player.position.z), Quaternion.identity);
        go.GetComponent<TextMeshPro>().text = amountDamage.ToString("F0");

        EnemyHit.Play();
    }
}
