using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpikeTrap : MonoBehaviour
{
    public float amountDamage;

    PlayerStats Player;

    public float eAttack;

    private StarterAssets.ThirdPersonController tpc;

    public GameObject FloatingTextPrefab;

    public Transform player;

    public AudioSource EnemyHit;

    Collider spikecollider;

    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<PlayerStats>();
        tpc = player.GetComponent<StarterAssets.ThirdPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !tpc.PlayerDeath)
        {
            eAttack = Random.Range(5, 15);
            amountDamage = Player.Defense / 100;
            amountDamage = eAttack * (1 - amountDamage);
            Player.Health = Player.Health - amountDamage;
            tpc.isHit = true;
            tpc.isHitAnim = true;

            var go = Instantiate(FloatingTextPrefab, new Vector3((player.position.x), (player.position.y + 1), player.position.z), Quaternion.identity);
            go.GetComponent<TextMeshPro>().text = amountDamage.ToString("F0");

            EnemyHit.Play();
        }
    }
}
