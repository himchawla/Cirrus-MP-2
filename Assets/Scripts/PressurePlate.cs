using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.attachedRigidbody.mass >= 10)
        {
            Debug.Log("success");
            //Insert functions we can attach
        }
    }
}
