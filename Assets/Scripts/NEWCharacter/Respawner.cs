using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Respawner : MonoBehaviour
{

    [SerializeField] private Transform[] m_spawnLocations;
    [SerializeField] private AudioSource m_respawnSound;

    private playerMovement m_player;
    public int m_currentSpawnLocation { get; set; }
    
    void Start()
    {
        m_player = GetComponent<playerMovement>();
        //m_controller = GetComponent<CharacterController>();
        m_currentSpawnLocation = 0;
    }

    void Update()
    {
      
    }

    private void LateUpdate()
    {
        //if (!m_controller.enabled)
        {
           // m_controller.enabled = true;
        }
    }

    public void ToSpawnLoc(int _delay)
    {
        //m_controller.enabled = false;
        
        m_player.m_canMove = false;
        Invoke("spawnInvokation", _delay);
    }

    public void SetSpawnLocation(int _loc)
    {
        m_currentSpawnLocation = _loc;
    }

    private void spawnInvokation()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.position = m_spawnLocations[m_currentSpawnLocation].position;
        m_player.m_canMove = true;
        GetComponent<PickUpItem>().DisablePickup();
        m_respawnSound.Play();
    }
}
