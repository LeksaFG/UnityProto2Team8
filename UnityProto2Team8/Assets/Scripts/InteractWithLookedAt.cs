using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Detects when player presser interact button while looking at an IInteractive
//and then calls that IInteractive's InteractWith method.
public class InteractWithLookedAt : MonoBehaviour
{
    [SerializeField]
    private DetectLookedAtInteractive detectLookedAtInteractive;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Interact") && detectLookedAtInteractive.LookedAtInteractive != null)
        {
            Debug.Log("Player pressed Interact button.");
            detectLookedAtInteractive.LookedAtInteractive.InteractWith();

        }
    }
}
