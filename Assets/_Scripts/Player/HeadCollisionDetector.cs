using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCollisionDetector : MonoBehaviour
{
    private float _detectionDelay = 0.05f;
    private float _detectionDistance = 0.2f;
    [SerializeField] private LayerMask _detectionLayers;

    public List<RaycastHit> _DetectionColliderHits { get; private set; }

    private float _currentTime;

    private List<RaycastHit> PerformDetection(Vector3 position, float distance, LayerMask mask)
    {
        List<RaycastHit> detectedHits = new();
        List<Vector3> directions = new() { transform.forward, transform.right, -transform.right };

        RaycastHit hit;
        foreach (Vector3 direction in directions)
        {
            if (Physics.Raycast(position, direction, out hit, distance, mask))
            {
                detectedHits.Add(hit);
            }
        }

        return detectedHits;
    }

    private void Start()
    {
        _DetectionColliderHits = PerformDetection(transform.position, _detectionDistance, _detectionLayers);
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime > _detectionDelay)
        {
            _currentTime = 0;
            _DetectionColliderHits = PerformDetection(transform.position, _detectionDistance, _detectionLayers);
        }
    }
}