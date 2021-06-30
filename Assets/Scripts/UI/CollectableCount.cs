using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CollectableCount : MonoBehaviour
{
    [FormerlySerializedAs("counted")] public bool m_counted = false;
    [FormerlySerializedAs("Unchecked")] [SerializeField]
    private Texture m_unchecked;
    [FormerlySerializedAs("Checked")] [SerializeField]
    private Texture m_checked;
    private RawImage m_uiImage;
    // Start is called before the first frame update
    void Start()
    {
        m_uiImage = GetComponent<RawImage>();
        m_uiImage.texture = m_unchecked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ONCollection()
    {
        m_uiImage.texture = m_checked;
        m_counted = true;
    }
}
