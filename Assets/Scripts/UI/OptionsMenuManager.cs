using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenuManager : MonoBehaviour
{
    private static OptionsMenuManager m_instance { get; set; }
    
    private Scene m_currentScene;

    public static OptionsMenuManager Instance
    {
        get { return m_instance; }
    }

    private void Awake()
    {
        if (m_instance != null && m_instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            m_instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        m_currentScene = SceneManager.GetActiveScene();
    }
}
