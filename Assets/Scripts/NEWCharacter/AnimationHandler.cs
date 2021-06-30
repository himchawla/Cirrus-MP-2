using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{

    private Animator m_animator;
    private CharacterController m_characterController;
    
    void Start()
    {
        m_animator = transform.GetChild(2).GetComponent<Animator>();
        m_characterController = GetComponentInParent<CharacterController>();
    }
    
    public bool m_jumpAnimator
    {
        get => m_animator.GetBool("isJumping");
        set => m_animator.SetBool("isJumping", value);
    }
    public bool m_glideAnimator
    {
        get => m_animator.GetBool("isGliding");
        set => m_animator.SetBool("isGliding", value);
    }
    public bool m_endJumpAnimator
    {
        get => m_animator.GetBool("Jump");
        set => m_animator.SetBool("Jump", value);
    }
    public float m_speedAnimator
    {
        get => m_animator.GetFloat("Speed");
        set => m_animator.SetFloat("Speed", value);
    }

    public float m_weightCast
    {
        get => m_animator.GetLayerWeight(1);
        set => m_animator.SetLayerWeight(1, value);
    }

    public float m_weightJump
    {
        get => m_animator.GetLayerWeight(2);
        set => m_animator.SetLayerWeight(2, value);
    }
}
