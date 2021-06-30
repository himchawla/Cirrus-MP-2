using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Vel : MonoBehaviour
{

    [FormerlySerializedAs("force")] [SerializeField]
    Vector3 m_force;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().velocity += m_force;
    }
}
