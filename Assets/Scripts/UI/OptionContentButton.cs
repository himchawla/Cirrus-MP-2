using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionContentButton : MonoBehaviour
{
    public GameObject[] OptionContent;

    [SerializeField] public Image m_Pause;
    [SerializeField] public Image m_Play;

    public void Start()
    {
        m_Pause.enabled = false;
        m_Play.enabled = true;
    }

    public void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            onClick();
        }
    }

    public void onClick()
    {
        foreach(GameObject go in OptionContent)
        {
            go.SetActive(!go.activeSelf);
        }

        m_Pause.enabled = !m_Pause.enabled;
        m_Play.enabled = !m_Play.enabled;
    }
}
