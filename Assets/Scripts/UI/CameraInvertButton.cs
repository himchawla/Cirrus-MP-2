using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class CameraInvertButton : MonoBehaviour
{
    public CinemachineFreeLook cam;

    [SerializeField] private Sprite m_invert;
    [SerializeField] private Sprite m_normal;

    private void Start()
    {
        if (cam.m_YAxis.m_InvertInput)
        {
            GetComponent<Image>().sprite = m_invert;
        }
        else
        {
            GetComponent<Image>().sprite = m_normal;
        }
    }

    void Update()
    {
        if (cam.m_YAxis.m_InvertInput)
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
        cam.m_YAxis.m_InvertInput = !cam.m_YAxis.m_InvertInput;

        if (cam.m_YAxis.m_InvertInput)
        {
            GetComponent<Image>().sprite = m_invert;
        } else
        {
            GetComponent<Image>().sprite = m_normal;
        }
    }
}
