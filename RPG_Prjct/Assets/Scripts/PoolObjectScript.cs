using UnityEngine;
using System.Collections.Generic;

public class PoolObjectScript : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform spawnPoint;   
    [SerializeField] private int _poolSize = 20;      
    [SerializeField] private float _shootingLength = 15f;
    [SerializeField] private float _fireRate = 0.5f;
    [SerializeField] private List<LayerMask> targetMasks;
    [SerializeField] private float _speed = 15f;
    
    private float _nextFire;
    private Queue<GameObject> _bulletPool;         

    private void Awake()
    {
        _bulletPool = new Queue<GameObject>();
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab); 
            bullet.SetActive(false);
            
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.Initialize(this, targetMasks, _shootingLength);
            }

            _bulletPool.Enqueue(bullet);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1)) 
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (_bulletPool.Count > 0 && Time.time >= _nextFire) 
        {
            _nextFire = Time.time + _fireRate;

            GameObject bullet = _bulletPool.Dequeue();
            bullet.transform.position = spawnPoint.position; 
            bullet.transform.rotation = spawnPoint.rotation; 
            bullet.SetActive(true); 

            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.linearVelocity = Vector3.zero;            // Сброс скорости
            rb.angularVelocity = Vector3.zero;    // Сброс вращения
            rb.linearVelocity = spawnPoint.forward * _speed;
        }
    }

    public void ReturnToPool(GameObject bullet)
    {
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        bullet.SetActive(false);
        _bulletPool.Enqueue(bullet);
    }
}
