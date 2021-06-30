using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    [SerializeField] private GameObject m_player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisablePickup()
    {
        m_player.GetComponent<PickUpItem>().DisablePickupNoRes();
    }
}
