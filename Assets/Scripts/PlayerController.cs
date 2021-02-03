using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float horizontal = 0;
    float vertical = 0;
    float mouseRotation = 0;

    public float rotationSpeed = 2;
    public float speed = 5;

    bool isMouseLocked = false;

    Rigidbody m_Rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isMouseLocked = true;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        mouseRotation = Input.GetAxis("Mouse X");
        
        //Go forward backwards
        m_Rigidbody.AddForce(transform.forward * vertical * speed);
        m_Rigidbody.AddForce(transform.right * horizontal * speed);

        transform.Rotate(new Vector3(0, mouseRotation * rotationSpeed, 0));
        //m_Rigidbody.angularVelocity = new Vector3(0,mouseRotation * rotationSpeed,0);
         
        if(Input.GetKeyDown("c"))
        {
            SwitchLockedMouse();
        }
    }

    void SwitchLockedMouse()
    {
        if(isMouseLocked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        isMouseLocked = !isMouseLocked;
    }
}
