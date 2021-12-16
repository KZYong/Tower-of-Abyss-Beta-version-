using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILookAtCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(Camera.main.transform);
    }

    private void LateUpdate()
    {
        //var direction = Camera.main.transform.position - transform.position;
        //var lookRotation = Quaternion.LookRotation(direction);
        //transform.rotation = lookRotation;

        //transform.rotation = Quaternion.LookRotation(transform.position - GetComponent<Camera>().transform.position);

        transform.rotation = Camera.main.transform.rotation;




        //transform.LookAt(Camera.main.transform);
    }
}
