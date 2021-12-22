using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampManager2 : MonoBehaviour
{
    public PlayerStats PlayerS;

    public GameObject[] Chests;

    public ActivateChest[] chestscript;

    public int index;

    public GameObject[] Enemies;

    // Start is called before the first frame update
    void Start()
    {
        PlayerS = FindObjectOfType<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < index; i++)
        {
            chestscript[i] = Chests[i].GetComponent<ActivateChest>();

            if (chestscript[i].Opened == true)
            {
                if (i == 0)
                    PlayerS.Camp1 = true;

                if (i == 1)
                    PlayerS.Camp2 = true;

                if (i == 2)
                    PlayerS.Camp3 = true;

                if (i == 3)
                    PlayerS.Camp4 = true;

                if (i == 4)
                    PlayerS.FreeChest1 = true;
                if (i == 5)
                    PlayerS.FreeChest2 = true;
                if (i == 6)
                    PlayerS.FreeChest3 = true;
                if (i == 7)
                    PlayerS.FreeChest4 = true;
                if (i == 8)
                    PlayerS.FreeChest5 = true;
            }


        }

        if (PlayerS.Camp1)
            chestscript[0].Opened = true;
        if (PlayerS.Camp2)
            chestscript[1].Opened = true;
        if (PlayerS.Camp3)
            chestscript[2].Opened = true;
        if (PlayerS.Camp4)
            chestscript[3].Opened = true;

        if (PlayerS.FreeChest1)
            chestscript[4].Opened = true;
        if (PlayerS.FreeChest2)
            chestscript[5].Opened = true;
        if (PlayerS.FreeChest3)
            chestscript[6].Opened = true;
        if (PlayerS.FreeChest4)
            chestscript[7].Opened = true;
        if (PlayerS.FreeChest5)
            chestscript[8].Opened = true;

    }

    public void LoadCamp()
    {

        if (PlayerS.Camp1)
        {
            Enemies[0].SetActive(false);
            Enemies[1].SetActive(false);
        }

        if (PlayerS.Camp2)
        {
            Enemies[2].SetActive(false);
            Enemies[3].SetActive(false);
            Enemies[4].SetActive(false);
        }


        if (PlayerS.Camp3)
        {
            Enemies[5].SetActive(false);
            Enemies[6].SetActive(false);
            Enemies[7].SetActive(false);
        }

        if (PlayerS.Camp4)
        {
            Enemies[8].SetActive(false);
            Enemies[9].SetActive(false);
            Enemies[10].SetActive(false);
        }
    }
}
