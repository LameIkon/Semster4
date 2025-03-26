using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCollisionHandler : MonoBehaviour
{
    [SerializeField] private HeadCollisionDetector _detector;
    [SerializeField] private CharacterController _characterController;
    private float _pushBackStrength = 1f;

    
    private Vector3 CalculatePushBackDirection(List<RaycastHit> colliderHits)
    {
        Vector3 combinedNormal = Vector3.zero;
        foreach (RaycastHit hit in colliderHits)
        {
            combinedNormal += new Vector3(hit.normal.x,0,hit.normal.z); 
        }
        return combinedNormal;
    }

    private void Update()
    {
        if (_detector._DetectionColliderHits.Count <= 0)
        {
            return;
        }
        Vector3 pushBackDirection = CalculatePushBackDirection(_detector._DetectionColliderHits);
        _characterController.Move(pushBackDirection.normalized * _pushBackStrength * Time.deltaTime);
    }
}
