using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

[System.Serializable]
public class ListWrapper
{
    public List<string> myList;
    public GameObject m_camera;
}
public class Dialogue : MonoBehaviour
{
    //private int textIterator = 0;
    // Start is called before the first frame update
    [SerializeField] private TMP_Text m_textBox;

    [SerializeField] private playerMovement m_player;

    [SerializeField] private List<ListWrapper> m_dialogues = new List<ListWrapper>();
    private bool m_canProceed = true;
    //private bool m_moveTextForward;

    public bool m_playing
    {
        get
        {
            return m_player.m_cutscenePlayin;
        }
        set
        {
            m_player.m_cutscenePlayin = value;
           // m_playing = value;
        }
    }


    public int m_pointer { get; set; }
    
    public int m_defaultWaitTime { get; set; }
    public int m_defaultDelayTime { get; set; }

    public void SetString(int _count)
    {
        m_textBox.transform.parent.gameObject.SetActive(true);
        if(m_dialogues[_count].m_camera!=null)
            m_dialogues[_count].m_camera.SetActive(true);
        m_pointer = 0;
        {
            StartCoroutine(eRunDialogue(_count));
        }
    }
    
    
    
    public void Increment(int _count)
    {
        m_pointer++;
        StartCoroutine(eSetString(m_defaultWaitTime, m_defaultDelayTime, m_dialogues[_count].myList[m_pointer]));
    }
    
    public void SetString(string _text)
    {
        
            int count = -1;
            int index = 0;
            int i = 0;
            foreach (var dialouge in m_dialogues)
            {
                for (i = 0; i < dialouge.myList.Count; i++)
                {
                    if (dialouge.myList[i] == _text)
                    {
                        count = index;
                        break;
                    }
                }
    
                index++;
            }
    
            if (count == -1)
            {
                throw new ArgumentException(
                    "This dialogue is not found in the Dialogue List, enable force mode to add this diaogue anyway");
            }
            StartCoroutine(eSetString(m_defaultWaitTime, m_defaultDelayTime, m_dialogues[count].myList[i]));
            
        
    }

    public void ForceString(string _text)
    {
        StartCoroutine(eSetString(m_defaultWaitTime, m_defaultDelayTime, _text));
    }

    private IEnumerator eRunDialogue(int _count)
    {
        while (m_pointer < m_dialogues[_count].myList.Count)    // If pointer is not at the end
        {
            if (m_canProceed)   
            {
                StartCoroutine(eSetString(m_defaultWaitTime, m_defaultDelayTime,
                    m_dialogues[_count].myList[m_pointer]));

                //m_canProceed = false;
                m_playing = true;
                yield return null;
            }
        }

        if (m_pointer == m_dialogues[_count].myList.Count)  //If pointer is at the end, remove the dialgue after default wait time(3 seconds).
        {
            m_playing = false;
            StartCoroutine(eSetString(m_defaultWaitTime, m_defaultWaitTime, ""));
            if(m_dialogues[_count].m_camera!= null)
                m_dialogues[_count].m_camera.SetActive(false);
            m_textBox.transform.parent.gameObject.SetActive(false);
        }

        //  StartCoroutine(eSetString(m_defaultWaitTime, m_defaultDelayTime, ""));

    }

    private IEnumerator eSetString(int _time, int _delay, string _text)
    {
        string word = "";
        
   
        m_textBox.SetText(_text);
        m_canProceed = true;

        yield return new WaitForSeconds(_time);

        // yield return new WaitForSeconds(_time);
        //m_textBox.SetText("");
    }
    
    void Start()
    {
        m_defaultDelayTime = 0;
        m_defaultWaitTime = 3;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_playing)
        {
            if (Input.GetButtonDown("Jump"))
            {
                m_pointer++;
            }
        }
    }
}
