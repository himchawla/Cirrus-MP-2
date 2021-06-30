using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionPasser : MonoBehaviour
{
    bool m_Invert = false;

    public CamInvertMainMenu m_setter;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("ah");
        if (m_setter != null)
        {
            m_Invert = m_setter.m_Inverted;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_setter != null)
        {
            m_Invert = m_setter.m_Inverted;
        }

        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            Cinemachine.CinemachineFreeLook cam;
            cam = GameObject.FindGameObjectWithTag("playerCam").GetComponent<Cinemachine.CinemachineFreeLook>();
            cam.m_YAxis.m_InvertInput = m_Invert;
            Destroy(this.gameObject);
        }
    }
}
