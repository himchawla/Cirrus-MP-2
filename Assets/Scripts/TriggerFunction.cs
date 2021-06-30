using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class TriggerFunction : MonoBehaviour
{
    [SerializeField] private UnityEvent m_triggerEnter;

    [SerializeField] private string m_enterTag;
    [SerializeField] private UnityEvent m_triggerEnterTagged;

    [SerializeField] private UnityEvent m_triggerExit;

    [SerializeField] private string m_exitTag;
    [SerializeField] private UnityEvent m_triggerExitTagged;


    [SerializeField] private bool m_onceAndDestroy;

    private void Start()
    {
        if (m_onceAndDestroy)
            m_triggerEnter.AddListener(Destroy);
    }

    void Destroy()
    {
        Destroy(this);
    }

    private void OnTriggerEnter(Collider _collider)
    {
        m_triggerEnter?.Invoke();
        if (m_enterTag != "")
        {
            if (_collider.CompareTag(m_enterTag))
            {
                m_triggerEnterTagged?.Invoke();
            }
        }
    }

    private void OnTriggerExit(Collider _collider)
    {
        m_triggerExit?.Invoke();
        if (m_exitTag != "")
        {
            if (_collider.CompareTag(m_exitTag))
            {
                m_triggerExitTagged?.Invoke();
            }
        }
    }
    
    
}
