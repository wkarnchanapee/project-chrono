using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    float x, y, z;
    [SerializeField] float moveSpd, fallSpd, jumpHeight;
    CharacterController charCtrl;
    Vector3 moveDir;
    // Use this for initialization
    void Start()
    {
        charCtrl = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

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

        charCtrl.Move(moveDir * Time.deltaTime);
    }
}
