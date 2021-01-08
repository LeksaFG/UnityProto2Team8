using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Detects when player presser interact button while looking at an IInteractive
//and then calls that IInteractive's InteractWith method.
public class InteractWithLookedAt : MonoBehaviour
{
private IInteractive lookedAtInteractive;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Interact") && lookedAtInteractive != null)
        {
            Debug.Log("Player pressed Interact button.");
            lookedAtInteractive.InteractWith();

        }
    }
    //Event handler for DetectLookedAtInteractive.LookedAtInteractiveChanged
    private void OnLookedAtInteractiveChanged(IInteractive newLookedAtInteractive)
    {
        lookedAtInteractive = newLookedAtInteractive;
    }
    #region Event subsctiption / unsubscription
    private void OnEnable()
    {
        DetectLookedAtInteractive.LookedAtInteractiveChanged += OnLookedAtInteractiveChanged;
    }
    private void OnDisable()
    {
        DetectLookedAtInteractive.LookedAtInteractiveChanged -= OnLookedAtInteractiveChanged;
    }
    #endregion
}
