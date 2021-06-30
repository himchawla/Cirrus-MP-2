using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Ring : MonoBehaviour
{
    [FormerlySerializedAs("ringCheck")] public bool m_ringCheck = false;
    [FormerlySerializedAs("blueRing")] [SerializeField]
    private Material m_blueRing;
    [FormerlySerializedAs("redRing")] [SerializeField]
    private Material m_redRing;
    // Start is called before the first frame update
    private void OnEnable()
    {
        m_ringCheck = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider _other)
    {
        if(_other.tag == "Player")
        {
            m_ringCheck = true;
            gameObject.GetComponent<MeshRenderer>().material = m_redRing;
        }
    }

    public void Reset()
    {
        m_ringCheck = false;
        gameObject.GetComponent<MeshRenderer>().material = m_blueRing;
    }
}
