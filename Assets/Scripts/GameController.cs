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
    public GameObject pauseMenuObject;
    public GameObject gameOverObject;

    public GameObject bloodthorneReward;
    public GameObject manaBloomReward;
    public GameObject sparkseedReward;


    [Range(0.0f, 3.0f)]
    public float spawnPeriod = 1;
    [Range(0.0f, 3.0f)]
    public float spawnPeriodModifier = 1;

    [Range(0.0f, 10.0f)]
    public float spawnersScalingSpeed = 0.0f;
    [Range(5, 100)]
    public int spawnersMaximumScaling = 10;
    [Range(0.0f, 10.0f)]
    public float enemyScaling = 1.0f;

    [Range(0.0f, 10.0f)]
    public float spriteScaling = 1.0f;
    [Range(0.0f, 2.0f)]
    public float spriteScalingSpeed = 1.0f;

    [Range(0.0f, 2.0f)]
    public float lightScalingSpeed = 1.0f;

    [Range(0.5f, 2.0f)]
    public float textEffectSpeed = 1.0f;
    [Range(0.0f, 1.0f)]
    public float textEffectDistance = 1.0f;

    public TMP_FontAsset textFont;

    public bool turretMenuOpened = false;

    public int healthPoints = 10;

    public int emberCount = 0;
    public int emberTotalCount = 0;

    public int bloodthorneCount = 0;
    public int manaBloomCount = 0;
    public int sparkseedCount = 0;

    public float waveTimer;
    public float waveLength = 60;
    public bool waveActive = true;

    public List<int> turretUpgradeCosts;

    [Range(0.0f, 5.0f)]
    public float cameraDistance;
    [Range(0.0f, 1.0f)]
    public float cameraSmoothness;
    [Range(0.0f, 5.0f)]
    public float smoothnessRatio;
    [Range(0.0f, 5.0f)]
    public float cameraCloseEnoughDistance;
    [Range(0.0f, 2.0f)]
    public float hoverGlowInitializedTimer;
    [Range(0.0f, 1.0f)]
    public float menuLightDimIntensity = 0.4f;

    private Vector3 cameraVelocity = Vector3.zero;

    public bool pauseMenuOpened;

    public void WavesCycling()
    {
        if (waveTimer > 0)
        {
            if (waveActive)
            {
                waveTimer -= Time.deltaTime;
                enemyScaling += spawnersScalingSpeed * Time.deltaTime;
            }
        }
        if (waveTimer <= 0 && waveActive)
        {
            Instantiate(bloodthorneReward);
            Instantiate(manaBloomReward);
            Instantiate(sparkseedReward);
            waveActive = false;
        }
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if(pauseMenuOpened) { return; }

        if (!context.started) return;
        RaycastHit2D rayHit = Physics2D.GetRayIntersection(mainCamera.GetComponent<Camera>().ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (!rayHit.collider) 
        {
            turretMenuOpened = false;
            return; 
        }

        if (rayHit.collider.gameObject.CompareTag("Option"))
        {
            if (rayHit.collider.gameObject.transform.parent.transform.parent.transform.parent.GetComponent<RedTurretController>() != null)
            {
                rayHit.collider.gameObject.transform.parent.transform.parent.transform.parent.GetComponent<RedTurretController>().ActivateButton(rayHit.collider.gameObject.GetComponent<OptionInformation>().buttonIndex);
            }

            if (rayHit.collider.gameObject.transform.parent.transform.parent.transform.parent.GetComponent<YellowTurretController>() != null)
            {
                rayHit.collider.gameObject.transform.parent.transform.parent.transform.parent.GetComponent<YellowTurretController>().ActivateButton(rayHit.collider.gameObject.GetComponent<OptionInformation>().buttonIndex);
            }

            if (rayHit.collider.gameObject.transform.parent.transform.parent.transform.parent.GetComponent<BlueTurretController>() != null)
            {
                rayHit.collider.gameObject.transform.parent.transform.parent.transform.parent.GetComponent<BlueTurretController>().ActivateButton(rayHit.collider.gameObject.GetComponent<OptionInformation>().buttonIndex);
            }

            if (rayHit.collider.gameObject.transform.parent.transform.parent.transform.parent.GetComponent<GreenTurretController>() != null)
            {
                rayHit.collider.gameObject.transform.parent.transform.parent.transform.parent.GetComponent<GreenTurretController>().ActivateButton(rayHit.collider.gameObject.GetComponent<OptionInformation>().buttonIndex);
            }

            if (rayHit.collider.gameObject.transform.parent.transform.parent.transform.parent.GetComponent<PurpleTurretController>() != null)
            {
                rayHit.collider.gameObject.transform.parent.transform.parent.transform.parent.GetComponent<PurpleTurretController>().ActivateButton(rayHit.collider.gameObject.GetComponent<OptionInformation>().buttonIndex);
            }

            if (rayHit.collider.gameObject.transform.parent.transform.parent.transform.parent.GetComponent<OrangeTurretController>() != null)
            {
                rayHit.collider.gameObject.transform.parent.transform.parent.transform.parent.GetComponent<OrangeTurretController>().ActivateButton(rayHit.collider.gameObject.GetComponent<OptionInformation>().buttonIndex);
            }

            if (rayHit.collider.gameObject.transform.parent.transform.parent.GetComponent<PillarController>() != null)
            {
                rayHit.collider.gameObject.transform.parent.transform.parent.GetComponent<PillarController>().ActivateButton(rayHit.collider.gameObject.GetComponent<OptionInformation>().buttonIndex);
            }

            if (rayHit.collider.gameObject.transform.parent.transform.parent.transform.parent.GetComponent<TurretLocationController>() != null)
            {
                rayHit.collider.gameObject.transform.parent.transform.parent.transform.parent.GetComponent<TurretLocationController>().ActivateButton(rayHit.collider.gameObject.GetComponent<OptionInformation>().buttonIndex);
            }

            if (rayHit.collider.gameObject.transform.parent.transform.parent.transform.parent.GetComponent<TurretController>() != null)
            {
                rayHit.collider.gameObject.transform.parent.transform.parent.transform.parent.GetComponent<TurretController>().ActivateButton(rayHit.collider.gameObject.GetComponent<OptionInformation>().buttonIndex);
            }
            if (rayHit.collider.gameObject.transform.parent.transform.parent.transform.parent.transform.parent.GetComponent<TurretController>() != null)
            {
                rayHit.collider.gameObject.transform.parent.transform.parent.transform.parent.transform.parent.GetComponent<TurretController>().ActivateButton(rayHit.collider.gameObject.GetComponent<OptionInformation>().buttonIndex);
            }
            return;
        }

        if (rayHit.collider.gameObject.CompareTag("Turret") && !turretMenuOpened)
        {
            if (rayHit.collider.gameObject.GetComponent<TurretLocationController>() != null)
            {
                rayHit.collider.gameObject.GetComponent<TurretLocationController>().currentFocus = true;
                turretMenuOpened = true;
                return;
            }

            if (rayHit.collider.gameObject.GetComponent<TurretController>() != null)
            {
                rayHit.collider.gameObject.GetComponent<TurretController>().currentFocus = true;
                turretMenuOpened = true;
                return;
            }

            if (rayHit.collider.gameObject.GetComponent<RedTurretController>() != null)
            {
                rayHit.collider.gameObject.GetComponent<RedTurretController>().currentFocus = true;
                turretMenuOpened = true;
                return;
            }
            if (rayHit.collider.gameObject.GetComponent<YellowTurretController>() != null)
            {
                rayHit.collider.gameObject.GetComponent<YellowTurretController>().currentFocus = true;
                turretMenuOpened = true;
                return;
            }
            if (rayHit.collider.gameObject.GetComponent<BlueTurretController>() != null)
            {
                rayHit.collider.gameObject.GetComponent<BlueTurretController>().currentFocus = true;
                turretMenuOpened = true;
                return;
            }
            if (rayHit.collider.gameObject.GetComponent<GreenTurretController>() != null)
            {
                rayHit.collider.gameObject.GetComponent<GreenTurretController>().currentFocus = true;
                turretMenuOpened = true;
                return;
            }
            if (rayHit.collider.gameObject.GetComponent<PurpleTurretController>() != null)
            {
                rayHit.collider.gameObject.GetComponent<PurpleTurretController>().currentFocus = true;
                turretMenuOpened = true;
                return;
            }
            if (rayHit.collider.gameObject.GetComponent<OrangeTurretController>() != null)
            {
                rayHit.collider.gameObject.GetComponent<OrangeTurretController>().currentFocus = true;
                turretMenuOpened = true;
                return;
            }
        }

        if (rayHit.collider.gameObject.CompareTag("Pillar") && !turretMenuOpened)
        {
            if (rayHit.collider.gameObject.GetComponent<PillarController>() != null)
            {
                rayHit.collider.gameObject.GetComponent<PillarController>().currentFocus = true;
                turretMenuOpened = true;
                return;
            }
        }

        turretMenuOpened = false;
        return;
    }

    public void MenuOptionsCheck(ref bool focus, GameObject options, List<GameObject> optionsList)
    {
        if (focus && turretMenuOpened)
        {
            if (!options.activeSelf)
            {
                options.SetActive(true);
            }
        }
        else
        {
            if (options.activeSelf)
            {
                for(int e = 0;e < optionsList.Count;e++)
                {
                    if(e == 0)
                    {
                        if (!optionsList[e].activeSelf)
                        {
                            optionsList[e].SetActive(true);
                        }
                    }
                    else
                    {
                        if (optionsList[e].activeSelf)
                        {
                            optionsList[e].SetActive(false);
                        }
                    }
                }
                options.SetActive(false);
            }
            focus = false;
        }
    }

    public void TakeDamage()
    {
        healthPoints--;
    }

    void RaycastToMouse()
    {
        // Turret Glow
        RaycastHit2D rayHit = Physics2D.GetRayIntersection(mainCamera.GetComponent<Camera>().ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (!rayHit.collider) return;

        if (rayHit.collider.gameObject.CompareTag("Turret"))
        {
            if(rayHit.collider.gameObject.GetComponent<TurretLocationController>() != null)
            {
                rayHit.collider.gameObject.GetComponent<TurretLocationController>().HoverGlow(hoverGlowInitializedTimer);
            }
        }

        if (rayHit.collider.gameObject.CompareTag("Option"))
        {
            rayHit.collider.gameObject.GetComponent<OptionHover>().HoverGlow(hoverGlowInitializedTimer);
        }
    }

    void Start()
    {
        playerModel = player.transform.Find("Model").gameObject;
        emberTotalCount = emberCount;
    }

    void FixedUpdate()
    {
        if (pauseMenuOpened) { return; }

        if(healthPoints <= 0)
        {
            pauseMenuObject.SetActive(false);
            gameOverObject.SetActive(true);
            pauseMenuOpened = true;
        }

        // Waves
        WavesCycling();

        // Turret
        turretFloorGlowLight.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0.0f);
        RaycastToMouse();

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