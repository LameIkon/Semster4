using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Simulation;

[RequireComponent(typeof(BoxCollider))]
public class GameStarter : MonoBehaviour
{
    public static event Action s_OnGameStartEvent;

#region Unity Methods
    private void Start()
    {
        Reset();
    }

    private void OnTriggerEnter(Collider target)
    {
        if (target.GetComponent<CharacterController>() != null || target.GetComponent<XRDeviceSimulator>() != null)
        {
            s_OnGameStartEvent?.Invoke();
            Destroy(this);
        }
    }

    private void Reset()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }
#endregion
}