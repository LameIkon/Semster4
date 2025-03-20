using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourFloatingPoint : MonoBehaviour
{
    public GameObject _FloatingPoint;
    public GameObject _ScorePosition;

    [SerializeField] private float _timeUntilDestruction;  // How much time before gameobject is destroyed.
    [SerializeField] private float _pointSpeed;     // Speed at which the object moves to target position.

    private void Start()
    {
        Destroy(gameObject, _timeUntilDestruction);  
    }

    private void FixedUpdate()
    {
        FlyToBoard();
    }

    private void FlyToBoard()
    {
        _FloatingPoint.transform.position = Vector3.MoveTowards(_FloatingPoint.transform.position, _ScorePosition.transform.position, _pointSpeed*Time.deltaTime);
    }
}
