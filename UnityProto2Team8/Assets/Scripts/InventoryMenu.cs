using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject inventoryMenuItemTogglePrefab;

    [Tooltip("Content of the scrollview for the list of inventory items.")]
    [SerializeField]
    private Transform inventoryListContentArea;

    [Tooltip("Place in the UI for displaying the name of the selected inventory item.")]
    [SerializeField]
    private Text itemLabelText;

    [Tooltip("Place in the UI for displaying the description of the selected inventory item.")]
    [SerializeField]
    private Text descriptionAreaText;

    private static InventoryMenu instance;
    private CanvasGroup canvasGroup;
    private Camera cameraScript;
    private Movement movementScript;
    private Footsteps footstepsScript;
    private HeadBob headbobScript;
    private AudioSource audioSource;

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

    public void ExitMenuButtonClicked()
    {
        HideMenu();
    }

    //Instantiates a new InventoryMenuItemToggle prefab and adds it in to the menu.
    public void AddItemToMenu(InventoryObject inventoryObjectToAdd)
    {
        GameObject clone = Instantiate(inventoryMenuItemTogglePrefab, inventoryListContentArea);
        InventoryMenuItemToggle toggle = clone.GetComponent<InventoryMenuItemToggle>();
        toggle.AssociatedInventoryObject = inventoryObjectToAdd;
    }

    private void ShowMenu()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        cameraScript.enabled = false;
        movementScript.enabled = false;
        footstepsScript.enabled = false;
        headbobScript.enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        audioSource.Play();
    }

    private void HideMenu()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        cameraScript.enabled = true;
        footstepsScript.enabled = true;
        headbobScript.enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        movementScript.enabled = true;
        audioSource.Play();
    }

    //The event handler for InventoryMenuItemSelected.
    private void OnInventoryMenuItemSelected(InventoryObject inventoryObjectThatWasSelected)
    {
        itemLabelText.text = inventoryObjectThatWasSelected.ObjectName;
        descriptionAreaText.text = inventoryObjectThatWasSelected.Desctiption;
    }

    private void OnEnable()
    {
        InventoryMenuItemToggle.InventoryMenuItemSelected += OnInventoryMenuItemSelected;
    }

    private void OnDisable()
    {
        InventoryMenuItemToggle.InventoryMenuItemSelected -= OnInventoryMenuItemSelected;
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
            audioSource = GetComponent<AudioSource>();
    }

        private void Start()
        {
            HideMenu();
            StartCoroutine(WaitForAudioClip());
            Debug.Log("Not done waiting");
        }

        private IEnumerator WaitForAudioClip()
        {
            float originalVolume = audioSource.volume;
            audioSource.volume = 0;
            Debug.Log("Start waiting");
            yield return new WaitForSeconds(audioSource.clip.length);
            Debug.Log("Done waiting");
            audioSource.volume = originalVolume;
        }
}
