using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.VFX;

public class Collectable : MonoBehaviour
{
    // Start is called before the first frame update
    [FormerlySerializedAs("UIImage")] public CollectableCount m_uiImage;
    [FormerlySerializedAs("collectSound")] public AudioSource m_collectSound;
    bool m_disappear = false;
    float m_timer = 0.0f;

    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        m_timer -= Time.deltaTime;
        if(m_disappear && m_timer < 0.0f)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.CompareTag("Player"))
        {
            m_uiImage.ONCollection();
            m_collectSound.Play();
            transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Play();
            transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Stop();
            m_disappear = true;
            m_timer = 1.0f;
            gameObject.GetComponent<SphereCollider>().enabled = false;
        }
    }
}
