using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.AI;

public class CameraController : MonoBehaviour
{

    [SerializeField] private CinemachineFreeLook m_freeLook;

    [Header("Cutscene 1")]
    [SerializeField] private GameObject m_collectibleCam;
    [SerializeField] private GameObject m_recieverCam;

    [Header("Cutscene 2")]
    [SerializeField] private GameObject m_doorCam;
    [SerializeField] private GameObject m_recieverNearCam;
    [SerializeField] private Transform m_depositLocation;
    [SerializeField] private Animator m_pickup;
    [SerializeField] private Animator m_doorL;
    [SerializeField] private Animator m_doorR;

    [Header("Cutscene 3")]
    [SerializeField] private GameObject m_recieverCamIsland2;
    [SerializeField] private GameObject m_collectibleCamIsland2;
    [SerializeField] private GameObject m_levelCamIsland2;
    
    [Header("Cutscene 4")]
    [SerializeField] private GameObject m_doorCamIsland2;
    [SerializeField] private GameObject m_recieverNearCamIsland2;
    [SerializeField] private Transform m_depositLocationIsland2;
    [SerializeField] private Animator m_pickupIsland2;
    
    [Header("Cutscene 5")]
    [SerializeField] private GameObject m_recieverNearCamIsland3;
    [SerializeField] private GameObject m_StaircaseCam;
    [SerializeField] private Transform m_depositLocationIsland3;
    [SerializeField] private Animator m_pickupIsland3;
    [SerializeField] private RisingPlatform m_staircase;
    
    [SerializeField] private playerMovement m_playerMovement;
    


    private IEnumerator eCutscene1()
    {
        m_collectibleCam.SetActive(true);
        yield return new WaitForSeconds(4.5f);
        m_collectibleCam.SetActive(false);
        m_recieverCam.SetActive(true);
        yield return new WaitForSeconds(4.5f);
        m_recieverCam.SetActive(false);
        if (m_playerMovement != null)
            m_playerMovement.m_cutscenePlayin = false;
    }

    private IEnumerator eCutscene3()
    {

        
        m_levelCamIsland2.SetActive(true);
        yield return new WaitForSeconds(4.5f);
        m_levelCamIsland2.SetActive(false);
        m_collectibleCamIsland2.SetActive(true);
        yield return new WaitForSeconds(4.5f);
        m_collectibleCamIsland2.SetActive(false);
        m_recieverCamIsland2.SetActive(true);
        yield return new WaitForSeconds(4.5f);
        m_recieverCamIsland2.SetActive(false);
        if (m_playerMovement != null)
            m_playerMovement.m_cutscenePlayin = false; 
    }
    
    

    public void Cutscene3()
    {
        if (m_playerMovement != null)
            m_playerMovement.m_cutscenePlayin = true;
        else
        {
            Debug.Log("Player not assigned in script CameraController.cs");
        }

        StartCoroutine(eCutscene3());
    }


    public void Cutscene1()
    {
        if (m_playerMovement != null)
            m_playerMovement.m_cutscenePlayin = true;
        else
        {
            Debug.Log("Player not assigned in script CameraController.cs");
        }

        StartCoroutine(eCutscene1());

    }

    public void Cutscene2()
    {
        if (m_playerMovement != null)
            m_playerMovement.m_cutscenePlayin = true;
        else
        {
            Debug.Log("Player not assigned in script CameraController.cs");
        }

        StartCoroutine(eCutscene2());
    }

    bool hasPath()
    {
        var agent = m_playerMovement.gameObject.GetComponent<NavMeshAgent>();

        if (agent.hasPath)
            return false;
        else return true;
    }

