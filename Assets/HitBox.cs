using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public GameObject hiteffect;

    private int _attackPower = 1;

    private bool _canAttack;

    private void OnEnable()
    {
        _canAttack = true;
    }

    public GameObject player;

    private bool HisAttack;

    private bool HisJumpAttack;

    private StarterAssets.ThirdPersonController tpc;


    // Start is called before the first frame update
    void Start()
    {
        tpc = player.GetComponent<StarterAssets.ThirdPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        HisAttack = tpc.isAttack;
        HisJumpAttack = tpc.isJumpAttack;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && HisAttack == true)
        {

            GameObject hObject = Instantiate(hiteffect, other.gameObject.transform.position, Quaternion.Euler(new Vector3(90, Random.Range(0, 360), 0))) as GameObject;
            Destroy(hObject, 1);
            //hit.Damage(attackPower);
            //_canAttack = false;
        }

        if (other.gameObject.tag == "Enemy" && HisJumpAttack == true)
        {

            GameObject hObject = Instantiate(hiteffect, new Vector3(other.gameObject.transform.position.x, (player.transform.position.y + 1) ,other.gameObject.transform.position.z), Quaternion.Euler(new Vector3(90, Random.Range(0, 360), 0))) as GameObject;
            Destroy(hObject, 1);
            //hit.Damage(attackPower);
            //_canAttack = false;
        }
    }
}
