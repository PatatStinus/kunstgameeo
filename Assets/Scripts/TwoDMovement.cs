using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDMovement : MonoBehaviour
{
    public JoystickMovement joystickMovement;
    public float playerSpeed;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(joystickMovement.joystickVec.x * playerSpeed, 0);
    }
}