using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private Image ProgressBars;

    public float CurrentProgress;

    // Start is called before the first frame update
    void Start()
    {
        ProgressBars = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        ProgressBars.fillAmount = CurrentProgress / 100f;
    }
}
