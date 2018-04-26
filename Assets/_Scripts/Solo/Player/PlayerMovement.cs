using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float x, y, z;
    [SerializeField] float maxX, maxY, maxZ;
    [SerializeField] float walkSpd, sprintSpd;
    [SerializeField] float xDamp, zDamp;
    [SerializeField] float moveSpd, fallSpd, jumpHeight;
    CharacterController charCtrl;
    [SerializeField] Vector3 moveDir;
    // Use this for initialization
    void Start()
    {
        charCtrl = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        x += Input.GetAxis("Horizontal");
        z += Input.GetAxis("Vertical");

        x = Mathf.Clamp(x, -maxX, maxX);
        z = Mathf.Clamp(z, -maxZ, maxZ);

        //damping back to 0
        x = Mathf.Lerp(x, 0f, xDamp);
        z = Mathf.Lerp(z, 0f, zDamp);


        //sprint check
        if (Input.GetButton("Sprint"))
        {
            maxX = sprintSpd;
            maxZ = sprintSpd;
        } else
        {
            maxX = walkSpd;
            maxZ = walkSpd;
        }

        //Gravity
        // Check Gravity
        if (charCtrl.isGrounded == true)
        {
            if (Input.GetButtonDown("Jump"))
            {
                y = jumpHeight;

            }
        }
        else
        { 
            y -= fallSpd * Time.deltaTime;
        }

        y = Mathf.Clamp(y, -fallSpd, jumpHeight);

        
        moveDir = transform.TransformDirection(new Vector3(x, y, z) * moveSpd);
        //moveDir = Vector3.Lerp(moveDir, Vector3.zero, 0.5f);

        charCtrl.Move(moveDir * Time.deltaTime);
    }
}
