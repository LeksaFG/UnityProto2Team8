using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class InteractiveObject : MonoBehaviour, IInteractive
{
    [Tooltip("Text that wiil display in the UI when the player looks at this object.")]
    [SerializeField]
    protected private string displayText = nameof(InteractiveObject);

    public virtual string DisplayText => displayText;
    protected AudioSource audioSource;

    protected virtual void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public virtual void InteractWith()
    {
        audioSource.Play();
        Debug.Log($"Player just interacted with {gameObject.name}.");
    }
}
