using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashDescriptionUIEnable : MonoBehaviour
{
    [SerializeField] private GameObject _trashUIPanel;
    private Transform _playerCamera; // NOT XR RIG - MAIN CAMERA OR GAZE INTERACTOR (CHILD OF XR RIG)


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

    }

    public void OnGrab()
    {
        _trashUIPanel.SetActive(true);
        _trashUIPanel.transform.SetParent(_playerCamera);
    }

    public void OnRelease()
    {
        _trashUIPanel.SetActive(false); // Hide UI when released
        _trashUIPanel.transform.SetParent(null);
    }

   
}
