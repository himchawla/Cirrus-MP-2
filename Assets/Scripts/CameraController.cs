using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

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



    [Header("Cutscene 6")] 
    [SerializeField]private GameObject m_recieverFinal;
    [SerializeField]private Transform m_depositLocationFinal1;
    [SerializeField]private Transform m_depositLocationFinal2;
    [SerializeField] private Transform m_depositLocationFinalOrigin;
    [SerializeField] private Animator m_book1;
    [SerializeField] private Animator m_book2;
    [SerializeField] private GameObject m_recieverCamFinal1;
    [SerializeField] private GameObject m_recieverCamFinal2;
    
    [SerializeField] private playerMovement m_playerMovement;
    
    [SerializeField] private Dialogue m_dialogueBox;
    [SerializeField] private RenderTexture m_renderTexture;

    [SerializeField] private AudioSource m_Door;

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
        m_Door.Play();
        m_doorL.SetTrigger("Open");
        m_doorR.SetTrigger("Open");
        yield return new WaitForSeconds(2.0f);

        m_doorCam.SetActive(false);
        m_playerMovement.enabled = true;

        if (m_playerMovement != null)
            m_playerMovement.m_cutscenePlayin = false;
    }
    
    private IEnumerator eCutscene6()
    {
        m_recieverFinal.SetActive(true);
        m_playerMovement.gameObject.GetComponent<AnimationHandler>().m_weightJump = 0f;
        var agent = m_playerMovement.gameObject.GetComponent<NavMeshAgent>();
        yield return new WaitForSeconds(2f);

        agent.enabled = true;
        agent.SetDestination(m_depositLocationFinalOrigin.position);

        m_playerMovement.LookOverride = null;
        yield return new WaitUntil(()=>!agent.enabled);

        m_playerMovement.transform.rotation = m_depositLocationFinalOrigin.rotation;

        m_recieverFinal.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 3.39f; 
        m_dialogueBox.ForceString("WHO DARES INTRUDE UPON MY LANDS! PREPARE TO BE SMITE- ");
        yield return new WaitForSeconds(4);
        m_recieverFinal.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f; 

        
        agent.enabled = true;
        agent.SetDestination(m_depositLocationFinal1.position);


        
        m_playerMovement.LookOverride = null;
        yield return new WaitUntil(()=>!agent.enabled);
        m_playerMovement.transform.rotation = m_depositLocationFinal1.rotation;

        
        
        m_recieverCamFinal1.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        m_playerMovement.gameObject.GetComponent<PickUpItem>().m_incerasing = true;
        m_book1.SetTrigger("Execute");
        yield return new WaitForSeconds(2.5f);
        m_playerMovement.gameObject.GetComponent<PickUpItem>().m_decreasing = true;
        m_recieverCamFinal1.SetActive(false);
        
        
        
        agent.enabled = true;
        agent.SetDestination(m_depositLocationFinalOrigin.position);

        m_playerMovement.LookOverride = null;
        yield return new WaitUntil(()=>!agent.enabled);

        m_playerMovement.transform.rotation = m_depositLocationFinalOrigin.rotation;
        
        
       
        
        agent.enabled = true;
        agent.SetDestination(m_depositLocationFinal2.position);

        m_playerMovement.LookOverride = null;
        yield return new WaitUntil(()=>!agent.enabled);

        m_playerMovement.transform.rotation = m_depositLocationFinal2.rotation;
        
       

        m_recieverCamFinal2.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        m_playerMovement.gameObject.GetComponent<PickUpItem>().m_incerasing = true;
        m_book2.SetTrigger("Execute");
        yield return new WaitForSeconds(2.5f);
        m_playerMovement.gameObject.GetComponent<PickUpItem>().m_decreasing = true;

        m_recieverCamFinal2.SetActive(false);
        
        agent.enabled = true;
        agent.SetDestination(m_depositLocationFinalOrigin.position);

        m_playerMovement.LookOverride = null;
        yield return new WaitUntil(()=>!agent.enabled);

        m_playerMovement.transform.rotation = m_depositLocationFinalOrigin.rotation;
        yield return new WaitForEndOfFrame();
        
        m_dialogueBox.ForceString("I-is that volumes 5 and 6 of the tales of Fishy the Magic Koi Fish?");
        yield return new WaitForSeconds(4);
        
        m_dialogueBox.ForceString("I've been looking everywhere for those! They're the last I needed for my collection!");
        yield return new WaitForSeconds(4);
        
        m_dialogueBox.ForceString("You made me happy!!");
        yield return new WaitForSeconds(4);

        m_recieverFinal.SetActive(false);
        if (m_playerMovement != null)
            m_playerMovement.m_cutscenePlayin = false;

        Camera.main.targetTexture = m_renderTexture;

        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
    }

    public void Cutscene6() 
    {
        if (m_playerMovement != null)
            m_playerMovement.m_cutscenePlayin = true;
        else
        {
            Debug.Log("Player not assigned in script CameraController.cs");
        }

        StartCoroutine(eCutscene6());
        
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
        m_Door.Play();
        m_staircase.rise();
        m_StaircaseCam.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 3.39f; 

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
