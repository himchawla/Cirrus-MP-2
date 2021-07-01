using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Guardian : MonoBehaviour
{
    public bool m_yes { get; set; }
    [FormerlySerializedAs("m_event")] [SerializeField] private UnityEvent m_eventBefore;
    [FormerlySerializedAs("m_event")] [SerializeField] private UnityEvent m_eventAfter;

    private bool m_move = false;
    private GameObject m_cam;

    [SerializeField] private GameObject m_interact;
    private void Start()
    {
        m_yes = false;
    }

    private bool button()
    {
        if (Input.GetButton("Jump"))
            return false;
        else return true;
    }
    

    private void OnTriggerStay(Collider _collider)
    {
        if (_collider.CompareTag("Player") && Input.GetButtonDown("PickUp"))
        {
            m_interact.SetActive(false);
            if (!m_yes)
            {
                m_eventBefore?.Invoke();
            }
            else
                m_eventAfter?.Invoke();
        }
        else if (_collider.CompareTag("Player") && !_collider.gameObject.GetComponent<playerMovement>().m_cutscenePlayin)
        {
            if(!m_interact.activeSelf)
                m_interact.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(m_interact.activeSelf)
            m_interact.SetActive(false);

    }

    private void Update()
    {
        
    }
}
