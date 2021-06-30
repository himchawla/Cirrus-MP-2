using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class WanderAI : MonoBehaviour
{
    
    
    [FormerlySerializedAs("radius")] public float m_radius;
    private Vector3 m_origin;
    [FormerlySerializedAs("maxTimer")] public float m_maxTimer;

    Vector3 m_vel = Vector3.zero;
    Vector3 m_movePos = Vector3.zero;

    private float m_timer;

    private void Start()
    {
    }
    private void OnEnable()
    {
        m_origin = transform.localPosition;
        GetComponent<Rigidbody>().velocity = Vector3.zero;

    }
    private void OnDisable()
    {
        //GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        m_timer -= Time.deltaTime;
        //transform.Translate(vel * Time.deltaTime);
        Vector3 velNor = m_vel.normalized;

        transform.rotation = Quaternion.LookRotation(GetComponent<Rigidbody>().velocity);

        if ((m_movePos - transform.localPosition).x < 0.1f && (m_movePos - transform.localPosition).z < 0.1f)
        {
            m_timer = 0.0f;
        }
        if (m_timer <= 0.0f)
        {
            m_movePos = RandomPositionInCircle(m_origin, 4);
            GetComponent<Rigidbody>().velocity = (m_movePos - transform.localPosition).normalized * 2.0f;
            //transform.rotation = Quaternion.Euler(0f, Mathf.Atan2(vel.x, vel.z) * Mathf.Rad2Deg, 0f);

            m_timer = m_maxTimer;
        }
    }

    Vector3 RandomPositionInCircle(Vector3 _origin, float _radius) 
    {
        Vector3 randPos = Random.insideUnitSphere * _radius;
        randPos.y = _origin.y;
        randPos += _origin;

        return randPos;
    }

    private void OnTriggerEnter(Collider _other)
    {
        if(_other.tag == "Player")
        {
           
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        if (_other.tag == "Player")
        {
            //agent.enabled = true;
        }
    }
}
