using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationControl : MonoBehaviour {


    [SerializeField] Animator animator;
    [SerializeField] CharacterController charCtrl;
    [SerializeField] SoloAiming mouse;

	void Start () {
        animator = GetComponent<Animator>();
	}

	void Update () {

        animator.SetFloat("Vertical",Input.GetAxis("Vertical"));
        animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        animator.SetFloat("CamHorizontal", mouse.x);

        if (charCtrl.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                animator.SetTrigger("StartJump");
                
            }
        }

        animator.SetBool("Running", Input.GetKey(KeyCode.LeftShift));
        

        animator.SetBool("IsGrounded", charCtrl.isGrounded);
	}
}
