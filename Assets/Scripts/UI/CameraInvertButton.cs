using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class CameraInvertButton : MonoBehaviour
{
    public CinemachineFreeLook cam;

    public TMPro.TextMeshProUGUI text;

    private void Start()
    {
        if (cam.m_YAxis.m_InvertInput)
        {
            text.text = "Camera Y: Inverted";
        }
        else
        {
            text.text = "Camera Y: Normal";
        }
    }

    public void onClick()
    {
        cam.m_YAxis.m_InvertInput = !cam.m_YAxis.m_InvertInput;

        if (cam.m_YAxis.m_InvertInput)
        {
            text.text = "Camera Y: Inverted";
        } else
        {
            text.text = "Camera Y: Normal";
        }
    }
}
