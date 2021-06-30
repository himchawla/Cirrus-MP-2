using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lillypad : MonoBehaviour
{
    private Vector3 startPos;
    private bool m_playerOn = false;
    private Rigidbody m_rigidbody;
    [SerializeField] private AudioSource m_squish;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        m_rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_playerOn)
        {
            m_rigidbody.MovePosition(transform.position + (Physics.gravity.normalized) * Time.fixedDeltaTime);
        } else
        {
            if (transform.position != startPos)
            {
                m_rigidbody.MovePosition(transform.position + (-Physics.gravity.normalized) * Time.fixedDeltaTime);
            }
        }

        transform.position = new Vector3(transform.position.x,
           Mathf.Clamp(transform.position.y, startPos.y - 10.0f, startPos.y),
           transform.position.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            m_playerOn = true;
            if (!m_squish.isPlaying)
            {
                m_squish.Play();
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            m_playerOn = false;
        }
    }
}
