using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SeñoraController : MonoBehaviour
{
    //Movement and rotation
    float horizontal = 0;
    float vertical = 0;
    float mouseRotation = 0;

    bool isMouseLocked = false;
    public float rotationSpeed = 2;
    public float speed = 5;

    //jump and gravity
    public float gravity = 9.8f;
    public float speedY = 0;
    bool jumped = false;
    bool isGrounded = false;
    public float jumpSpeed = 100;

    public float raycastLength = 10;

    BoxCollider feetCollider;

    //Animations
    Animator animator;

    // Rigidbody m_Rigidbody;
    CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        //m_Rigidbody = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();

        feetCollider = GetComponent<BoxCollider>();

        animator = GetComponent<Animator>();

        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
        isMouseLocked = true;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        mouseRotation = Input.GetAxis("Mouse X");

        transform.Rotate(new Vector3(0, mouseRotation * rotationSpeed, 0) * Time.deltaTime);

        if(vertical >0)
        {
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }

        if (Input.GetKeyDown("c"))
        {
            SwitchLockedMouse();
        }

        isGrounded = IsGrounded();
        


        if (isGrounded && speedY <= 0)
        {
            speedY = 0f;
            animator.SetBool("Jumping", false);
            if (Input.GetKeyDown("space"))
            {
                speedY = jumpSpeed;
            }
        }

        if(speedY >0)
        {
            animator.SetBool("Jumping", true);
        }




        ApplyGravity();
    }

    private void FixedUpdate()
    {
        controller.Move(transform.forward * vertical * speed * Time.deltaTime);
        controller.Move(transform.right * horizontal * speed * Time.deltaTime);
    }

    void ApplyGravity()
    {
        if (!isGrounded)
        {
            speedY -= gravity * Time.deltaTime;
        }

        controller.Move(new Vector3(0, speedY * Time.deltaTime, 0));
    }

    void SwitchLockedMouse()
    {
        if (isMouseLocked)
        {
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;
        }
        else
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            UnityEngine.Cursor.visible = false;
        }
        isMouseLocked = !isMouseLocked;
    }

    bool IsGrounded()
    {
        bool hit = Physics.Raycast(new Ray(transform.position, new Vector3(0, -1, 0)),raycastLength);

        Color rayColor = Color.red;

        if(hit)
        {
            rayColor = Color.red;
        }
        else
        {
            rayColor = Color.green;
        }

        Debug.DrawRay(transform.position, new Vector3(0, -raycastLength, 0),rayColor,1f);

        return hit;
    }
}
