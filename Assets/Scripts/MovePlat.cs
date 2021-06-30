using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class MovePlat : MonoBehaviour
{
    //The Distance the end points are
    [FormerlySerializedAs("DisplacementPos")] public Vector3 m_displacementPos;
    [FormerlySerializedAs("DisplacementNeg")] public Vector3 m_displacementNeg;
    //The Speed you arrive at a end point
    public Vector3 m_vectorSpeed;

    private Vector3 m_origin;
    //The Destination it checks
    private Vector3 m_destinationMax;
    private Vector3 m_destinationMin;

    [Header("Random")]
    [SerializeField] private Vector3 m_isRandom;
    
    [SerializeField] private Vector2 m_rangeX;
    [SerializeField] private Vector2 m_rangeY;
    [SerializeField] private Vector2 m_rangeZ;
    private Rigidbody m_rigidBody;

    //Prevents it from getting stuck
    bool reachmax = false;
    bool reachmin = false;

    // Start is called before the first frame update
    void Start()
    {
        if (m_isRandom.x == 1)
        {
            m_displacementNeg.x = Random.Range(m_rangeX.x, 0f);
            m_displacementPos.x = Random.Range(0f, m_rangeX.y);
        }
        
        if (m_isRandom.y == 1)
        {
            m_displacementNeg.y = Random.Range(m_rangeY.x, 0f);
            m_displacementPos.y = Random.Range(0f, m_rangeY.y);
        }
        
        if (m_isRandom.z == 1)
        {
            m_displacementNeg.z = Random.Range(m_rangeZ.x, 0f);
            m_displacementPos.z = Random.Range(0f, m_rangeZ.y);
        }
        m_rigidBody = GetComponent<Rigidbody>();
        m_origin = transform.position;
        m_destinationMax = transform.position + m_displacementPos;
        m_destinationMin = transform.position + m_displacementNeg;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        //Move towards location
        m_rigidBody.MovePosition(transform.position + m_vectorSpeed * Time.fixedDeltaTime);
        //Clamp between for correct calculation
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, m_destinationMin.x, m_destinationMax.x),
            Mathf.Clamp(transform.position.y, m_destinationMin.y, m_destinationMax.y),
            Mathf.Clamp(transform.position.z, m_destinationMin.z, m_destinationMax.z));
        //check if reached
        DestinationReach();
    }

    public void ResetPosition()
    { 
        Invoke("resetPos", 3);
    }

    private void resetPos()
    {
        transform.position = m_origin;
    }
    //Check if it reached its destination
    void DestinationReach()
    {
        //Without the bool itd get stuck at a destination and have its speed reset over and over

        if(transform.position.x == m_destinationMax.x && transform.position.y == m_destinationMax.y && transform.position.z == m_destinationMax.z && !reachmax)
        {
            m_vectorSpeed = -m_vectorSpeed;
            reachmax = true;
            reachmin = false;
        }

        if (transform.position.x == m_destinationMin.x && transform.position.y == m_destinationMin.y && transform.position.z == m_destinationMin.z && !reachmin)
        {
            m_vectorSpeed = -m_vectorSpeed;
            reachmin = true;
            reachmax = false;
        }
    }


    public void ToFinalPos()
    {
        transform.position = new Vector3(-7.666f, -5.651f, 85.005f);
        transform.rotation = Quaternion.Euler(180f, 0f, 180f);
    }
}
