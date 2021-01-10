using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenu : MonoBehaviour
{
    private static InventoryMenu instance;
    private CanvasGroup canvasGroup;
    private Camera cameraScript;
    private Movement movementScript;
    private Footsteps footstepsScript;
    private HeadBob headbobScript;
    public static InventoryMenu Instance
    {
        get
        {
            if (instance == null)
                throw new System.Exception("There is currently no InventoryMenu instance, " +
                "but you are trying to access it!");
            return instance;
        }
        private set { instance = value; }
    }

    private bool IsVisible => canvasGroup.alpha > 0;

    private void ShowMenu()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        cameraScript.enabled = false;
        movementScript.enabled = false;
        footstepsScript.enabled = false;
        headbobScript.enabled = false;
        Cursor.lockState = CursorLockMode.None;
    }

    private void HideMenu()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        cameraScript.enabled = true;
        movementScript.enabled = true;
        footstepsScript.enabled = true;
        headbobScript.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetButtonDown("Show Inventory Menu"))
            if (IsVisible)
                HideMenu();
            else
                ShowMenu();
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            throw new System.Exception("There is already an instance of InventoryMenu, and there can only be one");

            canvasGroup = GetComponent<CanvasGroup>();
            cameraScript = FindObjectOfType<Camera>();
            movementScript = FindObjectOfType<Movement>();
            footstepsScript = FindObjectOfType<Footsteps>();
            headbobScript = FindObjectOfType<HeadBob>();
    }

        private void Start()
        {
            HideMenu();
        }
}
