using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryObject : InteractiveObject
{
    [Tooltip("The name of the object, as it will appear in the inventory menu UI.")]
    [SerializeField]
    private string objectName = nameof(InventoryObject);    
    
    [Tooltip("Description text shown in the inventory menu when an item is selected.")]
    [TextArea(3, 8)]
    [SerializeField]
    private string description;

    [Tooltip("Icon of an item in the inventory menu.")]
    [SerializeField]
    private Sprite icon;
    
    public string ObjectName => objectName;

    private new Renderer renderer;
    private new Collider collider;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        collider = GetComponent<Collider>();
    }
    
    public InventoryObject()
    {
        displayText = $"Take {objectName}";
    }
    
    //When the player interacts with an inventory object, do 2 things:
    //1. Add the inventory object to the PlayerInventory list
    //2. Remove the object from the game world
    public override void InteractWith()
    {
        base.InteractWith();
        PlayerInventory.InventoryObjects.Add(this);
        renderer.enabled = false;
        collider.enabled = false;
        Debug.Log($"Inventory menu game object name {InventoryMenu.Instance.name}");
    }
}
