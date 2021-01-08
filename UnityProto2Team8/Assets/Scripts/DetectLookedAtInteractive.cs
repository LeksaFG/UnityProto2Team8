using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DetectLookedAtInteractive : MonoBehaviour
{
    // Start is called before the first frame update
    [Tooltip("Starting point of raycast used to detect interactives.")]
    [SerializeField]
    private Transform raycastOrigin;

    [Tooltip("How far from the raycastOrigin we will search for interactive elements.")]
    [SerializeField]
    private float maxRange = 5.0f;

    //Event raised when the player looks at a different IInteractive.
    public static event Action<IInteractive> LookedAtInteractiveChanged; 
    public IInteractive LookedAtInteractive
    {
        get { return lookedAtInteractive; }
        private set //{ lookedAtInteractive = value; }
        {
            bool isInteractiveChanged = value != lookedAtInteractive;
            if (isInteractiveChanged)
            {
                lookedAtInteractive = value;
                LookedAtInteractiveChanged?.Invoke(lookedAtInteractive);
            }
        }
    }
    
    private IInteractive lookedAtInteractive;

    
    private void FixedUpdate()
    {
        LookedAtInteractive = GetLookedAtInteractive();
    }

    //Raycasts forward from the camera to look for IInteractives.
    //Returns the first IInteractive detected, or null if none are found.
    private IInteractive GetLookedAtInteractive()
    {
        Debug.DrawRay(raycastOrigin.position, raycastOrigin.forward * maxRange, Color.red);
        RaycastHit hitInfo;
        bool objectWasDetected = Physics.Raycast(raycastOrigin.position, raycastOrigin.forward, out hitInfo, maxRange);

        IInteractive interactive = null;

        LookedAtInteractive = interactive;

        if (objectWasDetected)
        {
            //Debug.Log($"Player is looking at: {hitInfo.collider.gameObject.name}");
            interactive = hitInfo.collider.gameObject.GetComponent<IInteractive>();
        }



            return interactive;
    }
}
