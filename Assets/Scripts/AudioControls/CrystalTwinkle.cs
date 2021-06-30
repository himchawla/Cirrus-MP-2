using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalTwinkle : MonoBehaviour
{
    [SerializeField] private AudioSource m_twinkle;

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (!m_twinkle.isPlaying)
            {
                m_twinkle.Play();
            }
        }
    }
}
