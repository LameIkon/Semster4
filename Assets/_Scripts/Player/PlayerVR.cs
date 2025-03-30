using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class PlayerVR : Singleton<PlayerVR>
{
    [SerializeField] private InputActionAsset _inputActionAsset;
    private InputActionMap _leftActionMap;
    private InputActionMap _rightActionMap;

    // Left hand
    private InputAction _leftGripAction;

    // Rigth hand
    private InputAction _rightGripAction;
    private InputAction rightSelectionAction;

    // To track held objects
    private GameObject _heldObject;

    private void OnEnable()
    {
        // Initialize the left and right Action Maps from the Input Action Asset
        _leftActionMap = _inputActionAsset.FindActionMap("XRI Left Interaction");  // Left Interaction Map
        _rightActionMap = _inputActionAsset.FindActionMap("XRI Right Interaction");  // Right Interaction Map

        // Find the Select actions in each Action Map
        _leftGripAction = _leftActionMap.FindAction("Select");     
        _rightGripAction = _rightActionMap.FindAction("Select");

        // Find the activate action in Action Map
        rightSelectionAction = _rightActionMap.FindAction("Activate"); 

        // Enable both Action Maps
        _leftActionMap.Enable();
        _rightActionMap.Enable();

        // Subscribe to the action event
        _leftGripAction.performed += OnLeftGripPerformed;
        _rightGripAction.performed += OnRightGripPerformed;
        rightSelectionAction.performed += OnRightSelectionPerformed;
    }

    private void OnDisable()
    {
        // Unsubscribe from the action event when disabling the actions
        _leftGripAction.performed -= OnLeftGripPerformed;
        _rightGripAction.performed -= OnRightGripPerformed;

        // Disable both Action Maps
        _leftActionMap.Disable();
        _rightActionMap.Disable();
    }

    private void OnLeftGripPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("Left Grip Pressed");
    }

    // Handler when the right grip button is pressed
    private void OnRightGripPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("Right Grip Pressed");
    }

    private void OnRightSelectionPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("Selection Pressed");
    }

    public bool IsHoldingObject()
    {
        return _heldObject != null;
    }

    public void OnObjectTriggerEnter(Collider other)
    {
        _heldObject = other.gameObject;
    }

    public void OnObjectTriggerExit()
    {
        _heldObject = null;
    }
}
