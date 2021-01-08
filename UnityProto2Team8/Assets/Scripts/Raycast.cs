using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Raycast : MonoBehaviour
{
    private GameObject raycastedObj;

    [SerializeField] private int rayLength = 10;
    [SerializeField] private LayerMask layerMaskInteract;

    void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        if(Physics.Raycast(transform.position, fwd, out hit, rayLength, layerMaskInteract.value))
        {
            if(hit.collider.CompareTag("Object"))
            {
                raycastedObj = hit.collider.gameObject;

                if(Input.GetKeyDown("e"))
                {
                    Debug.Log("INTERACTION");
                    raycastedObj.SetActive(false);
                }
            }
        }
        else
        {
            //add post-interaction stuff here if wanted
        }
        Debug.DrawRay(transform.position, fwd, Color.green);
    }
}
