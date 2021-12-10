using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPS_Check : MonoBehaviour
{
    public TextMeshProUGUI display_Text;

    public float deltaTime;

    public int avgFrameRate;

    public float FPSTimer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FPSTimer += Time.deltaTime;

        if (FPSTimer > 1)
        {
            float current = 0;
            current = Time.frameCount / Time.time;
            avgFrameRate = (int)current;
            display_Text.text = "FPS - " + avgFrameRate.ToString();
        }
    }
}
