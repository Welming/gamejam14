using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputActionMap playerControls;
    public Rigidbody2D player;
    [Range(1.0f, 8.0f)]
    public float moveSpeed = 3;
    private Vector2 moveDirection = Vector2.zero;

    private void Awake()
    {
        
    }

    public void MoveCharacter(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            moveDirection = context.ReadValue<Vector2>();
            player.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        if (context.canceled)
        {
            moveDirection = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        player.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
}
