using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    CharacterController m_controller;
    Animator m_animator;
    public ParticleSystem slashSword;
    public Camera cam;
    public float speed = 6.0F;
    public float maxJumpHeight = 4f;
    public float timeToJumpApex = .4f;

    private float gravity;
    private float jumpSpeed;
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 m_CamForward;
    private int countJump = 0;
    private float yVel;
    private float m_MovingTurnSpeed = 360;
    private float m_StationaryTurnSpeed = 180;
    private Vector3 rotationMovement;
    private float turnAmount;
    private float m_ForwardAmount;
    private float turnSpeed;
    private bool isAttacking;

    void Start() {
        m_controller = GetComponent<CharacterController>();

        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpSpeed = Mathf.Abs(gravity) * timeToJumpApex;
        isAttacking = false;
    }

    void Update() {
        yVel = moveDirection.y;

        InputMovement();

        RotateMovement();

        Jump();

        moveDirection.y += gravity * Time.deltaTime;
        m_controller.Move(moveDirection * Time.deltaTime);

        if (Input.GetButtonDown("Fire1"))
        {
            slashSword.Play();
        }
    }

    void InputMovement()
    {
        m_CamForward = Vector3.Scale(cam.transform.forward, new Vector3(1, 0, 1)).normalized;
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        moveDirection = (moveDirection.z * m_CamForward + moveDirection.x * cam.transform.right) * speed;
    }

    void RotateMovement() {
        rotationMovement = transform.InverseTransformDirection(moveDirection);
        turnAmount = Mathf.Atan2(rotationMovement.x, rotationMovement.z);
        m_ForwardAmount = rotationMovement.z;
        turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
        transform.Rotate(0, turnAmount * turnSpeed * Time.deltaTime, 0);
    }

    void Jump() {
        if (m_controller.isGrounded) {
            countJump = 0;
            if (Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.Space)) {
                moveDirection.y = jumpSpeed;
                countJump++;
            }
        }
        else {
            if (countJump == 1 && (Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.Space))) {
                moveDirection.y = jumpSpeed * 0.8f;
                countJump++;
            }
            else {
                moveDirection.y = yVel;
            }
        }
    }
}
