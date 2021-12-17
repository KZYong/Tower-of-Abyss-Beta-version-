using UnityEngine;
using System.Collections;
using TMPro;

public class ActivateChest : MonoBehaviour
{

	public Transform lid, lidOpen, lidClose;    // Lid, Lid open rotation, Lid close rotation
	public float openSpeed = 5F;                // Opening speed
	public bool canClose;                       // Can the chest be closed

	[HideInInspector]
	public bool _open;                          // Is the chest opened

	public bool canOpen;

	public bool doneOpen;

	public bool ChestUnlock;

	public bool Opened;

	private StarterAssets.ThirdPersonController tpc;
	PlayerStats PlayerS;

	public GameObject Shiny;
	public float EXPGet;

	public GameObject EXPPlus;
	private Animator EXPAnim;
	public TextMeshProUGUI EXPAmount;

	public GameObject ItemTextObject;
	private Animator ItemAnim;
	public TextMeshProUGUI ItemName;

	public GameObject RewardTextObject;
	private Animator RewardAnim;

	public float RewardTimer;
	public bool Rewarding;

	public float Roll;

	private Enemy1 Enemy;
	private Enemy1 Enemy2;
	private Enemy1 Enemy3;
	public GameObject FirstEnemy;
	public GameObject SecondEnemy;
	public GameObject ThirdEnemy;

	public GameObject ChestLockEffect;

	public AudioSource ChestSound;
	public AudioSource RewardSound;

	public GameObject Player;

	public int EnemyNumbers;

	private void Start()
    {
		tpc = FindObjectOfType<StarterAssets.ThirdPersonController>();
		PlayerS = FindObjectOfType<PlayerStats>();
		Player = GameObject.FindWithTag("Player");

		EXPAnim = EXPPlus.GetComponent<Animator>();
		RewardAnim = RewardTextObject.GetComponent<Animator>();
		ItemAnim = ItemTextObject.GetComponent<Animator>();

		Enemy = FirstEnemy.GetComponent<Enemy1>();
		Enemy2 = SecondEnemy.GetComponent<Enemy1>();
		Enemy3 = ThirdEnemy.GetComponent<Enemy1>();
	}

    void Update()
	{
		if (!ChestUnlock && !Opened)
			ChestLockEffect.SetActive(true);
		if (ChestUnlock)
			ChestLockEffect.SetActive(false);

		//Unlock Chest (According the Enemy Count)

		if (!Opened)
		{
			if (EnemyNumbers == 0)
			{
				ChestUnlock = true;
			}

			if (EnemyNumbers == 1)
			{
				if (Enemy.isDeath == true)
				{
					ChestUnlock = true;
				}
			}

			if (EnemyNumbers == 2)
			{
				if (Enemy.isDeath == true && Enemy2.isDeath == true)
				{
					ChestUnlock = true;
				}
			}

			if (EnemyNumbers == 3)
			{
				if (Enemy.isDeath == true && Enemy2.isDeath == true && Enemy3.isDeath == true)
				{
					ChestUnlock = true;
				}
			}
		}

		if (Rewarding)
        {
			RewardTimer += Time.deltaTime;

			if (RewardTimer > 9)
            {
				Rewarding = false;
				EXPPlus.SetActive(false);
				RewardTextObject.SetActive(false);
				ItemTextObject.SetActive(false);
			}				
        }

		if (!Rewarding)
			RewardTimer = 0;

		if (_open)
		{

			if (!doneOpen)
			{
			//	SavedData.LoadedPositionX = Player.transform.position.x;
			//	SavedData.LoadedPositionY = Player.transform.position.y + 1;
			//	SavedData.LoadedPositionZ = Player.transform.position.z;

				Roll = Random.Range(1, 100);

				if (Roll <= 30)
                {
					ItemName.text = "Greater Healing Potion x1";
					PlayerS.GreaterPotion += 1;
                }

				if (Roll >= 31)
				{
					ItemName.text = "Lesser Healing Potion x1";
					PlayerS.LesserPotion += 1;
				}

				GameObject hObject = Instantiate(Shiny, new Vector3(transform.position.x, (transform.position.y), transform.position.z), Quaternion.Euler(new Vector3(90, Random.Range(0, 360), 0))) as GameObject;
				Destroy(hObject, 1);

				EXPGet = Random.Range(14, 18);
				PlayerS.EXP += EXPGet;

				EXPAmount.text = "EXP+" + EXPGet.ToString("F0");
				EXPPlus.SetActive(true);
				EXPAnim.Play("EXP_PLUS_ANIM");

				RewardTextObject.SetActive(true);
				RewardAnim.Play("EXP_PLUS_ANIM");

				ItemTextObject.SetActive(true);
				ItemAnim.Play("EXP_PLUS_ANIM");

				ChestSound.Play();
				RewardSound.Play();

				doneOpen = true;
				Rewarding = true;
				Opened = true;
			}
		}

		if (Opened)
		{
			ChestClicked(lidOpen.rotation);
			ChestLockEffect.SetActive(false);
			//ChestUnlock = false;
		}
	}

	// Rotate the lid to the requested rotation
	void ChestClicked(Quaternion toRot)
	{
		if (lid.rotation != toRot)
		{
			lid.rotation = Quaternion.Lerp(lid.rotation, toRot, Time.deltaTime * openSpeed);
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player")
        {
			if (ChestUnlock && !doneOpen && !Opened)
			{
				canOpen = true;
				tpc.TheChest = this.gameObject;
				tpc.canChest = true;
			}

			if (doneOpen)
            {
				canOpen = false;
				tpc.canChest = false;
            }
        }
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			canOpen = false;
			tpc.canChest = false;
		}
	}
}
