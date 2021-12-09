using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour
{
    private float m_walkSpeed = 7.5f;
    private float m_runningSpeed = 11.5f;
    private float m_jumpSpeed = 8.0f;
    private float m_gravity = 20.0f;
    public Camera m_playerCamera;
    private float m_lookSpeed = 2.0f;
    private float m_directionXLimit = 45.0f;

    CharacterController m_charController;
    Vector3 m_moveDirection = Vector3.zero;
    private float m_rotationX = 0;

    [HideInInspector]
    public bool m_canMove = true;

    void Start()
    {
        m_charController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = m_canMove ? (isRunning ? m_runningSpeed : m_walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = m_canMove ? (isRunning ? m_runningSpeed : m_walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = m_moveDirection.y;
        m_moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && m_canMove && m_charController.isGrounded)
        {
            m_moveDirection.y = m_jumpSpeed;
        }
        else
        {
            m_moveDirection.y = movementDirectionY;
        }
        if (!m_charController.isGrounded)
        {
            m_moveDirection.y -= m_gravity * Time.deltaTime;
        }

        m_charController.Move(m_moveDirection * Time.deltaTime);

        if (m_canMove)
        {
            m_rotationX += -Input.GetAxis("Mouse Y") * m_lookSpeed;
            m_rotationX = Mathf.Clamp(m_rotationX, -m_directionXLimit, m_directionXLimit);
            m_playerCamera.transform.localRotation = Quaternion.Euler(m_rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * m_lookSpeed, 0);
        }
    }
}
