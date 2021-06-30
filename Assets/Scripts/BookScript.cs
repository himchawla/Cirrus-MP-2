using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

public class BookScript : MonoBehaviour
{
   [SerializeField] private UnityEvent m_event;
   private bool m_shrink;
   private Material m_material;

   private void Start()
   {
   }

   private void OnTriggerEnter(Collider _collider)
   {
      if (_collider.CompareTag("Player"))
      {
         m_event?.Invoke();
         m_shrink = true;
      }
   }

   private void Update()
   {
      if (m_shrink)
      {
         if (transform.GetChild(0).GetComponent<Renderer>().material.color.a > 0)
         {
            foreach (var material in transform.GetChild(0).GetComponent<Renderer>().materials)
            {
             
               material.color = new Color(material.color.r,
                  material.color.g, material.color.b,
                  material.color.a - Time.deltaTime);  
            }

            if(transform.localScale.x > 0)
               transform.localScale -= new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime);
         }
         else Destroy(this.gameObject);
      }
   }
}

