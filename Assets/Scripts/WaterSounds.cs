using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class WaterSounds : MonoBehaviour
{
    // Start is called before the first frame update
    [FormerlySerializedAs("watersound")] public AudioSource m_watersound;
    [FormerlySerializedAs("overworld")] public AudioSource m_overworld;
    [FormerlySerializedAs("challenge")] public AudioSource m_challenge;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider _other)
    {
        if(_other.tag == "Player")
        {
            m_watersound.Play();
            m_overworld.volume = m_overworld.volume / 2;
            m_challenge.volume = m_challenge.volume / 2;
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        if(_other.tag == "Player")
        {
            m_watersound.Stop();
            m_overworld.volume = m_overworld.volume * 2;
            m_challenge.volume = m_challenge.volume * 2;
        }
    }
}
