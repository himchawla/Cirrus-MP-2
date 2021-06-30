using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InbetweenAmbience : MonoBehaviour
{
    [SerializeField] private AudioSource[] m_AmbienceNoises;
    
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            //Randomly play a ambience noise
            foreach (AudioSource a in m_AmbienceNoises)
            {
                if (a.isPlaying)
                {
                    return;
                }
            }

            int noise = Random.Range(0, m_AmbienceNoises.Length - 1);

            m_AmbienceNoises[noise].Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            //Randomly play a ambience noise
            foreach (AudioSource a in m_AmbienceNoises)
            {
                a.Stop();
            }
        }
    }
}
