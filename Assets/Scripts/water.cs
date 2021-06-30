using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eWater : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider _other)
    {

        if (_other.CompareTag("Player"))
            _other.gameObject.transform.GetChild(4).gameObject.GetComponent<Animator>().SetBool("inWater", true);
    }

    private void OnTriggerExit(Collider _other)
    {
        if(_other.CompareTag("Player"))
        _other.gameObject.transform.GetChild(4).gameObject.GetComponent<Animator>().SetBool("inWater", false);
    }
}
