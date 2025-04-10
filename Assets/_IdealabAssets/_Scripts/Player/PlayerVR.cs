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
    private GameObject _heldObject; // For other scripts to check if holding an object

    [Header("booleans")] // For other scripts to check if buttons are being pressed
    public bool _IsHoldingObjectButton;
    public bool _IsInspectingObjectButton;


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

    //private void OnGripPerformed(InputAction.CallbackContext context)
    //{
    //    if (context.started)
    //    {
    //        Debug.Log("started");
    //        //_IsHoldingObjectButton = true;
    //        S_OnGripStateChanged?.Invoke(context.started);
    //        Debug.Log(context.started);
    //    }
    //    else if (context.canceled)
    //    {
    //        Debug.Log("cancelled");
    //        S_OnGripStateChanged?.Invoke(context.canceled);
    //        Debug.Log(context.canceled);
    //    }
    //    else if (context.performed)
    //    {
    //        Debug.Log(context.performed);
    //    }
    //}

    private void OnGripStarted(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("started");
            S_OnGripStateChanged?.Invoke(true);
        }
    }

    private void OnGripCanceled(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            Debug.Log("cancelled");
            S_OnGripStateChanged?.Invoke(false);
        }
    }

    public void TestGripButton()
    {
        S_OnGripStateChanged?.Invoke(true);
    }

    public void TestInspectButton() 
    {
        S_OnSelectStateChanged?.Invoke(true);
    }

    public static event Action<bool> S_TestTrashing; // Used for tutorial
    public void TestTrashingButton() 
    {
        S_TestTrashing?.Invoke(true);
    }

    public static event Action<bool> S_TestDoor; // Used for tutorial
    public void TestDoorOpenedButton() 
    {
        S_TestDoor?.Invoke(true);
    }

    public bool IsHoldingObject() // For other scripts to check if player is currently holding an object
    {
        if (_heldObject != null)
        {
            return true;
        }
        else
        {
            return false;
        }
        //return _heldObject != null;
    }

    public bool IsInspectingObject() // For other scripts 
    {
        if (_heldObject != null && _toggleInspect)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool _toggleInspect;
    private void OnSelectStarted(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("started");
            S_OnGripStateChanged?.Invoke(true);

            if (_heldObject)
            {
                _toggleInspect = true;
            }
            else
            {
                _toggleInspect = false;
            }
            
        }
    }

    private void OnSelectCancel(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            Debug.Log("canceled");
            S_OnGripStateChanged?.Invoke(false);
        }
    }


    public void OnObjectTriggerEnter(Collider other) // Used by hands for when entering its trigger
    {
        Debug.Log("you are holding an object");
        _heldObject = other.gameObject;
    }

    public void OnObjectTriggerExit() // Used by hands for when entering exiting trigger
    {
        Debug.Log("stopped holding object");
        _heldObject = null;
    }

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
        //_rightGripAction.performed += OnGripPerformed;
        _rightGripAction.started += OnGripStarted;
        _rightGripAction.canceled += OnGripCanceled;


        //_rightSelectionAction.performed += OnSelectionPerformed;

        _rightSelectionAction.started += OnSelectStarted;
        _rightSelectionAction.canceled += OnSelectCancel;
    }

    private void EnableLeftMap()
    {
        //_leftGripAction.performed += OnGripPerformed;
        //_leftGripAction.canceled += OnGripReleased;
        //_leftSelectionAction.performed += OnSelectionPerformed;
        //_leftSelectionAction.canceled += OnSelectionReleased;
    }
    private void DisableRightMap()
    {
        //_rightGripAction.performed -= OnGripPerformed;
       // _rightGripAction.canceled -= OnGripReleased;
        //_rightSelectionAction.performed -= OnSelectionPerformed;
        //_rightSelectionAction.canceled -= OnSelectionReleased;

        _rightGripAction.started -= OnGripStarted;
        _rightGripAction.canceled -= OnGripCanceled;
        _rightSelectionAction.started -= OnSelectStarted;
        _rightSelectionAction.canceled -= OnSelectCancel;
    }

    private void DisableLeftMap()
    {
        //_leftGripAction.performed -= OnGripPerformed;
        //_leftGripAction.canceled -= OnGripReleased;
        //_leftSelectionAction.performed -= OnSelectionPerformed;
        //_leftSelectionAction.canceled -= OnSelectionReleased;


    }
}
