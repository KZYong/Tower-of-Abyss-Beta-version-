using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NewStats
{
    public float positionX;
    public float positionY;
    public float positionZ;
    public int CurrentStage;
    public float CurrentHealth;
    public float CurrentMaxHealth;
    public int CurrentLevel;
    public float CurrentEXP;
    public float CurrentMaxEXP;
    public float CurrentCritRate;
    public float CurrentDefense;
    public float CurrentSeconds;
    public float CurrentAttackLow;
    public float CurrentAttackHigh;
    public int CurrentPot1;
    public int CurrentPot2;

    public bool FirstDialogue;
    public bool Camp1;
    public bool Camp2;
    public bool Camp3;
    public bool Camp4;
    public bool Camp5;
    public bool Camp6;
    public bool Camp7;

    public bool FreeChest1;
    public bool FreeChest2;
    public bool FreeChest3;
    public bool FreeChest4;
    public bool FreeChest5;

    public NewStats(int stage, float posX, float posY, float posZ, float HP, float MaxHP, int Level, float EXP, float MaxEXP, 
        float CritRate, float Def, float Sec, float AttL, float AttH, int Pot1, int Pot2, bool C1, bool C2, bool C3, bool C4, bool C5,
        bool C6, bool C7, bool FC1, bool FC2, bool FC3, bool FC4, bool FC5,
        bool FirstDia)
    {
        positionX = posX;
        positionY = posY;
        positionZ = posZ;
        CurrentStage = stage;
        CurrentHealth = HP;
        CurrentMaxHealth = MaxHP;
        CurrentLevel = Level;
        CurrentEXP = EXP;
        CurrentMaxEXP = MaxEXP;
        CurrentCritRate = CritRate;
        CurrentDefense = Def;
        CurrentSeconds = Sec;
        CurrentAttackLow = AttL;
        CurrentAttackHigh = AttH;
        CurrentPot1 = Pot1;
        CurrentPot2 = Pot2;
        Camp1 = C1;
        Camp2 = C2;
        Camp3 = C3;
        Camp4 = C4;
        Camp5 = C5;
        Camp6 = C6;
        Camp7 = C7;
        FreeChest1 = FC1;
        FreeChest2 = FC2;
        FreeChest3 = FC3;
        FreeChest4 = FC4;
        FreeChest5 = FC5;
        FirstDialogue = FirstDia;
    }
}
