﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakTheLight : MonoBehaviour
{
    private void OnCollisionStay(Collision collision) {
        Debug.Log (collision.collider.name);
    }


}
