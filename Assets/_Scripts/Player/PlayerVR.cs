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


    public static event Action<bool> OnGripStateChanged; // Event to notify listeners
    public static event Action<bool> OnSelectStateChanged; // Event to notify listeners

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

    private void OnGripPerformed(InputAction.CallbackContext context)
    {
        bool isHolding = context.performed;
        OnGripStateChanged?.Invoke(isHolding);


        //if (context.performed)
        //{
        //    //_IsHoldingObjectButton = true;
        //    OnGripStateChanged?.Invoke(true);
        //}
        //else if (context.canceled)
        //{
        //    //_IsHoldingObjectButton = false;
        //    OnGripStateChanged?.Invoke(false);
        //}
    }

    public void TestButton() // button
    {
        OnGripStateChanged?.Invoke(true);
    }

    public void Test2Button() // button
    {
        OnSelectStateChanged?.Invoke(true);
    }


    public bool IsHoldingObject() // For other scripts to check if player is currently holding an object
    {
        return true;
        //return _heldObject != null;
    }

    //private void OnGripReleased(InputAction.CallbackContext context)
    //{
    //    _IsHoldingObjectButton = false;
    //}

    private void OnSelectionPerformed(InputAction.CallbackContext context)
    {
        _IsInspectingObjectButton = true;
    }

    //private void OnSelectionReleased(InputAction.CallbackContext context)
    //{
    //    _IsInspectingObjectButton = false;
    //}


    public void OnObjectTriggerEnter(Collider other) // Used by hands for when entering its trigger
    {
        _heldObject = other.gameObject;
    }

    public void OnObjectTriggerExit() // Used by hands for when entering exiting trigger
    {
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
        _rightGripAction.performed += OnGripPerformed;
        //_rightGripAction.canceled += OnGripReleased;
        _rightSelectionAction.performed += OnSelectionPerformed;
        //_rightSelectionAction.canceled += OnSelectionReleased;
    }

    private void EnableLeftMap()
    {
        _leftGripAction.performed += OnGripPerformed;
        //_leftGripAction.canceled += OnGripReleased;
        _leftSelectionAction.performed += OnSelectionPerformed;
        //_leftSelectionAction.canceled += OnSelectionReleased;
    }
    private void DisableRightMap()
    {
        _rightGripAction.performed -= OnGripPerformed;
       // _rightGripAction.canceled -= OnGripReleased;
        _rightSelectionAction.performed -= OnSelectionPerformed;
        //_rightSelectionAction.canceled -= OnSelectionReleased;
    }

    private void DisableLeftMap()
    {
        _leftGripAction.performed -= OnGripPerformed;
        //_leftGripAction.canceled -= OnGripReleased;
        _leftSelectionAction.performed -= OnSelectionPerformed;
        //_leftSelectionAction.canceled -= OnSelectionReleased;
    }
}
