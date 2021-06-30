using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PickUpItem : MonoBehaviour
{
    private GameObject m_pickUp;

    private AnimationHandler m_animationHandler;
    bool m_canPickUp = false;
    private bool m_pickepUp = false;
    private NavMeshAgent m_agent;
    
    private bool m_incerasing;
    private bool m_decreasing;

    private void Start()
    {
        m_animationHandler = GetComponent<AnimationHandler>();
        m_agent = GetComponent<NavMeshAgent>();
    }

    public void DisablePickup()
    {
        if (m_pickUp != null)
        {
            m_pickUp.transform.GetChild(1).gameObject.SetActive(false);     //Disable bubble
            m_decreasing = true;    //Disable Blend Gradually, done in Update
            m_pickepUp = false;
            m_pickUp.GetComponent<Rigidbody>().velocity = Vector3.zero;
            m_pickUp.transform.rotation = Quaternion.identity;
            m_pickUp.GetComponent<MovePlat>().ResetPosition();
            m_canPickUp = false;
            m_pickUp = null;
        }
    }

    public void DisablePickupNoRes()
    {
        if (m_pickUp!= null)
        {
            m_decreasing = true;
            m_pickepUp = false;
            m_pickUp.GetComponent<Rigidbody>().velocity = Vector3.zero;
            m_pickUp.transform.rotation = Quaternion.identity;
            m_pickUp.transform.GetChild(1).gameObject.SetActive(false);
            //m_pickUp.GetComponent<MovePlat>().ResetPosition();
            m_canPickUp = false;
            m_pickUp = null;
        }
    }
    
    void Update()
    {

        if (GetComponent<playerMovement>().m_cutscenePlayin) return;

        if (m_incerasing)
        {
            if (m_animationHandler.m_weightCast < 1f)
                m_animationHandler.m_weightCast += 2 * Time.deltaTime;
            else
            {
                m_animationHandler.m_weightCast = 1f;
                m_incerasing = false;
            }
        }
        
        if (m_decreasing)
        {
            if (m_animationHandler.m_weightCast > 0f)
                m_animationHandler.m_weightCast -= 2 * Time.deltaTime;
            else
            {
                m_animationHandler.m_weightCast = 0f;
                m_decreasing = false;
            }
        }
        if (Input.GetButtonDown("PickUp") && m_pickUp != null && m_pickepUp)
        {
            DisablePickup();
        }

        else if (Input.GetButtonDown("PickUp") && m_canPickUp == true && m_pickepUp ==false)
        {
            void picked()
            {
                m_pickepUp = true;
                m_incerasing = true;    //Gradually increase blend
                m_pickUp.transform.GetChild(1).gameObject.SetActive(true);  //enable Buble
            }
                m_pickUp.GetComponent<MovePlat>().enabled = false;
                m_canPickUp = false;
                picked();
        }
        if(m_pickUp != null)
        {
            m_pickUp.transform.Rotate(new Vector3(10 * Time.deltaTime, 0.0f, 0.0f));
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PickUp")
        {
            m_pickUp = other.gameObject;
            m_canPickUp = true;
           
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PickUp")
        {
            m_canPickUp = false;
            //m_pickUp.GetComponent<MovePlat>().enabled = true;
            //     
            //Invoke("other.gameObject.GetComponent<MovePlat>().ResetPosition()", 3);

        }
    }

    private void LateUpdate()
    {
        if(m_pickUp != null && m_pickepUp == true)
        {
            m_pickUp.transform.position = transform.position + (transform.forward * 2f) + (transform.up);
        }
        if(m_agent.enabled && m_pickUp != null)
        {
            m_pickUp.transform.position = transform.position + (transform.forward);
        }
    }
    
}
