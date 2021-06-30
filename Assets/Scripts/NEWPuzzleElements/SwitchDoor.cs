using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SwitchDoor : MonoBehaviour
{
    [FormerlySerializedAs("Switches")] public ItemSlot[] switches;

    bool m_isOpen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        foreach(ItemSlot Switch in switches)
        {
            if (Switch.m_isConnected)
            {
                i++;
            }
        }

        if(i == switches.Length)
        {
            m_isOpen = true;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        } else
        {
            m_isOpen = false;
            gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
