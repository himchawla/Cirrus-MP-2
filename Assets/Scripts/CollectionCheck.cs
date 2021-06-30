using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class CollectionCheck : MonoBehaviour
{
    [FormerlySerializedAs("counters")] public CollectableCount[] m_counters;
    [FormerlySerializedAs("bFinished")] public bool m_bFinished = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int collection = 0;
        foreach(CollectableCount counter in m_counters)
        {
            if(counter.m_counted == true)
            {
                collection++;
            }
        }

        if(collection == 5)
        {
            m_bFinished = true;
        }
    }
}
