using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class eMouseLook : MonoBehaviour
{

    [FormerlySerializedAs("mouseSensitivity")] public float m_mouseSensitivity;
    [FormerlySerializedAs("playerBody")] public Transform m_playerBody;

    float m_xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        float mouseY = Input.GetAxis("Mouse Y") * m_mouseSensitivity * Time.deltaTime;

        m_xRotation -= mouseY;

        //if (gameObject.tag == "MainCamera")
            m_xRotation = Mathf.Clamp(m_xRotation, -15f, 15f);

       
        transform.localRotation = (Quaternion.Euler(m_xRotation, 0f, 0f));
    }
}
