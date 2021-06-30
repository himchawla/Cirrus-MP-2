  using System.Collections;
using System.Collections.Generic;
  using Cinemachine;
  using UnityEngine;
using UnityEngine.Serialization;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook m_freeLook;
    [SerializeField]
    private float m_speed = 12;
    private float m_sprintSpeed;
    private float m_baseSpeed;

    private CharacterController m_controller;

    //Jump Variables
    [SerializeField]
    private float m_jumpheight = 2;
    [SerializeField]
    private Transform m_groundChecker;
    [SerializeField]
    private LayerMask m_layerMask;
    [SerializeField]
    private float m_groundDistance = 0.4f;
    [SerializeField]
    private bool m_isGrounded = false;
    [SerializeField]
    private float m_gravity = 20.0f;
    [SerializeField] 
    private Transform m_playerCam;

    private bool isGliding = false;

    //Axis
    private float m_VelX;
    private float m_VelZ;
    private float m_smoothX;
    private float m_smoothZ;

    private Vector3 Velocity;
    private Vector3 movementRaw;
    private Vector3 movementSmooth;
    private float movementAir;

    RaycastHit hitInfo;
    RaycastHit SlopeHit;
    Vector3 forward;
    float groundAngle;
    float slopeDistance;

    Vector3 SlopeOff;
    bool onSlope = false;

    float disG;

    private Animator m_animator;
    bool isJumping;
    private static readonly int Speed = Animator.StringToHash("Speed");


    // Start is called before the first frame update
    void Start()
    {
        m_animator = transform.GetChild(2).GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        m_controller = GetComponent<CharacterController>();
        m_sprintSpeed = m_speed * 2;
        m_baseSpeed = m_speed;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        GetAxis();

        m_isGrounded = Physics.Raycast(transform.position, -Vector3.up, out hitInfo, m_groundDistance, m_layerMask);

        disG = transform.position.y - hitInfo.point.y;

        GetGroundAngle();

        GetForward();

        SpeedControl();

        MovementControl();
    }

    private void Update()
    {
        GlideControl();
        ApplyGravity();
    }

    void GetAxis()
    {
        m_VelX = Input.GetAxisRaw("Horizontal");
        m_VelZ = Input.GetAxisRaw("Vertical");
        m_smoothX = Input.GetAxis("Horizontal");
        m_smoothZ = Input.GetAxis("Vertical");

        movementRaw = new Vector3(m_VelX, 0.0f, m_VelZ);
        movementSmooth = new Vector3(m_smoothX, 0.0f, m_smoothZ);
    }

    void GetForward()
    {
        if (!m_isGrounded)
        {
            forward = transform.forward;
            return;
        }

        forward = Vector3.Cross(hitInfo.normal, -transform.right);
    }

    void GetGroundAngle()
    {
        Physics.Raycast(transform.position, -Vector3.up, out SlopeHit, 2.0f, m_layerMask);

        if (SlopeHit.point != Vector3.zero)
        {
            slopeDistance = transform.position.y - SlopeHit.point.y;

            SlopeOff = Vector3.Cross(SlopeHit.normal, -transform.right);
            groundAngle = Vector3.Angle(SlopeHit.normal, transform.forward);
            if (groundAngle == 90)
            {
                m_groundDistance = (m_controller.height / 2) + m_controller.skinWidth + 0.0004f;
                onSlope = true;
            }
            else
            {
                m_groundDistance = slopeDistance;
                onSlope = false;
            }
        }
    }

    void SpeedControl()
    {
        if (Input.GetButton("Sprint"))
        {
            m_speed = m_sprintSpeed;
        }
        else
        {
            m_speed = m_baseSpeed;
        }

        if (m_isGrounded) //Whenever player is grounded
        {
            movementAir = m_speed;
            m_freeLook.m_XAxis.m_MaxSpeed = 450;
        }
    }

    void GlideControl()
    {
        if (!m_isGrounded && Input.GetButtonDown("Jump"))
        {
            Velocity = Vector3.zero;
            isGliding = true;
            //No movement Airspeed while gliding
            movementAir = 0;
        }
        if (!m_isGrounded && Input.GetButtonUp("Jump"))
        {
            isGliding = false;
            //Return to base speed on release
            movementAir = m_baseSpeed;
        }

        if (m_isGrounded)
        {
            isGliding = false;
        }
    }

   void MovementControl()
    {
        if (movementRaw.magnitude < 0.1f)
        {
            m_animator.SetFloat(Speed, 0.0f);
        }
        if(m_isGrounded && groundAngle >= 130)
        {
            m_controller.Move(SlopeOff * m_speed * Time.fixedDeltaTime);
            m_animator.SetFloat(Speed, (SlopeOff * m_speed).magnitude);
        }

        if (m_isGrounded && movementRaw.magnitude > 0.1f) //If grounded and moving
        {
            if (groundAngle < 130)
            {
                float targetAngle = Mathf.Atan2(movementRaw.x, movementRaw.z) * Mathf.Rad2Deg + m_playerCam.eulerAngles.y;
                transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
                m_controller.Move(forward * (m_speed * Time.fixedDeltaTime));
                m_animator.SetFloat(Speed, (forward * m_speed).magnitude);
            } 
            
            else
            {
                m_controller.Move(SlopeOff * m_speed * Time.fixedDeltaTime);
                m_animator.SetFloat(Speed, (SlopeOff * m_speed).magnitude);
            }

        }
        else if (!m_isGrounded && movementSmooth.magnitude > 0.1f && !isGliding) //If in the air and moving
        {
            float targetAngle = Mathf.Atan2(movementSmooth.x, movementSmooth.z) * Mathf.Rad2Deg + m_playerCam.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            m_controller.Move(transform.forward * (movementAir * Time.fixedDeltaTime));
            m_animator.SetFloat(Speed, (transform.forward * (movementAir )).magnitude);
        }
        else if (!m_isGrounded && !isGliding)//If in the air and not moving / gliding
        {
            m_controller.Move(movementSmooth * movementAir);
            m_animator.SetFloat(Speed, (movementSmooth *movementAir ).magnitude);
        }
        else if (!m_isGrounded && isGliding && movementRaw.magnitude > 0.1f) //if gliding while moving
        {
            float targetAngle = Mathf.Atan2(movementRaw.x, movementRaw.z) * Mathf.Rad2Deg + m_playerCam.eulerAngles.y;

            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            m_controller.Move(transform.forward * (m_baseSpeed * Time.fixedDeltaTime));
            m_animator.SetFloat(Speed, (transform.forward * (m_baseSpeed )).magnitude);

        }
        else if (!m_isGrounded && isGliding) //If gliding without moving
        {
            m_controller.Move(movementSmooth * (movementAir * Time.fixedDeltaTime));
            m_animator.SetFloat(Speed, (movementSmooth * (movementAir )).magnitude);
        }
    }
    void ApplyGravity()
    {
        if (Velocity.y < 0)
        {
            isJumping = false;
        }
        if (Input.GetButtonDown("Jump") && m_isGrounded)
        {
            isJumping = true;
            Velocity.y += m_jumpheight;
        }
        else if (!m_isGrounded && isGliding)
        {
            //We are not aiming for a exponential fall,
            //but a constant one
            // m_freeLook.m_XAxis.m_MaxSpeed = 450;
            Velocity.y += (m_gravity / 4) * Time.deltaTime;
        }
        else if (!m_isGrounded)
        {
            Velocity.y += m_gravity * Time.deltaTime;
            //m_freeLook.m_XAxis.m_MaxSpeed = 0;
        }
        else if (m_isGrounded && !onSlope)
        {
            Velocity.y += m_gravity * Time.deltaTime;
        }

        if (m_isGrounded && !isJumping)
        {

            Velocity = Vector3.zero;
        }

        m_controller.Move(Velocity * Time.deltaTime);

    }

}
