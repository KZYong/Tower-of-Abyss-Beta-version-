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

    public static int LoadStage;

    public static bool StartDialogue;

    public GameObject Player;
    public static bool NewGame;
    public static bool Load;
    public static bool LoadStats;
    public static bool PlayerDead;

    public static bool Level1;
    public static bool Level2;
    public static bool Boss;

    // Start is called before the first frame update
    void Start()
    {
        if (NewGame == true)
        {
            LoadedPositionX = 0;
            LoadedPositionY = 0.9299f;
            LoadedPositionZ = 0;

            StartDialogue = false;
            NewGame = false;
        }

      
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

        Debug.Log("Saved Position is " + data.positionX + "," + data.positionY + "," + data.positionZ); 
    }
}
