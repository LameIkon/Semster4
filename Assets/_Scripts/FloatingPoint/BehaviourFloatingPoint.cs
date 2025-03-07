using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourFloatingPoint : MonoBehaviour
{
    public float _DestroyObject = 3f; // Declaring a variable, which will be the countdown for when the GameObject should be destroyed.

    private void Start()
    {
        Destroy(gameObject, _DestroyObject);
    }
}
