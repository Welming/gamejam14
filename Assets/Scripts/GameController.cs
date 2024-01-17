using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class GameController : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject player;
    public InputAction clickControls;
    [InspectorName("Camera Distance"),Range(0.0f, 5.0f)]
    public float cameraDistance;
    [InspectorName("Camera Smoothness"), Range(0.0f, 1.0f)]
    public float cameraSmoothness;

    private Vector3 cameraVelocity = Vector3.zero;

    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        RaycastHit2D rayHit = Physics2D.GetRayIntersection(mainCamera.GetComponent<Camera>().ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (!rayHit.collider) return;

        if (rayHit.collider.gameObject.CompareTag("Cauldron"))
        {
            rayHit.collider.gameObject.GetComponent<Animator>().SetBool("Ignited", true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Vector2.Distance(mainCamera.transform.position, player.transform.Find("Model").transform.position) > cameraDistance)
        {
            mainCamera.transform.position = Vector3.SmoothDamp(mainCamera.transform.position, new Vector3(player.transform.Find("Model").transform.position.x, player.transform.Find("Model").transform.position.y, mainCamera.transform.position.z), ref cameraVelocity, cameraSmoothness);
        }
    }
}