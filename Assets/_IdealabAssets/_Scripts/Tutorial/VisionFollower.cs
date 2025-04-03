using UnityEngine;

public class VisionFollower : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    private bool _isCentered;

    [Header("Settings")]
    [SerializeField] private bool _canFollow;
    [SerializeField] private float _distance;
    [SerializeField] private float _moveSpeed = 0.025f;
    [SerializeField] private float _centeringTreshold = 0.1f;
    [SerializeField] private float _resetDelay = 2f;
    private float _timeSinceOutOfView = 0f;

    private void Start()
    {
        transform.position = new Vector3(transform.position.x, 1, transform.position.z);
    }

    void Update()
    {
        if (!_canFollow) return;
        HandleVisionFollower();
    }

    private void HandleVisionFollower()
    {

        if (!IsCanvasInView())
        {
            _timeSinceOutOfView += Time.deltaTime;

            if (_timeSinceOutOfView >= _resetDelay)
            {
                Debug.Log("false");
                _isCentered = false;
                _timeSinceOutOfView = 0; // Reset the timer after triggering
            }
        }
        else
        {
            _timeSinceOutOfView = 0; // Reset the timer if back in view
        }

        if (!_isCentered)
        {
            Vector3 targetPosition = FindTargetPosition();

            MoveTowards(targetPosition);

            if (ReachedPosition(targetPosition))
            {
                Debug.Log("true");
                _isCentered = true;
            }
        }
    }

    private Vector3 FindTargetPosition()
    {
        Vector3 flatForward = new Vector3(_camera.forward.x, 0, _camera.forward.z).normalized;
        return _camera.position + (flatForward * _distance);
    }

    private void MoveTowards(Vector3 targetPosition)
    {
        transform.position = new Vector3(
        transform.position.x + (targetPosition.x - transform.position.x) * _moveSpeed, // Move X
        transform.position.y, // Lock Y
        transform.position.z + (targetPosition.z - transform.position.z) * _moveSpeed  // Move Z
    );
    }

    private bool ReachedPosition(Vector3 targetPosition)
    {
        return Vector2.Distance(new Vector2(targetPosition.x, targetPosition.z), new Vector2(transform.position.x, transform.position.z)) < _centeringTreshold;
    }

    private bool IsCanvasInView()
    {
        Vector3 viewportPoint = Camera.main.WorldToViewportPoint(transform.position);
        return viewportPoint.x >= 0 && viewportPoint.x <= 1 && viewportPoint.y >= 0 && viewportPoint.y <= 1 && viewportPoint.z > 0;
    }

}
