using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedData : MonoBehaviour
{
    public static SavedData loaddata;

    public static float LoadedPositionX;
    public static float LoadedPositionY;
    public static float LoadedPositionZ;

    public static float LoadedHealth;
    public static float LoadedMaxHealth;
    public static float LoadedLevel;
    public static float LoadedEXP;
    public static float LoadedMaxEXP;
    public static float LoadedCritRate;
    public static float LoadedDefense;
    public static float LoadedSeconds;
    public static float LoadedAttackLow;
    public static float LoadedAttackHigh;

    public GameObject Player;
    public static bool NewGame;
    public static bool Load;

    // Start is called before the first frame update
    void Start()
    {
        NewGame = true;

        if (NewGame == true)
        {
            LoadedPositionX = 0;
            LoadedPositionY = 0.9299f;
            LoadedPositionZ = 0;

            NewGame = false;
        }

      
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Current Saved Position is " + LoadedPositionX + "," + LoadedPositionY + "," + LoadedPositionZ);
    }

    public void LoadPlayer()
    {
        //Load Player Position
        Player.transform.position = new Vector3(LoadedPositionX, LoadedPositionY, LoadedPositionZ);
    }
}
