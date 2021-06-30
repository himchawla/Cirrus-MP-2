using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBackground : MonoBehaviour
{
    [SerializeField] private AudioSource opening;
    [SerializeField] private AudioSource island1Loop;
    [SerializeField] private AudioSource island2Loop;

    // Update is called once per frame
    void Update()
    {
        //There is 100% a better way but bear with me
        if(!opening.isPlaying && !island2Loop.isPlaying && !island1Loop.isPlaying)
        {
            island1Loop.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("yes");
            island1Loop.Stop();
            opening.Stop();
            island2Loop.Play();
        }
    }
}
