using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorUnlock : MonoBehaviour
{
    public static event Action OnUnlock;
    // Start is called before the first frame update
    void Start()
    {
        OnUnlock.Invoke();
    }
}
