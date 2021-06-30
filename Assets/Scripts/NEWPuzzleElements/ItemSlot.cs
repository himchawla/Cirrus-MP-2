using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ItemSlot : MonoBehaviour
{
    [FormerlySerializedAs("Connected Item")] public GameObject m_connectedItem;
    
    //So its door can check
    public bool m_isConnected;

    [SerializeField] private BoxCollider m_boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == m_connectedItem)
        {
            m_isConnected = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == m_connectedItem)
        {
            m_isConnected = false;
        }
    }

    //Not in use yet but Would be a nice touch
    void MoveItemToCenter()
    {
        Vector3 direction = new Vector3(m_boxCollider.bounds.center.x, m_connectedItem.transform.position.y, m_boxCollider.bounds.center.z);

        m_connectedItem.transform.LookAt(direction);
        m_connectedItem.transform.Translate(transform.forward * Time.deltaTime, Space.World);
    }
}