    private IEnumerator eCutscene2()
    {
        m_recieverNearCam.SetActive(true);
        var agent = m_playerMovement.gameObject.GetComponent<NavMeshAgent>();
        yield return new WaitForSeconds(2f);
        agent.enabled = true;
        agent.SetDestination(m_depositLocation.position);

        m_playerMovement.LookOverride = m_depositLocation;
        yield return new WaitUntil(()=>!agent.enabled);
        m_pickup.SetTrigger("Execute");
        m_pickup.tag = "Untagged";
        //m_playerMovement.enabled = false;
        agent.transform.rotation = m_depositLocation.rotation;
        yield return new WaitForSeconds(3f);

        m_recieverNearCam.SetActive(false);
        //agent.enabled = false;
        m_doorCam.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        m_doorL.SetTrigger("Open");
        m_doorR.SetTrigger("Open");
        yield return new WaitForSeconds(2.0f);

        m_doorCam.SetActive(false);
        m_playerMovement.enabled = true;

        if (m_playerMovement != null)
            m_playerMovement.m_cutscenePlayin = false;
    }
    
    
    private IEnumerator eCutscene4()
    {
        m_recieverNearCamIsland2.SetActive(true);
        var agent = m_playerMovement.gameObject.GetComponent<NavMeshAgent>();
        yield return new WaitForSeconds(2f);
        agent.enabled = true;
        agent.SetDestination(m_depositLocationIsland2.position);
        yield return new WaitForSeconds(5f);
        m_pickupIsland2.SetTrigger("Execute2");
        m_pickupIsland2.tag = "Untagged";
        
        agent.transform.rotation = Quaternion.Lerp(agent.transform.rotation, m_depositLocationIsland2.rotation, 10);
        yield return new WaitForSeconds(3f);

        m_recieverNearCamIsland2.SetActive(false);
        agent.enabled = false;
        m_doorCamIsland2.SetActive(true);
        yield return new WaitForSeconds(4.0f);
        m_doorCamIsland2.SetActive(false);

        if (m_playerMovement != null)
            m_playerMovement.m_cutscenePlayin = false;
    }
    
    private IEnumerator eCutscene5()
    {
        m_recieverNearCamIsland3.SetActive(true);
        var agent = m_playerMovement.gameObject.GetComponent<NavMeshAgent>();
        yield return new WaitForSeconds(2f);
        agent.enabled = true;
        agent.SetDestination(m_depositLocationIsland3.position);
        yield return new WaitForSeconds(5f);
        m_pickupIsland3.SetTrigger("Execute3");
        m_pickupIsland3.tag = "Untagged";
                
        agent.transform.rotation = Quaternion.Lerp(agent.transform.rotation, m_depositLocationIsland3.rotation, 10);
        yield return new WaitForSeconds(3f);

        m_recieverNearCamIsland3.SetActive(false);
        agent.enabled = false;
        m_StaircaseCam.SetActive(true);

        yield return new WaitForSeconds(2f);
        
        m_staircase.rise();
        m_StaircaseCam.GetComponent<Animator>().SetBool("Shake", true);

        //Add the VFX here
        yield return new WaitForSeconds(4.0f);

        m_StaircaseCam.SetActive(false);

        if (m_playerMovement != null)
            m_playerMovement.m_cutscenePlayin = false;
    }

    public void Cutscene4()
    {
        if (m_playerMovement != null)
            m_playerMovement.m_cutscenePlayin = true;
        else
        {
            Debug.Log("Player not assigned in script CameraController.cs");
        }

        StartCoroutine(eCutscene4());
    }
    
    public void Cutscene5()
    {
        if (m_playerMovement != null)
            m_playerMovement.m_cutscenePlayin = true;
        else
        {
            Debug.Log("Player not assigned in script CameraController.cs");
        }

        StartCoroutine(eCutscene5());
    }
    
    private void enable()
    {
        m_freeLook.m_YAxisRecentering.m_enabled = true;
    }
    
    public void SetfreeCam()
    {
        m_freeLook.m_YAxis.Value = 0f;
        m_freeLook.m_YAxisRecentering.m_enabled = false;
        Invoke("enable", 2);
    }
    
}
