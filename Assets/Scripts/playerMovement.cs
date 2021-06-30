using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngineInternal;

public class playerMovement : MonoBehaviour
{

    float m_glideTimer = 0.0f;
    float m_smoothTime = 0.1f;
    float m_smoothVel;

    private NavMeshAgent m_agent;

    [FormerlySerializedAs("glide")] public bool m_glide;

    //Movement Based Variables
    [FormerlySerializedAs("speed")] public float m_speed = 12.0f;
    [FormerlySerializedAs("jumpHeight")] public float m_jumpHeight;


    public Transform LookOverride { get; set; }

    Vector3 m_velocity;

    [FormerlySerializedAs("groundCheck")] public Transform m_groundCheck;
    [FormerlySerializedAs("groundDistance")] public float m_groundDistance = 0.4f;
    [FormerlySerializedAs("groundMask")] public LayerMask m_groundMask;

    private Transform m_cam;

    [FormerlySerializedAs("glideFactor")] [Range(0f, 20.0f)]
    public float m_glideFactor = -1.8f;

    Rigidbody m_rb;

    bool m_isJumping;
    bool m_isGrounded;
    float m_defaultPos;
    bool m_canGlide;
    bool m_isGliding;
    //Interaction Based Variables

    Vector3 m_dir;
    Vector3 m_move;

    [SerializeField] bool unlockGlide;

    private AnimationHandler m_animationHandler;
    
    float m_jumpTimer;
    public bool m_canMove { get; set; }

    public bool m_cutscenePlayin { get; set; }

    private Vector3 m_movingPlat = Vector3.zero;

    private bool m_paused = false;

    [SerializeField] private AudioSource m_walking;
    [SerializeField] private AudioSource m_running;
    [SerializeField] private AudioSource m_jumpStart;
    [SerializeField] private AudioSource m_jumpLand;
    [SerializeField] private AudioSource m_Splash;
    private RaycastHit hitInfo;
    private float disG;
    private Animator m_animator;
    private bool m_increaseWeight;
    private bool m_decreaseWeight;

    // Start is called before the first frame update
    void Start()
    {
        m_animationHandler = GetComponent<AnimationHandler>();
        m_animator = transform.GetChild(2).GetComponent<Animator>();
        m_agent = GetComponent<NavMeshAgent>();
        m_canMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        m_cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
        m_rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //Pause stuff
        if (Input.GetButtonDown("Pause"))
        {
            if (m_paused)
            {
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                m_paused = false;
            } 
            else
            {
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                m_paused = true;
            }
        }

        if (m_cutscenePlayin || !m_canMove)
        {
            m_rb.velocity = Vector3.zero;
            //m_rb.AddForce(Physics.gravity, ForceMode.Acceleration);

            if (m_agent.enabled)
            {
                m_rb.velocity = Vector3.zero;
                m_rb.angularVelocity = Vector3.zero;
                if (m_agent.remainingDistance < 0.1f)
                {
                    m_agent.enabled = false;
                }
                m_animationHandler.m_speedAnimator =  m_agent.velocity.magnitude;
            }
            return;
        }

        if(m_increaseWeight)
            m_animationHandler.m_weightJump += 5 * Time.deltaTime;
        if (m_animator.GetLayerWeight(2) >= 1f)
            m_increaseWeight = false;
        
        if(m_decreaseWeight)
            m_animationHandler.m_weightJump -= 5 * Time.deltaTime;
        if (m_animator.GetLayerWeight(2) <= 0f)
            m_decreaseWeight = false;
        
        Physics.Raycast(transform.position, -Vector3.up, out hitInfo, 100f, m_groundMask);

        disG = hitInfo.distance;
        //Jump Force
        if (Input.GetButtonDown("Jump") && m_isGrounded)
        {

            JumpInvokation();


            m_increaseWeight = true;
            
            m_jumpTimer = 0.2f;


        }

        if (unlockGlide)
        {
            //Check if we can glide
            // if (Input.GetButtonUp("Jump"))
            // {
            //     m_canGlide = true;
            //     m_glideTimer = m_glideFactor;
            // }
            //
            // //Gliding
            // if (Input.GetButtonDown("Jump") && m_canGlide)
            // {
            //     m_rb.velocity = Vector3.zero;
            //     m_isGliding = true;
            // }
            // if (!m_isGrounded && Input.GetButtonUp("Jump"))
            // {
            //     m_isGliding = false;
            // }
        }
    }

