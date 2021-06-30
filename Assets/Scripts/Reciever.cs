using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Reciever : MonoBehaviour
{

    public UnityEvent m_stuff;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider _collider)
    {
        if (_collider.CompareTag("PickUp"))
        {
            m_stuff?.Invoke();
            Destroy(this.gameObject);
        }
}
}
