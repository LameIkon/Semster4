using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrashDescriptionUIEnable : MonoBehaviour
{
    [SerializeField] private GameObject _trashUIPanel;
    private Transform _playerCamera; // NOT XR RIG - MAIN CAMERA OR GAZE INTERACTOR (CHILD OF XR RIG)

    [SerializeField] private SOTrashData _trashData;
    private TextMeshProUGUI _headerText;
    private TextMeshProUGUI _contentText;


    private void Awake()
    {
        _headerText = _trashUIPanel.GetComponentsInChildren<TextMeshProUGUI>()[0]; //first found, should be Header
        _contentText = _trashUIPanel.GetComponentsInChildren<TextMeshProUGUI>()[1]; 

        UpdateUI(); //we update the texts in awake before Start is called. 
    }

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
        _trashUIPanel.transform.localScale = new Vector3(0.5f,0.5f,0.5f);     
    }

    public void OnRelease()
    {
        _trashUIPanel.SetActive(false); // Hide UI when released
        _trashUIPanel.transform.SetParent(null);
    }

    private void UpdateUI()
    {
        if (_headerText != null)
        {
            _headerText.text = _trashData._Name; 
        }
        if (_contentText != null)
        {
            _contentText.text = _trashData._Description;
        }
    }
}
