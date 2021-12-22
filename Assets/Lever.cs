using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{

	private StarterAssets.ThirdPersonController tpc;

	public bool canInteract; 

	// Start is called before the first frame update
	void Start()
    {
		tpc = FindObjectOfType<StarterAssets.ThirdPersonController>();
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			canInteract = true;
			tpc.CanNPC = true;
			tpc.TheNPC = this.gameObject;

			//Debug.Log("Save collided!");

		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			canInteract = false;
			tpc.CanNPC = false;
		}
	}
}
