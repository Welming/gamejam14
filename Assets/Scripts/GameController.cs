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
    private GameObject playerModel;
    public GameObject turretFloorGlowLight;
    public InputAction clickControls;

    [SerializeField]
    private int healthPoints;

    [InspectorName("Camera Distance"),Range(0.0f, 5.0f)]
    public float cameraDistance;
    [InspectorName("Camera Smoothness"), Range(0.0f, 1.0f)]
    public float cameraSmoothness;
    [InspectorName("Smoothness Ratio"), Range(0.0f, 5.0f)]
    public float smoothnessRatio;
    [InspectorName("Close Enough Distance"), Range(0.0f, 5.0f)]
    public float cameraCloseEnoughDistance;
    [InspectorName("Glow Timer"), Range(0.0f, 2.0f)]
    public float hoverGlowInitializedTimer;

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
        playerModel = player.transform.Find("Model").gameObject;
    }

    public void TakeDamage()
    {
        healthPoints--;
    }

    void RaycastToTurret()
    {
        // Turret Glow
        RaycastHit2D rayHit = Physics2D.GetRayIntersection(mainCamera.GetComponent<Camera>().ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (!rayHit.collider) return;

        if (rayHit.collider.gameObject.CompareTag("Turret"))
        {
            rayHit.collider.gameObject.GetComponent<TurretController>().HoverGlow(hoverGlowInitializedTimer);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Turret
        turretFloorGlowLight.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0.0f);
        RaycastToTurret();

        if (Vector2.Distance(mainCamera.transform.position, playerModel.transform.position) > cameraDistance)
        {
            mainCamera.transform.position = Vector3.SmoothDamp(mainCamera.transform.position, new Vector3(playerModel.transform.position.x, playerModel.transform.position.y, mainCamera.transform.position.z), ref cameraVelocity, cameraSmoothness);
        }
        else if (Vector2.Distance(mainCamera.transform.position, playerModel.transform.position) > cameraCloseEnoughDistance)
        {
            mainCamera.transform.position = Vector3.SmoothDamp(mainCamera.transform.position, new Vector3(playerModel.transform.position.x, playerModel.transform.position.y, mainCamera.transform.position.z), ref cameraVelocity, (cameraSmoothness * smoothnessRatio));
        }
    }
}