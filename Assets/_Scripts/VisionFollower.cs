using UnityEngine;

public class VisionFollower : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private float _distance;

    [SerializeField] private bool _isCentered;

    //private void OnBecameInvisible()
    //{
    //    Debug.Log("False");
    //    _isCentered = false;   
    //}

    private void Start()
    {
        transform.position = new Vector3(transform.position.x, 1, transform.position.z);
    }

    void Update()
    {
        if (!IsCanvasInView())
        {
            Debug.Log("false");
            _isCentered = false;
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
        return _camera.position + (_camera.forward * _distance);
    }

    private void MoveTowards(Vector3 targetPosition)
    {
        transform.position = new Vector3(
        transform.position.x + (targetPosition.x - transform.position.x) * 0.025f, // Move X
        transform.position.y, // Lock Y
        transform.position.z + (targetPosition.z - transform.position.z) * 0.025f  // Move Z
    );
    }

    private bool ReachedPosition(Vector3 targetPosition)
    {
        return Vector2.Distance(new Vector2(targetPosition.x, targetPosition.z), new Vector2(transform.position.x, transform.position.z)) < 0.1f;
    }

    private bool IsCanvasInView()
    {
        Vector3 viewportPoint = Camera.main.WorldToViewportPoint(transform.position);
        return viewportPoint.x >= 0 && viewportPoint.x <= 1 && viewportPoint.y >= 0 && viewportPoint.y <= 1 && viewportPoint.z > 0;
    }

}
