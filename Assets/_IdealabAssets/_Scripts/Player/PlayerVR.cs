using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerVR : Singleton<PlayerVR>
{
    [SerializeField] private InputActionAsset _inputActionAsset;
    private InputActionMap _leftActionMap;
    private InputActionMap _rightActionMap;

    // Left hand
    private InputAction _leftGripAction;
    private InputAction _leftSelectionAction;

    // Rigth hand
    private InputAction _rightGripAction;
    private InputAction _rightSelectionAction;

    // To track held objects
    private GameObject _heldRightObject; // For other scripts to check if holding an object
    private GameObject _heldLeftObject; // For other scripts to check if holding an object
    private bool _toggleRightInspect;
    private bool _toggleLeftInspect;

    public static event Action<bool> S_OnGripStateChanged; // Event to notify listeners
    public static event Action<bool> S_OnSelectStateChanged; // Event to notify listeners

    private void OnEnable()
    {
        FindActionMaps();
       
        // Enable both Action Maps
        _leftActionMap.Enable();
        _rightActionMap.Enable();

        // Subscribe to the action event
        EnableRightMap();
        EnableLeftMap();
    }

    private void OnDisable()
    {
        // Disable both Action Maps
        _leftActionMap.Disable();
        _rightActionMap.Disable();

        // Unsubscribe from the action event when disabling the actions
        DisableRightMap();
        DisableLeftMap();
    }

    private void OnGripStarted(InputAction.CallbackContext context) // Need delay since it takes time for item to get to hand
    {
        if (context.started)
        {
            Invoke(nameof(InvokeGrip),0.3f);
        }
    }
    private void InvokeGrip()
    {
        S_OnGripStateChanged?.Invoke(true);
    }

    public bool IsHoldingObject() // For other scripts to check if player is currently holding an object
    {
        Debug.Log("check holding");
        if (_heldRightObject != null)
        {
            return true;
        }
        else if (_heldLeftObject != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsInspectingObject() // For other scripts 
    {
        if (_heldRightObject != null && _toggleRightInspect)
        {
            return true;
        }
        else if (_heldLeftObject != null && _toggleLeftInspect)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #region Select
    private void OnSelectRightStarted(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Invoke(nameof(InvokeSelect), 0.2f);
            _toggleRightInspect = true;          
        }
    }

    private void OnSelectRightCancel(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            _toggleRightInspect = false;
            S_OnSelectStateChanged?.Invoke(false);
        }
    }

    private void OnSelectLeftStarted(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Invoke(nameof(InvokeSelect), 0.2f);
            _toggleLeftInspect = true;          
        }
    }

    private void OnSelectLeftCancel(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            _toggleLeftInspect = false;
            S_OnSelectStateChanged?.Invoke(false);
        }
    }
    private void InvokeSelect()
    {
        S_OnSelectStateChanged?.Invoke(true);
    }
    #endregion

    # region Triggers
    public void OnObjectRightTriggerEnter(Collider other) // Used by hands for when entering its trigger
    {
        _heldRightObject = other.gameObject;
    }

    public void OnObjectRightTriggerExit() // Used by hands for when entering exiting trigger
    {
        _heldRightObject = null;
    }

    public void OnObjectLeftTriggerEnter(Collider other) // Used by hands for when entering its trigger
    {
        _heldLeftObject = other.gameObject;
    }

    public void OnObjectLeftTriggerExit() // Used by hands for when entering exiting trigger
    {
        _heldLeftObject = null;
    }
    #endregion

    #region Input map
    private void FindActionMaps()
    {
        // Initialize the left and right Action Maps from the Input Action Asset
        _leftActionMap = _inputActionAsset.FindActionMap("XRI Left Interaction");
        _rightActionMap = _inputActionAsset.FindActionMap("XRI Right Interaction");

        // Find the actions in each Action Map
        _leftGripAction = _leftActionMap.FindAction("Select");
        _rightGripAction = _rightActionMap.FindAction("Select");

        _leftSelectionAction = _leftActionMap.FindAction("Activate");
        _rightSelectionAction = _rightActionMap.FindAction("Activate");
    }

    private void EnableRightMap()
    {
        _rightGripAction.started += OnGripStarted;

        _rightSelectionAction.started += OnSelectRightStarted;
        _rightSelectionAction.canceled += OnSelectRightCancel;
    }

    private void EnableLeftMap()
    {
        _leftGripAction.started += OnGripStarted;

        _leftSelectionAction.started += OnSelectLeftStarted;
        _leftSelectionAction.canceled += OnSelectLeftCancel;
    }
    private void DisableRightMap()
    {
        _rightGripAction.started -= OnGripStarted;

        _rightSelectionAction.started -= OnSelectRightStarted;
        _rightSelectionAction.canceled -= OnSelectRightCancel;
    }

    private void DisableLeftMap()
    {
        _leftGripAction.started -= OnGripStarted;

        _leftSelectionAction.started -= OnSelectLeftStarted;
        _leftSelectionAction.canceled-= OnSelectLeftCancel;
    }

    #endregion

    #region Testing
    public void TestGripButton() // Test
    {
        S_OnGripStateChanged?.Invoke(true);
    }

    public void TestInspectButton() // Test
    {
        S_OnSelectStateChanged?.Invoke(true);
    }

    public static event Action<bool> S_TestTrashing; // Used for tutorial
    public void TestTrashingButton() // button
    {
        S_TestTrashing?.Invoke(true);
    }

    public static event Action<bool> S_TestDoor; // Used for tutorial
    public void TestDoorOpenedButton() 
    {
        S_TestDoor?.Invoke(true);
    }

    #endregion
}
