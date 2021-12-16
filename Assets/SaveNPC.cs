using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveNPC : MonoBehaviour
{
	public bool canInteract;
	public bool Saving;

	private StarterAssets.ThirdPersonController tpc;
	private PlayerStats PlayerS;

	public GameObject Player;
	

	// Start is called before the first frame update
	void Start()
    {
		tpc = FindObjectOfType<StarterAssets.ThirdPersonController>();
		PlayerS = FindObjectOfType<PlayerStats>();

		Player = GameObject.FindWithTag("Player");
	}

    // Update is called once per frame
    void Update()
    {
        if (Saving)
        {
			//Save Player Position
			SavedData.LoadedPositionX = Player.transform.position.x;
			SavedData.LoadedPositionY = Player.transform.position.y + 1;
			SavedData.LoadedPositionZ = Player.transform.position.z;

			SavedData.LoadedHealth = PlayerS.Health;
			SavedData.LoadedMaxHealth = PlayerS.MaxHealth;
			SavedData.LoadedLevel = PlayerS.level;
			SavedData.LoadedEXP = PlayerS.EXP;
			SavedData.LoadedMaxEXP = PlayerS.MaxEXP;
			SavedData.LoadedCritRate = PlayerS.CritRate;
			SavedData.LoadedDefense = PlayerS.Defense;
			SavedData.LoadedSeconds = PlayerS.Seconds;
			SavedData.LoadedAttackLow = PlayerS.LowerAttackDamage;
			SavedData.LoadedAttackHigh = PlayerS.UpperAttackDamage;
			SavedData.LoadedHPPot1 = PlayerS.LesserPotion;
			SavedData.LoadedHPPot2 = PlayerS.GreaterPotion;

			SavedData.LoadStage = 1;

			Saving = false;
        }
    }

	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			canInteract = true;
			tpc.canSave = true;
			tpc.TheSave = this.gameObject;

			Debug.Log("Save collided!");

		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			canInteract = false;
			tpc.canSave = false;
		}
	}
}
