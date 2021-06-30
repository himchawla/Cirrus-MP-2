using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class RisingPlatform : MonoBehaviour
{
    private Vector3 m_origin;

    private bool m_rise;

    [SerializeField] private float m_speed = 5f;
    [SerializeField] private GameObject m_camera;
    
    private void Start()
    {
        m_origin = transform.position;
    }

    public void rise()
    {
        m_rise = true;
    }

    private void Update()
    {
        if (m_rise)
        {
            if ((transform.position.y - m_origin.y) < 11f)
            {
                transform.Translate(0f, m_speed * Time.deltaTime, 0f);
            }
            else
            {
                m_camera.GetComponent<Animator>().SetBool("Shake", false);
            }
        }
    }
    
}
