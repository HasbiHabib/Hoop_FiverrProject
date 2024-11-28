using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControl : MonoBehaviour
{
    public Transform cam;
    public float Speed = 6f;
    public float jumpForce;
    float turnSmoothTime = 1f;
    float turnSmoothvelocity;
    float movingSpeed;

    public Animator Animator;
    private CharacterController charCont;

    private Vector3 direction;
    private Vector3 moveDir;

    void Start()
    {
        charCont = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        direction = new Vector3(horizontal, 0, vertical).normalized;
        // Horizontal movement control .................................
        if (direction.magnitude >= 0.1f)
        {
            float targerAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targerAngle, ref turnSmoothvelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            moveDir = Quaternion.Euler(0f, targerAngle, 0f) * Vector3.forward;
            movingSpeed = direction.magnitude * Speed;
            charCont.Move(moveDir.normalized * Speed * Time.deltaTime);
             Animator.SetBool("walk", true);
        }
        else
        {
            movingSpeed = 0;
            Animator.SetBool("walk", false);
        }
        
    }
}