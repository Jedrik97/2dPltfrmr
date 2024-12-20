
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private List<GameObject> _enemyPrefab;
    private Transform _spawnPoint;
    [SerializeField] private Boundary _boundary;
    [SerializeField] private float _spawnInterval;
    private float _spawnTimer;
    private void Update()
    {
        if (Time.time >= _spawnTimer)
        {
            _spawnTimer += Time.deltaTime + _spawnInterval;
                Instantiate(_enemyPrefab[Random.Range(0, _enemyPrefab.Count)], SpawnPosition(), Quaternion.identity); 
        }
    }

    Vector3 SpawnPosition()
    {
        Vector3 position = new Vector3
        (
            Mathf.Clamp((float)Random.Range(-6,6), _boundary.xMin, _boundary.xMax),0.0f,18.0f
        );
        return position;
    }
}
