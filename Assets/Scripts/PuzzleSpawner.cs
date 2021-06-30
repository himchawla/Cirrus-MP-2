using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PuzzleSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject[] m_puzzleElements;
    [FormerlySerializedAs("OverWorld")] public AudioSource m_overWorld;
    [FormerlySerializedAs("Challenge")] public AudioSource m_challenge;
    [FormerlySerializedAs("puzzletag")] public string m_puzzletag;
    [FormerlySerializedAs("Timer")] public float m_timer = 20.0f;
    [FormerlySerializedAs("maxTimer")] public float m_maxTimer = 20.0f;
    private bool m_isTiming = false;
    private bool m_bActive = false;
    void Start()
    {
        m_puzzleElements = GameObject.FindGameObjectsWithTag(m_puzzletag);
        foreach (GameObject element in m_puzzleElements)
        {
            element.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(m_isTiming == true)
        {
            m_timer -= Time.deltaTime;
            if (m_timer <= 10.0f)
            {
                m_challenge.pitch += 0.5f / m_maxTimer * Time.deltaTime;
            }
        }

        if(m_timer <= 0.0f)
        {
            PuzzleEnd();
        }
    }

    private void OnCollisionEnter(Collision _collision)
    {
        if (_collision.gameObject.tag == "Player" && m_bActive == false)
        {
            PuzzleBegin();
            m_challenge.Play();
            m_overWorld.Stop();
        }
    }

    private void PuzzleBegin()
    {
        foreach (GameObject element in m_puzzleElements)
        {
            element.SetActive(true);
            m_isTiming = true;
            m_bActive = true;
        }
    }

    private void PuzzleEnd()
    {
        foreach (GameObject element in m_puzzleElements)
        {
            if(element.GetComponent<GoalRing>() != null)
            {
                element.GetComponent<GoalRing>().Reset();
            }
            element.SetActive(false);
            m_isTiming = false;
            m_timer = m_maxTimer;
            m_bActive = false;
            m_challenge.Stop();
            m_challenge.pitch = 1.0f;
            m_overWorld.Play();
        }
    }
}
