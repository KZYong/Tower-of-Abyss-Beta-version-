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

    public int GreaterPotion;
    public int LesserPotion;

    public float Timer;
    public float Minutes;
    public float Seconds;
    public float TotalSeconds;

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

    public float LastPositionX;
    public float LastPositionY;
    public float LastPositionZ;

    public float deathtimer;

    private StarterAssets.ThirdPersonController tpc;
    private GameObject Player;

    public GameObject GreatPot;
    public GameObject LessPot;
    public TextMeshProUGUI GreatPotText;
    public TextMeshProUGUI LessPotText;

    public GameObject LevelUpText;
    public GameObject LevelUpEffect;
    public Animator LevelUpAnim;
    public Animator LevelUpTextAnim;

    public AudioSource LevelUpSound;
    public GameObject LevelUpVFX;

    public float LevelUpTime;
    public bool LevelUpDone;

    public GameObject DeadScreen;
    public GameObject DeadPanel;
    private Animator DeadPanelAnim;
    public GameObject DeadText;
    public GameObject DeadGlow;
    private Animator DeadGlowAnim;
    private float DeadTimer;

    public int ThisStage;

    CampManager campmanage;

    public bool Camp1; public bool Camp2; public bool Camp3; public bool Camp4; public bool Camp5; public bool Camp6; public bool Camp7;
    public bool FreeChest1; public bool FreeChest2; public bool FreeChest3;

    void Start()
    {
        StaminaAnim = StaminaWarning.GetComponent<Animator>();

        tpc = GetComponent<StarterAssets.ThirdPersonController>();

        LevelUpAnim = LevelUpEffect.GetComponent<Animator>();
        LevelUpTextAnim = LevelUpText.GetComponent<Animator>();
        DeadGlowAnim = DeadGlow.GetComponent<Animator>();
        DeadPanelAnim = DeadPanel.GetComponent<Animator>();

        campmanage = FindObjectOfType<CampManager>();

        Player = GameObject.FindWithTag("MainPlayer");

        LastPositionX = SavedData.LoadedPositionX;
        LastPositionY = SavedData.LoadedPositionY;
        LastPositionZ = SavedData.LoadedPositionZ;

        //LoadGame
        if (SavedData.Load && !SavedData.NewGame)
        {
            LoadStats();
            SavedData.Load = false;
        }

        if (SavedData.PlayerDead && !SavedData.NewGame)
        {
            LoadStats();
        }

        if (SavedData.PlayerDead)
        {
            LoadTime();
            SavedData.PlayerDead = false;
        }

        if (SavedData.NextLevel)
        {
            LoadStats();
            SavedData.NextLevel = false;
        }

    }

    // Update is called once per frame
    void Update()
    {


        if (Health <= 0)
        {
            SavedData.LoadedSeconds = TotalSeconds;

            DeadScreen.SetActive(true);
            DeadPanel.SetActive(true);
            DeadPanelAnim.Play("DeadPanelAnim");

            DeadTimer += Time.deltaTime;

            if (DeadTimer >= 2)
            {
                DeadText.SetActive(true);
                DeadGlow.SetActive(true);
                DeadGlowAnim.Play("LevelUpAnim");
            }
            
        }

        if (LevelUpDone)
        {
            LevelUpTime += Time.deltaTime;
            if (LevelUpTime > 4)
            {
                LevelUpDone = false;
                LevelUpText.SetActive(false);
            }
        }
        if (!LevelUpDone)
        {
            LevelUpTime = 0;
        }

        if (Health > MaxHealth)
            Health = MaxHealth;

        GreatPotText.text = "x" + GreaterPotion.ToString();
        LessPotText.text = "x" + LesserPotion.ToString();

        if (GreaterPotion >= 1)
        {
            GreatPot.SetActive(true);
        }
        if (LesserPotion >= 1)
        {
            LessPot.SetActive(true);
        }
        if (GreaterPotion <= 0)
        {
            if (GreaterPotion < 0)
                GreaterPotion = 0;

            GreatPot.SetActive(false);
        }
        if (LesserPotion <= 0)
        {
            if (LesserPotion < 0)
                LesserPotion = 0;

            LessPot.SetActive(false);
        }

        deathtimer += Time.deltaTime;

        if (tpc.PlayerDeath)
        {
            if (deathtimer > 5)
            {
                SavedData.LoadedSeconds = TotalSeconds;
                SavedData.PlayerDead = true;
                MainMenuManager.instance.ResetLevel1();
            }
        }

        if (!tpc.PlayerDeath)
            deathtimer = 0;

        Timer += Time.deltaTime;
        Seconds += Time.deltaTime;
        TotalSeconds += Time.deltaTime;

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

        if (level == 1)
            MaxEXP = 20;

        if (level >= 2)
        {
            MaxEXP = 20 + (25 * (level-1));
        }

        if (EXP >= MaxEXP)
        {
            EXP = EXP - MaxEXP;
            level++;
            
            MaxHealth = MaxHealth + Random.Range(15, 30);
            Health = MaxHealth;

            LowerAttackDamage += 4;
            UpperAttackDamage += 5;
            Defense += 1;
            CritRate += 1;

            LevelUpText.SetActive(true);
            LevelUpAnim.Play("LevelUpAnim");
            LevelUpTextAnim.Play("LevelUpFade");
            LevelUpDone = true;
            LevelUpSound.Play();

            GameObject loObject = Instantiate(LevelUpVFX, new Vector3(transform.position.x, (transform.position.y + 1), transform.position.z), Quaternion.Euler(new Vector3(90, Random.Range(0, 360), 0))) as GameObject;
            Destroy(loObject, 1);
        }

        LevelText.text = "LV." + level.ToString();
    }

    public void LoadStats()
    {
        Player.transform.position = new Vector3(LastPositionX, LastPositionY, LastPositionZ);

        //PlayerInformation Initialize
        Health = SavedData.LoadedHealth;
        MaxHealth = SavedData.LoadedMaxHealth;
        level = SavedData.LoadedLevel;
        MaxEXP = SavedData.LoadedMaxEXP;
        EXP = SavedData.LoadedEXP;
        CritRate = SavedData.LoadedCritRate;
        Defense = SavedData.LoadedDefense;
        Seconds = SavedData.LoadedSeconds;
        TotalSeconds = SavedData.LoadedSeconds;
        LowerAttackDamage = SavedData.LoadedAttackLow;
        UpperAttackDamage = SavedData.LoadedAttackHigh;
        LesserPotion = SavedData.LoadedHPPot1;
        GreaterPotion = SavedData.LoadedHPPot2;

        //MapInformation Initialize
        if (SavedData.LoadedCamp1) Camp1 = true;
        if (!SavedData.LoadedCamp1) Camp1 = false;

        if (SavedData.LoadedCamp2) Camp2 = true;
        if (!SavedData.LoadedCamp2) Camp2 = false;

        if (SavedData.LoadedCamp3) Camp3 = true;
        if (!SavedData.LoadedCamp3) Camp3 = false;

        if (SavedData.LoadedCamp4) Camp4 = true;
        if (!SavedData.LoadedCamp4) Camp4 = false;

        if (SavedData.LoadedCamp5) Camp5 = true;
        if (!SavedData.LoadedCamp5) Camp5 = false;

        if (SavedData.LoadedCamp6) Camp6 = true;
        if (!SavedData.LoadedCamp6) Camp6 = false;

        if (SavedData.LoadedCamp7) Camp7 = true;
        if (!SavedData.LoadedCamp7) Camp7 = false;

        campmanage.LoadCamp();

        if (SavedData.LoadedFreeChest1) FreeChest1 = true;
        if (!SavedData.LoadedFreeChest1) FreeChest1 = false;

        if (SavedData.LoadedFreeChest2) FreeChest2 = true;
        if (!SavedData.LoadedFreeChest2) FreeChest2 = false;

        if (SavedData.LoadedFreeChest3) FreeChest3 = true;
        if (!SavedData.LoadedFreeChest3) FreeChest3 = false;
    }

    public void LoadTime()
    {
        Seconds = SavedData.LoadedSeconds;
    }
}
