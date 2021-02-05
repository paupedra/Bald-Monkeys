using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ProtagonistController : MonoBehaviour
{
    //Movement and rotation
    float horizontal = 0;
    float vertical = 0;
    Vector2 mouseRotation = Vector2.zero;

    public float rotationSpeed = 2;
    public float speed = 5;

    //jump and gravity
    public float gravity = 9.8f;
    public float speedY = 0;
    bool jumping = false;
    bool isGrounded = false;
    public float jumpSpeed = 100;

    public float raycastLength = 10;
    bool mining = false;

    float miningTimer = 0;

    //Artifacts
    bool[] artifacts = new bool[5];

    //Animations
    Animator animator;

    //Camera
    public GameObject m_HeadJoint;

    //game Manager
    public GameManager manager;

    // Rigidbody m_Rigidbody;
    CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            artifacts[i] = false;
        }
        controller = GetComponent<CharacterController>();

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        int state = manager.GetGameState();

        if(state != 3)
        {
            animator.enabled = false;
            return;
        }

        animator.enabled = true;

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        mouseRotation.x = Input.GetAxis("Mouse X");
        mouseRotation.y = Input.GetAxis("Mouse Y");

        transform.Rotate(new Vector3(0, mouseRotation.x * rotationSpeed * Time.deltaTime, 0) );

        Quaternion previousRotation = m_HeadJoint.transform.rotation;

        m_HeadJoint.transform.Rotate(new Vector3(-mouseRotation.y * rotationSpeed * Time.deltaTime, 0, 0));

        if (m_HeadJoint.transform.rotation.eulerAngles.x >= 30)
        {
            if (m_HeadJoint.transform.rotation.eulerAngles.x <= 345)
            {
                m_HeadJoint.transform.rotation = previousRotation;
            }
        }

        //Mining
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!jumping)
            {
                mining = true;
            }
        }

        IsGroundedCheck();

        UpdateAnimatorParameters();

        ApplyGravity();
    }

    private void FixedUpdate()
    {
        int state = manager.GetGameState();

        if (state != 3)
        {
            return;
        }

        if (mining)
        {
            miningTimer += Time.deltaTime;

            if(miningTimer>= 1.5)
            {
                mining = false;
                miningTimer = 0;
            }
            return;
        }

        controller.Move(transform.forward * vertical * speed * Time.deltaTime);

        if (horizontal != 0)
        {
            controller.Move(transform.right * horizontal * 0.5f * speed  * Time.deltaTime);

            if (vertical == 0)
            {
                if (horizontal > 0)
                {
                    controller.Move(transform.forward * horizontal * speed * Time.deltaTime);
                }
                else
                {
                    controller.Move(transform.forward * -horizontal * speed * Time.deltaTime);
                }
            }
        }
    }

    void ApplyGravity()
    {
        if (!isGrounded)
        {
            speedY -= gravity * Time.deltaTime;
        }

        controller.Move(new Vector3(0, speedY * Time.deltaTime, 0));
    }

    void IsGroundedCheck()
    {
        isGrounded = IsGrounded();

        bool isGroundClose = IsGroundClose();

        if (isGroundClose && speedY <= 0)
        {
            if (isGrounded)
            {
                speedY = 0f;
            }

            jumping = false;

            if (Input.GetKeyDown("space") && !mining)
            {
                speedY = jumpSpeed;
            }

        }
    }

    void UpdateAnimatorParameters()
    {
        if (vertical > 0)
        {
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }

        if (horizontal > 0)
        {
            animator.SetBool("StrafingRight", true);
            animator.SetBool("StrafingLeft", false);
        }
        else if (horizontal == 0)
        {
            animator.SetBool("StrafingRight", false);
            animator.SetBool("StrafingLeft", false);
        }
        else
        {
            animator.SetBool("StrafingRight", false);
            animator.SetBool("StrafingLeft", true);
        }

        if (speedY > 0)
        {
            jumping = true;
        }

        animator.SetBool("Jumping", jumping);

        //Mining
        if (mining)
        {
            animator.SetBool("Mining", true);
        }
        else 
        {
            animator.SetBool("Mining", false);
        }


        //animator.GetCurrentAnimatorStateInfo(1).loop  ---> possible solution to the walk + mining bug
    }

    bool IsGrounded()
    {
        bool hit = Physics.Raycast(new Ray(transform.position, new Vector3(0, -1, 0)),raycastLength);

        Color rayColor;

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
    bool IsGroundClose()
    {
        bool hit = Physics.Raycast(new Ray(transform.position, new Vector3(0, -1, 0)), raycastLength + 0.4f);

        Color rayColor;

        if (hit)
        {
            rayColor = Color.red;
        }
        else
        {
            rayColor = Color.green;
        }

        Debug.DrawRay(transform.position, new Vector3(0, -raycastLength + 0.4f, 0), rayColor, 1f);

        return hit;
    }

    public void AddArtifact(int id) //When an artifact is found the position in array is set to true
    {
        id--;
        artifacts[id] = true;
    }
}
