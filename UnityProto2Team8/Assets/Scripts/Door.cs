using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Door : InteractiveObject
{
    [Tooltip("Assigning a key here will lock the door. If the player has the key in their inventory, they can open the locked door.")]
    [SerializeField]
    private InventoryObject key;
    [Tooltip("If this is checked, the associated key will be removed from the player inventory when the assigned door is unlocked.")]
    [SerializeField]
    private bool consumesKey;

    [Tooltip("Text that displays when the player interacts with a locked door")]
    [SerializeField]
    private string lockedDisplayText = "Locked";

    [SerializeField]
    [Tooltip("Play this AudioClip when player interacts with a locked door without possesing the key")]
    private AudioClip lockedAudioClip;
    [Tooltip("Play this AudioClip when player opens the door")]
    [SerializeField]
    private AudioClip openAudioClip;
    // public override string DisplayText => isLocked ? lockedDisplayText : base.DisplayText;

    // Just a different way to express the same logic as above.
    public override string DisplayText
    {
        get
        {
            string toReturn;
            if (isLocked)
                toReturn = HasKey ? $"[E] Use {key.ObjectName}" : lockedDisplayText;
            else
                toReturn = base.DisplayText;
            return toReturn;
        }
    }

    private bool HasKey => PlayerInventory.InventoryObjects.Contains(key);
    private Animator animator;
    private bool isOpen = false;
    private bool isLocked;
    private int shouldOpenAnimParameter = Animator.StringToHash("shouldOpenAnimParameter");
    //Using a constructor here to initialize displayText in the editor.
    public Door()
    {
        displayText = nameof(Door);
    }

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        InitlializeIsLocked();
    }

    private void InitlializeIsLocked()
    {
        if (key != null)
            isLocked = true;
    }

    public override void InteractWith()
    {
        if (!isOpen)
        {
            if (isLocked && !HasKey)
            {
                audioSource.clip = lockedAudioClip;
            }
            else //If it's not locked, or if it is locked and player has the key in their inventory.
            {
                audioSource.clip = openAudioClip;
                animator.SetBool(shouldOpenAnimParameter, true);
                displayText = string.Empty;
                isOpen = true;
                UnlockDoor();
            }
            base.InteractWith(); // <--- This plays a sound effect.
        }
    }

    private void UnlockDoor()
    {
        isLocked = false;
        if (key != null && consumesKey)
            PlayerInventory.InventoryObjects.Remove(key);
    }
}
