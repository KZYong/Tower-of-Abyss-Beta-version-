using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal1 : MonoBehaviour
{
	public bool canInteract;

	private StarterAssets.ThirdPersonController tpc;

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
			tpc.canPortal = true;
			tpc.ThePortal = this.gameObject;

			//Debug.Log("Save collided!");

		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			canInteract = false;
			tpc.canPortal = false;
		}
	}
}
