using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Manager : MonoBehaviour
{
    public PlayerStats PlayerS;

    // Start is called before the first frame update
    void Start()
    {
        PlayerS = FindObjectOfType<PlayerStats>();

        PlayerS.ThisStage = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
