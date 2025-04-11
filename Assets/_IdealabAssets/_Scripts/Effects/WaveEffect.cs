using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveEffect : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform spawnArea;

    [Header("Settings")]
    [SerializeField] private int _amountToInstantiate;
    [SerializeField] private float _floatingSpeed;
    [SerializeField] private float _delayBetweenInstantiations;
    [SerializeField] private float _timeUntilDestroyed;

    [Header("Wave Effect")]
    [SerializeField] private float _waveFrequency;
    [SerializeField] private float _waveHeight;

    private void OnEnable()
    {
        TrashBin.s_OnTrashedEvent3 += StartEffect;
    }

    private void OnDisable()
    {
        TrashBin.s_OnTrashedEvent3 -= StartEffect;
    }

    private void StartEffect()
    {
        StartCoroutine(InstantiateWaveEffect());
    }

    private IEnumerator InstantiateWaveEffect()
    {
        for (int i = 0; i < _amountToInstantiate; i++)
        {
            Vector3 randomPosition = GetRandomPositionWithinCollider(spawnArea);

            GameObject prefab = Instantiate(_prefab, randomPosition, Quaternion.identity);
            StartCoroutine(FloatUpwards(prefab)); // Start floating upwards 

            yield return new WaitForSeconds(_delayBetweenInstantiations); // Wait before instantiating the next point
        }
    }

    private Vector3 GetRandomPositionWithinCollider(Transform area) // Determind spawn area
    {
        // Ensure the area has a collider
        Collider areaCollider = area.GetComponent<Collider>();
        if (areaCollider == null)
        {
            Debug.LogError("The spawn area must have a Collider component!");
            return Vector3.zero;
        }

        Bounds bounds = areaCollider.bounds;

        // Get a random point within the bounds
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);
        float randomZ = Random.Range(bounds.min.z, bounds.max.z);

        return new Vector3(randomX, randomY, randomZ);
    }

    private IEnumerator FloatUpwards(GameObject point)
    {
        float elapsedTime = 0f;

        Vector3 initialPosition = point.transform.position;
        float initialZ = initialPosition.z;

        int waveDirection = Random.Range(0, 2) == 0 ? -1 : 1; // Decides at random what range to take. Can only take a value 0 or 1. If 0 it will be minus and plus if 1
        while (elapsedTime < _timeUntilDestroyed)
        {

            float waveX = Mathf.Sin(elapsedTime * _waveFrequency) * _waveHeight * waveDirection; 
            float waveY = elapsedTime * _floatingSpeed;

            point.transform.position = new Vector3(initialPosition.x + waveX, initialPosition.y + waveY, initialZ);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Destroy(point);
    }
}
