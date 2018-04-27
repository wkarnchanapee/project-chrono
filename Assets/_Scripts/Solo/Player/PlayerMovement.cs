using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    float x, y, z;
    float maxX, maxY, maxZ;
    [Header("Set movement speed caps")]
    [Tooltip("The walking speed cap.")]
    [SerializeField] float walkSpd;
    [Tooltip("The sprinting speed cap.")]
    [SerializeField] float sprintSpd;


    [Tooltip("Enable character control whilst not contacting ground?")]
    [SerializeField] bool inAirMovement;


    [Header("Damping")]
    float xDamp;
    float zDamp;
    [Tooltip("The lerp to zero perecentage whilst mid-air.")]
    [SerializeField] float airDamp;
    [Tooltip("The lerp to zero percentage whilst on land.")]
    [SerializeField] float landDamp;
    [Tooltip("The amount to divide the current horizontal and vertical axis values by whilst mid-air.")]
    [SerializeField] float inAirMoveModifier;

    [Header("Jumping")]
    [Tooltip("The speed at which the player falls at.")]
    [SerializeField] float fallSpd;
    [Tooltip("The maximum height of the jump.")]
    [SerializeField] float jumpHeight;

    [Tooltip("The multiplier that is applied to the final move direction vector. ")]
    [SerializeField] float moveSpd;

    CharacterController charCtrl;
    Vector3 moveDir;
    Camera cam;

    void Start()
    {
        charCtrl = GetComponent<CharacterController>();
        cam = Camera.main;
    }

    void Update()
    {

        // Get Input Axes
        if (charCtrl.isGrounded)
        {
            x += Input.GetAxis("Horizontal");
            z += Input.GetAxis("Vertical");
        } else
        {
            x += Input.GetAxis("Horizontal") / inAirMoveModifier;
            z += Input.GetAxis("Vertical") / inAirMoveModifier;
        }
        
        // clamp forward and horizontal values.
        x = Mathf.Clamp(x, -maxX, maxX);
        z = Mathf.Clamp(z, -maxZ, maxZ);

        // damping forward and horizontal values.
        x = Mathf.Lerp(x, 0f, xDamp);
        z = Mathf.Lerp(z, 0f, zDamp);


        if (charCtrl.isGrounded == true)
        {
            // jump check
            if (Input.GetButtonDown("Jump"))
            {
                y = jumpHeight;

            }

            //sprint check
            if (Input.GetButton("Sprint"))
            {
                maxX = sprintSpd;
                maxZ = sprintSpd;

            }
            else
            {
                maxX = walkSpd;
                maxZ = walkSpd;

            }

            // apply land damping speeds
            xDamp = landDamp;
            zDamp = landDamp;
        }
        else
        { 
            // apply air damping speeds.
            xDamp = airDamp;
            zDamp = airDamp;

            // gravity
            y -= fallSpd * Time.deltaTime;
        }

        // gravity clamp.
        y = Mathf.Clamp(y, -fallSpd, jumpHeight);
        
        moveDir = transform.TransformDirection(new Vector3(x, y, z) * moveSpd);
        charCtrl.Move(moveDir * Time.deltaTime);
    }
}
