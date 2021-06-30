using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GoalRing : MonoBehaviour
{
    [FormerlySerializedAs("collectable")] public Collectable m_collectable;
    [FormerlySerializedAs("Rings")] public Ring[] m_rings;
    int m_rCount = 0;

    // Start is called before the first frame update

    private void OnEnable()
    {
        m_collectable.gameObject.SetActive(false);
        
    }
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
            foreach(Ring r in m_rings)
            {
                if(r.m_ringCheck == true)
                {
                    m_rCount++;
                }
            }

            if(m_rCount == m_rings.Length)
            {
                m_collectable.gameObject.SetActive(true);
                GameObject.FindGameObjectWithTag("Switch2").GetComponent<PuzzleSpawner>().m_timer = 0.0f;
            }
        }
    }

    public void Reset()
    {
        foreach (Ring r in m_rings)
        {
            r.Reset();
        }

        m_rCount = 0;
    }
}
