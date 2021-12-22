using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrbHP : MonoBehaviour
{
    private Image eHealthBars;
    public float eCurrentHealth;
    public float eMaxHealth;
    public GameObject Orb;
    ExplosionOrb OrbScript;
    // Start is called before the first frame update
    void Start()
    {
        eHealthBars = GetComponent<Image>();
        OrbScript = Orb.GetComponent<ExplosionOrb>();
    }

    // Update is called once per frame
    void Update()
    {
        eCurrentHealth = OrbScript.OrbHealth;
        eMaxHealth = OrbScript.OrbMaxHealth;

        eHealthBars.fillAmount = eCurrentHealth / eMaxHealth;
    }

    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
    }
}
