using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputAction playerControls;
    public Rigidbody2D player;
    [Range(1.0f, 8.0f)]
    public float moveSpeed = 3;
    private Vector2 moveDirection = Vector2.zero; 

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = playerControls.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        player.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

}
