using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMainScript : MonoBehaviour
{
    public GameObject dynamicAngleChild;
    
    public float _Speed = 2;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Looking();
    }

    void Movement()
    {
        rb.velocity = getFinalMovementVelocity(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
    Vector3 getFinalMovementVelocity(float horizontal, float vertical)
    {
        Vector3 flatVelocity = getFlatVelocity(horizontal, vertical);
        Vector3 heightVelocity = getHeightVelocity();
        flatVelocity.y = 0;
        return (flatVelocity + heightVelocity);
    }
    Vector3 getHeightVelocity()
    {
        Vector3 heightVel = new Vector3(0, rb.velocity.y, 0);

        if (Input.GetKey("space"))
        {
            if (Physics.Raycast(transform.position, Vector3.down, 0.5f))
            {
                heightVel.y = 3;
            }
        }
        return heightVel;
    }
    Vector3 getFlatVelocity(float horizontal, float vertical)
    {
        Vector3 temporaryStoreAngles = transform.localEulerAngles;
        Vector3 temporarySetAngles = transform.localEulerAngles;
        temporarySetAngles.x = 0;
        temporarySetAngles.z = 0;
        transform.localEulerAngles = temporarySetAngles;
        Vector3 finalVelocity = transform.forward * vertical + transform.right * horizontal;
        transform.localEulerAngles = temporaryStoreAngles;
        finalVelocity = Vector3.MoveTowards(new Vector3(), finalVelocity, 1) * _Speed;
        return finalVelocity;
    }
    void Looking()
    {
        Vector3 angles = transform.eulerAngles;
        angles.y += Input.GetAxis("Mouse X");
        transform.eulerAngles = angles;

        angles = dynamicAngleChild.transform.localEulerAngles;
        angles.x -= Input.GetAxis("Mouse Y");
        dynamicAngleChild.transform.localEulerAngles = angles;
    }
}
