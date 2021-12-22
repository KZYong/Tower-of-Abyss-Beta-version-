using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetNextLevel : MonoBehaviour
{
    public bool startReset;
    public float resettimer;

    // Start is called before the first frame update
    void Start()
    {
        startReset = true;
    }

    // Update is called once per frame
    void Update()
    {
        resettimer += Time.deltaTime;

        if (!startReset)
            resettimer = 0;

        if (startReset)
        {
            if (resettimer >= 5)
            {
                SavedData.NextLevel = false;
            }
        }
    }
}
