using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SavedData : MonoBehaviour
{
    public static SavedData loaddata;

    public static float LoadedPositionX;
    public static float LoadedPositionY;
    public static float LoadedPositionZ;

    public static float LoadedHealth;
    public static float LoadedMaxHealth;
    public static int LoadedLevel;
    public static float LoadedEXP;
    public static float LoadedMaxEXP;
    public static float LoadedCritRate;
    public static float LoadedDefense;
    public static float LoadedSeconds;
    public static float LoadedAttackLow;
    public static float LoadedAttackHigh;
    public static int LoadedHPPot1;
    public static int LoadedHPPot2;

    public static bool LoadedCamp1;
    public static bool LoadedCamp2;
    public static bool LoadedCamp3;
    public static bool LoadedCamp4;
    public static bool LoadedCamp5;
    public static bool LoadedCamp6;
    public static bool LoadedCamp7;

    public static bool LoadedFreeChest1;
    public static bool LoadedFreeChest2;
    public static bool LoadedFreeChest3;

    public static int LoadStage;

    public static bool StartDialogue;

    public GameObject Player;
    public static bool NewGame;
    public static bool Load;
    public static bool LoadStats;
    public static bool MenuLoad;
    public static bool PlayerDead;

    public static bool Level1;
    public static bool Level2;
    public static bool Boss;

    // Start is called before the first frame update
    void Start()
    {


      
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("Current Saved Position is " + LoadedPositionX + "," + LoadedPositionY + "," + LoadedPositionZ);

        if (LoadStats == true)
        {
            LoadData();
            LoadStats = false;
        }
    }

    public void LoadData()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenRead(destination);
        else
        {
            Debug.LogError("Save File not found");
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        NewStats data = (NewStats)bf.Deserialize(file);
        file.Close();

        LoadStage = data.CurrentStage;
        LoadedPositionX = data.positionX;
        LoadedPositionY = data.positionY;
        LoadedPositionZ = data.positionZ;
        LoadedHealth = data.CurrentHealth;
        LoadedMaxHealth = data.CurrentMaxHealth;
        LoadedLevel = data.CurrentLevel;
        LoadedEXP = data.CurrentEXP;
        LoadedMaxEXP = data.CurrentMaxEXP;
        LoadedCritRate = data.CurrentCritRate;
        LoadedDefense = data.CurrentDefense;
        LoadedSeconds = data.CurrentSeconds;
        LoadedAttackLow = data.CurrentAttackLow;
        LoadedAttackHigh = data.CurrentAttackHigh;
        LoadedHPPot1 = data.CurrentPot1;
        LoadedHPPot2 = data.CurrentPot2;
        LoadedCamp1 = data.Camp1;
        LoadedCamp2 = data.Camp2;
        LoadedCamp3 = data.Camp3;
        LoadedCamp4 = data.Camp4;
        LoadedCamp5 = data.Camp5;
        LoadedCamp6 = data.Camp6;
        LoadedCamp7 = data.Camp7;
        LoadedFreeChest1 = data.FreeChest1;
        LoadedFreeChest2 = data.FreeChest2;
        LoadedFreeChest3 = data.FreeChest3;
        StartDialogue = data.FirstDialogue;

        //Debug.Log("Saved Position is " + data.positionX + "," + data.positionY + "," + data.positionZ); 
    }
}
