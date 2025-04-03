using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{

    public Transform _PlayerCamera;
    public Transform _ThisPortal;
    public Transform _OtherPortal;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       UpdateCameraPos();
    }

    private void UpdateCameraPos()
    {
        Vector3 playerOffsetFromPortal = _PlayerCamera.position - _OtherPortal.position;
        transform.position = _ThisPortal.position + playerOffsetFromPortal;
    }
}
