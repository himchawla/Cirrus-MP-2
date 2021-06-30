using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamInvertMainMenu : MonoBehaviour
{
    [SerializeField] private Sprite m_invert;
    [SerializeField] private Sprite m_normal;

    bool m_Inverted = false;

    private void Start()
    {
        if (m_Inverted)
        {
            GetComponent<Image>().sprite = m_invert;
        }
        else
        {
            GetComponent<Image>().sprite = m_normal;
        }
    }

    public void onClick()
    {
        m_Inverted = !m_Inverted;

        if (m_Inverted)
        {
            GetComponent<Image>().sprite = m_invert;
        }
        else
        {
            GetComponent<Image>().sprite = m_normal;
        }
    }
}
