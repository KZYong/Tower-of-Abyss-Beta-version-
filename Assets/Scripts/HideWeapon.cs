using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideWeapon : MonoBehaviour
{
    private StarterAssets.ThirdPersonController tpc;

    public GameObject weapon;
    public GameObject player;
    public GameObject heffect;

    public bool isHideWeapon;

    public float hidetimer;
    public float hidetime = 10f;


    // Start is called before the first frame update
    void Start()
    {
        tpc = player.GetComponent<StarterAssets.ThirdPersonController>();

        isHideWeapon = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (tpc.isAttack == true)
        {
            if (isHideWeapon == true)
            {
                weapon.SetActive(true);
                isHideWeapon = false;
            }
        }

        if (tpc.isGuard == true)
        {
            if (isHideWeapon == true)
            {
                weapon.SetActive(true);
                isHideWeapon = false;
            }
        }

        if (isHideWeapon == false)
        {
            if (tpc.isAttack == false)
            {
                hidetimer += Time.deltaTime;

                if (hidetimer >= hidetime)
                {
                    GameObject pObject = Instantiate(heffect, weapon.transform.position, Quaternion.Euler(new Vector3(90, Random.Range(0, 360), 0)), player.transform);
                    Destroy(pObject, 1);

                    weapon.SetActive(false);
                    isHideWeapon = true;
                }
            }
        }

        if (isHideWeapon == true)
        {
            hidetimer = 0;
        }

        if (tpc.isAttack == true)
        {
            hidetimer = 0;
        }

        if (tpc.isGuard == true)
        {
            hidetimer = 0;
        }
    }
}
