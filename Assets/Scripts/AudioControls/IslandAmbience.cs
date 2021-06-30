using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandAmbience : MonoBehaviour
{
    [SerializeField] private AudioSource m_ambientWind;
    [SerializeField] private AudioSource m_ambientBird;
    [SerializeField] private AudioSource[] m_ambientBugs;

    float birdTimer = 13.0f;
    float bugTimer = 5.0f;

    //Start the ambient as you enter
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            m_ambientWind.Play();
        }
    }

    //Play the ambient sounds as the player stays in the area
    private void OnTriggerStay(Collider other)
    {
        //Bugs like the inbetween wind
        //Do it last since it has a *return* statement
        if (other.tag == "Player")
        {
            birdTimer -= Time.deltaTime;
            bugTimer -= Time.deltaTime;

            if (birdTimer < 0)
            {
                m_ambientBird.Play();
                //Make it feel more natural
                birdTimer = Random.Range(10.0f, 25.0f);
            }

            if (bugTimer < 0)
            {
                int noise = Random.Range(0, m_ambientBugs.Length - 1);

                bugTimer = Random.Range(5.0f, 10.0f);

                m_ambientBugs[noise].Play();
            }
        }
    }

    //Turn em all off when you leave
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //Randomly play a ambience noise
            foreach (AudioSource a in m_ambientBugs)
            {
                a.Stop();
            }
        }
    }
}
