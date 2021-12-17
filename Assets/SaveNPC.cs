using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

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
			SavedData.LoadedSeconds = PlayerS.TotalSeconds;
			SavedData.LoadedAttackLow = PlayerS.LowerAttackDamage;
			SavedData.LoadedAttackHigh = PlayerS.UpperAttackDamage;
			SavedData.LoadedHPPot1 = PlayerS.LesserPotion;
			SavedData.LoadedHPPot2 = PlayerS.GreaterPotion;

			if (PlayerS.Camp1) SavedData.LoadedCamp1 = true;
			if (!PlayerS.Camp1) SavedData.LoadedCamp1 = false;

			if (PlayerS.Camp2) SavedData.LoadedCamp2 = true;
			if (!PlayerS.Camp2) SavedData.LoadedCamp2 = false;

			if (PlayerS.Camp3) SavedData.LoadedCamp3 = true;
			if (!PlayerS.Camp3) SavedData.LoadedCamp3 = false;

			if (PlayerS.Camp4) SavedData.LoadedCamp4 = true;
			if (!PlayerS.Camp4) SavedData.LoadedCamp4 = false;

			if (PlayerS.Camp5) SavedData.LoadedCamp5 = true;
			if (!PlayerS.Camp5) SavedData.LoadedCamp5 = false;

			if (PlayerS.Camp6) SavedData.LoadedCamp6 = true;
			if (!PlayerS.Camp6) SavedData.LoadedCamp6 = false;

			if (PlayerS.Camp7) SavedData.LoadedCamp7 = true;
			if (!PlayerS.Camp7) SavedData.LoadedCamp7 = false;

			if (PlayerS.FreeChest1) SavedData.LoadedFreeChest1 = true;
			if (!PlayerS.FreeChest1) SavedData.LoadedFreeChest1 = false;

			if (PlayerS.FreeChest2) SavedData.LoadedFreeChest2 = true;
			if (!PlayerS.FreeChest2) SavedData.LoadedFreeChest2 = false;

			if (PlayerS.FreeChest3) SavedData.LoadedFreeChest3 = true;
			if (!PlayerS.FreeChest3) SavedData.LoadedFreeChest3 = false;


			SavedData.LoadStage = PlayerS.ThisStage;

			if (SavedData.NewGame)
				SavedData.NewGame = false;

			//

			SaveToFile();

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

			//Debug.Log("Save collided!");

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

	public void SaveToFile()
    {
		string destination = Application.persistentDataPath + "/save.dat";
		FileStream file;

		if (File.Exists(destination)) file = File.OpenWrite(destination);
		else file = File.Create(destination);

		NewStats data = new NewStats(SavedData.LoadStage,
			SavedData.LoadedPositionX, SavedData.LoadedPositionY, SavedData.LoadedPositionZ,
			SavedData.LoadedHealth, SavedData.LoadedMaxHealth,
			SavedData.LoadedLevel, SavedData.LoadedEXP, SavedData.LoadedMaxEXP,
			SavedData.LoadedCritRate, SavedData.LoadedDefense, SavedData.LoadedSeconds,
			SavedData.LoadedAttackLow, SavedData.LoadedAttackHigh,
			SavedData.LoadedHPPot1, SavedData.LoadedHPPot2,
			SavedData.LoadedCamp1, SavedData.LoadedCamp2, SavedData.LoadedCamp3, SavedData.LoadedCamp4, SavedData.LoadedCamp5, SavedData.LoadedCamp6,
			SavedData.LoadedCamp7, SavedData.LoadedFreeChest1, SavedData.LoadedFreeChest2, SavedData.LoadedFreeChest3,
			SavedData.StartDialogue
			);
		BinaryFormatter bf = new BinaryFormatter();
		bf.Serialize(file, data);
		file.Close();
	}
}
