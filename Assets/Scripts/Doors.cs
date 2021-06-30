using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{

    [SerializeField] private AudioSource m_DoorOpen;

    public void OpenDoor(int _delay)
    {
       Invoke("Open", _delay);
    }

    private void Open()
    {
        if(m_DoorOpen != null)
        {
            m_DoorOpen.Play();
        }
        GetComponent<Animator>().SetTrigger("Open");
    }



   
}
