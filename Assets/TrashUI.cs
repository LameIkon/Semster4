using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashUI : MonoBehaviour
{
    [SerializeField] private GameObject _trashUIPanel;
    [SerializeField] private Transform _playerCamera;

    public float _uiDistance; // Distance from player
    public Vector3 _uiOffset;

    // Start is called before the first frame update
    void Start()
    {
        _trashUIPanel.SetActive(false);

        if (_playerCamera == null)
        {
            _playerCamera = Camera.main.transform; // Auto-assign VR camera
        }
    }

    // Update is called once per frame
    void Update()
    {
        PositionUI();
    }

    public void OnGrab()
    {
        _trashUIPanel.SetActive(true);
        PositionUI();
    }

    public void OnRelease()
    {
        _trashUIPanel.SetActive(false); // Hide UI when released
    }

    public void PositionUI()
    {
        if (_playerCamera)
        {
            
            Vector3 targetPosition = _playerCamera.position + _playerCamera.forward * _uiDistance + _uiOffset;

            // Lock the UI's Y position to the player's camera height
            targetPosition.y = _playerCamera.position.y + _uiOffset.y;

            // Move UI to the calculated position
            _trashUIPanel.transform.position = targetPosition;

            // Rotate the UI to always face the camera (only rotate around the Y-axis)
            Vector3 direction = _playerCamera.position - _trashUIPanel.transform.position;
            direction.y = 0; // Prevent rotation around X and Z axes
            _trashUIPanel.transform.rotation = Quaternion.LookRotation(direction);

            // Flip the UI 180 degrees to ensure the front faces the camera
            _trashUIPanel.transform.Rotate(0, 180, 0);
        }    
    }
}
