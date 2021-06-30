using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FadingPlat : MonoBehaviour
{
    [FormerlySerializedAs("maxFallTimer")] [SerializeField]
    private float m_maxFallTimer = 2.0f;
    private bool m_fadestart = false;
    private bool m_unfadeStart = false;
    private bool m_faded = false;
    private float m_fadeTimer;
    private Vector3 m_baseScale;
    // Start is called before the first frame update
    void Start()
    {
        m_fadeTimer = m_maxFallTimer;
        m_baseScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_fadestart == true)
        { 
           if (m_faded == false)
           {
              Fade();
           }
        }
        
        if(m_unfadeStart == true)
        {
            m_fadeTimer -= Time.deltaTime;

            if (m_fadeTimer <= 0.0f)
            {
                UnFade();
            }
        }
    }

    private void OnCollisionEnter(Collision _collision)
    {
        m_fadestart = true;
        m_fadeTimer = m_maxFallTimer;
    }

    private void Fade()
    {
        transform.localScale = new Vector3(Mathf.Clamp(transform.localScale.x - 1 * Time.deltaTime, 0.001f, m_baseScale.x),
            transform.localScale.y,
            Mathf.Clamp(transform.localScale.z - 1 * Time.deltaTime, 0.001f, m_baseScale.z));

        if(transform.localScale.x == 0.001f && transform.localScale.z == 0.001f)
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            m_faded = true;
            m_fadestart = false;
            m_unfadeStart = true;
            m_fadeTimer = m_maxFallTimer;
        }
    }

    private void UnFade()
    {
        transform.localScale = new Vector3(Mathf.Clamp(transform.localScale.x + 10 * Time.deltaTime, 0.001f, m_baseScale.x),
            transform.localScale.y,
            Mathf.Clamp(transform.localScale.z + 10 * Time.deltaTime, 0.001f, m_baseScale.z));

        if (transform.localScale.x >= 0.001f && transform.localScale.z >= 0.001f)
        {
            gameObject.GetComponent<BoxCollider>().enabled = true;
        }

            if (transform.localScale.x == m_baseScale.x && transform.localScale.z == m_baseScale.z)
        {
            m_fadeTimer = m_maxFallTimer;
            m_unfadeStart = false;
            m_fadestart = false;
            m_faded = false;
        }
    }
}