    private void JumpInvokation()
    {
        m_animator.SetBool("isJumping", true);
        m_isJumping = true;
        m_rb.AddForce(Vector3.up * Mathf.Sqrt(m_jumpHeight * -2f * Physics.gravity.y), ForceMode.Impulse);
        m_defaultPos = transform.position.y;
        m_jumpStart.Play();
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        if (m_cutscenePlayin || !m_canMove)
        {
            if(!m_agent.enabled)
               m_animationHandler.m_speedAnimator =  0f;
            else
            {
                transform.rotation = LookOverride.rotation;
                if(m_velocity.magnitude > 1f)
                    transform.forward = m_agent.velocity.normalized;
            }
            return;
        }
        m_jumpTimer -= Time.fixedDeltaTime;
        m_glideTimer -= Time.fixedDeltaTime;
        bool test = m_isGrounded;
        m_isGrounded = Physics.CheckSphere(m_groundCheck.position, m_groundDistance, m_groundMask);



        if(test != m_isGrounded && m_isGrounded == true)
        {
            m_jumpStart.Stop();
            m_jumpLand.Play();
        }

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        //Move with local dir
        m_move = new Vector3(x, 0f, y).normalized;

        float angle = Mathf.Atan2(m_move.x, m_move.z) * Mathf.Rad2Deg + m_cam.eulerAngles.y;

        if (Input.GetButton("Sprint"))
        {
            m_speed = 12;
        }
        else
        {
            m_speed = 6;
        }

        //float smAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref m_smoothVel, m_smoothTime);

        //resets if grounded
        if (m_isGrounded)
        {
            m_canGlide = false;
            m_isGliding = false;
            // yield return new WaitForSeconds(0.5f);
            if (m_jumpTimer < 0.0f)
            {
                m_isJumping = false;
                m_animationHandler.m_jumpAnimator = false;
                m_animationHandler.m_endJumpAnimator = false;
                // transform.GetChild(4).GetComponent<Animator>().SetBool("isGliding", false);

                // m_decreaseWeight = true;
                m_animationHandler.m_weightJump = 0f;
            }


        }

        //animate

        //Rotate in direction of movement

        //Make a sweep test
        RaycastHit sweep;

        //We can mess with the max distance (1.5f)
        bool isAtwall = m_rb.SweepTest(m_dir, out sweep, 1.5f, QueryTriggerInteraction.Ignore);
        //bool isAtwall = m_rb.SweepTest(m_dir, out sweep, 1.5f, QueryTriggerInteraction.Ignore);

        Debug.DrawLine(transform.position, sweep.point);

        
        //We first check if the sweep has passed and we arent grounded
        if (!m_isGrounded && isAtwall)
        {
            Debug.Log(sweep.collider.tag);
            if (sweep.collider.CompareTag("PickUp"))
            {
                
                m_animationHandler.m_speedAnimator = m_rb.velocity.magnitude;
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                m_dir = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
                m_rb.velocity = new Vector3(m_dir.x * m_speed, m_rb.velocity.y, m_dir.z * m_speed);

                TurnOnMovingSFX();
            }
            else
            {
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                m_dir = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;

                m_rb.velocity = new Vector3(0f + m_movingPlat.x, m_rb.velocity.y, 0f + m_movingPlat.z);
                m_rb.angularVelocity = Vector3.zero;

                TurnOffMovingSFX();
            }
        }
        else if (m_move.magnitude > 0.1f) //if moving
        {
            m_animationHandler.m_speedAnimator = m_rb.velocity.magnitude;
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            m_dir = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
            m_rb.velocity = new Vector3(m_dir.x * m_speed, m_rb.velocity.y, m_dir.z * m_speed);

            TurnOnMovingSFX();
        }
        else //if still
        {
            //transform.rotation = Quaternion.Euler(0f, angle, 0f);
            m_animationHandler.m_speedAnimator = 0;

            //m_dir = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;

            m_rb.velocity = new Vector3(0f + m_movingPlat.x, m_rb.velocity.y, 0f + m_movingPlat.z);
            m_rb.angularVelocity = Vector3.zero;

            TurnOffMovingSFX();
        }




        //Fall down



        //movement if we arent jumping?

        if (!m_isGrounded && !m_isGliding)
        {
            //Debug.Log("call");
            if (m_isJumping && m_rb.velocity.y < 0 && Input.GetButton("Jump") && unlockGlide)
            {
                m_isGliding = true;
                m_animationHandler.m_glideAnimator = true;
            }

            if (m_isJumping && m_rb.velocity.y < 0 && disG < 2.4f)
            {
                m_animationHandler.m_jumpAnimator = true;
            }

            if (!m_isGliding)
            {
                if (m_isJumping && m_rb.velocity.y < 0) //Descent
                    m_rb.AddForce(Physics.gravity * 4f, ForceMode.Acceleration);
                else if (m_isJumping && m_rb.velocity.y > 0 &&
                         !Input.GetButton("Jump")) //Jumping up and not holding the jump button
                    m_rb.AddForce(Physics.gravity * 2.5f, ForceMode.Acceleration);
                else if (m_isJumping && m_rb.velocity.y > 0) //Jumping up and holding the jump button
                    m_rb.AddForce(Physics.gravity * 1.25f, ForceMode.Acceleration);
                else if (!m_isGliding) //Normal gravity in other case
                    m_rb.AddForce(Physics.gravity, ForceMode.Acceleration);
            }
        }

        if (Input.GetButton("Jump") && m_isGliding)
        {
            if(disG > 5)
                m_rb.AddForce(Physics.gravity / 4f, ForceMode.Acceleration);


            else
            {
                float dis = disG;
                dis = disG / 5f;
                
                m_rb.AddForce((Physics.gravity / 6f) * dis, ForceMode.Acceleration);
            }
        }
        else if (m_isGliding)
        {
            m_isGliding = false;
        }

    }

    public void turnOnGlide()
    {
        unlockGlide = true;
    }


    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.layer == 7)
        {
            m_movingPlat = collision.gameObject.GetComponent<Rigidbody>().velocity;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            m_movingPlat = Vector3.zero;
        }
    }

    public void SetCam()
    {

    }

    public void TurnOffMovingSFX()
    {
        m_walking.Stop();
        m_running.Stop();
    }

    public void TurnOnMovingSFX()
    {
        if (m_isGrounded)
        {
            if (m_speed == 12 && !m_running.isPlaying)
            {
                m_walking.Stop();
                m_running.Play();
            }
            else if (m_speed == 6 && !m_walking.isPlaying)
            {
                m_running.Stop();
                m_walking.Play();
            }
        }
        else
        {
            TurnOffMovingSFX();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Water")
        {
            m_Splash.Play();
        }
    }
};
