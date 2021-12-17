using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NewStats
{
    public float positionX;
    public float positionY;
    public float positionZ;
    public int CurrentStage = 1;
    public float CurrentHealth;
    public float CurrentMaxHealth;

    public NewStats(int stage, float posX, float posY, float posZ, float HP, float MaxHP)
    {
        positionX = posX;
        positionY = posY;
        positionZ = posZ;
        CurrentStage = stage;
        CurrentHealth = HP;
        CurrentMaxHealth = MaxHP;
    }
}
