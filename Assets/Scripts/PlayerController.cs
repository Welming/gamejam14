using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputActionMap playerControls;
    [Range(1.0f, 8.0f)]
    public float moveSpeed = 3;

    private Vector2 moveDirection = Vector2.zero;
    private Vector2 moveMemory = Vector2.zero;
    Animator animator;
    Rigidbody2D rigidBody;

    private void ResetAnimatorParameters(string parameter)
    {
        for (int e = 0; e < animator.parameters.Length;e++)
        {
            if(animator.parameters[e].name != "X" && animator.parameters[e].name != "Y")
            {
                if(animator.parameters[e].name == parameter)
                {
                    animator.SetBool(animator.parameters[e].name, true);
                }

                if (animator.parameters[e].name != parameter)
                {
                    animator.SetBool(animator.parameters[e].name, false);
                }
            }
        }
    }

    public void MoveCharacter(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            moveDirection = context.ReadValue<Vector2>();
            if(context.ReadValue<Vector2>().y != 0)
            {
                moveMemory.y = context.ReadValue<Vector2>().y;
            }
            if (context.ReadValue<Vector2>().x != 0)
            {
                moveMemory.x = context.ReadValue<Vector2>().x;
            }
            rigidBody.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            ResetAnimatorParameters("Walk");
            animator.SetFloat("X", moveMemory.x);
            animator.SetFloat("Y", moveMemory.y);
        }
        if (context.canceled)
        {
            ResetAnimatorParameters("Idle");
            moveDirection = new Vector2(0,0);
        }
    }

    private void Start()
    {
        animator = gameObject.transform.Find("Model").GetComponent<Animator>().GetComponent<Animator>();
        rigidBody = gameObject.transform.Find("Model").GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
}
