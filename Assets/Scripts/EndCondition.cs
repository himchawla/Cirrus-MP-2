using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class EndCondition : MonoBehaviour
{
    // Start is called before the first frame update
    [FormerlySerializedAs("cc")] public CollectionCheck m_cc;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider _other)
    {
        if(_other.tag == "Player")
        {
            if(m_cc.m_bFinished == true)
            {
                SceneManager.LoadScene(2);
            }
        }
    }
}
