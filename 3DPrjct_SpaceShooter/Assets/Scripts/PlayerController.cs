using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

[System.Serializable]
public class Boundary
{   
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject _boltPrefab;
    [SerializeField] private Transform _boltSpawn;
    [SerializeField] private float _fireRate = 0.5f;
    [SerializeField] private int _poolSize = 20;   
    [SerializeField] private float _speed = 5f;    
    [SerializeField] private float _tiltAngle = 45f;
    [SerializeField] private Boundary _boundary;  

    private float _nextFire; 
    private Rigidbody _rg;   

    private Queue<GameObject> _boltPool; 
    void Start()
    {
        _rg = GetComponent<Rigidbody>();
        
        InitializePool();
        
    }

    void Update()
    {
        HandleShooting();
    }
    void FixedUpdate()
    {
        HandleMovement();
    }
    private void InitializePool()
    {
        _boltPool = new Queue<GameObject>();
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject bolt = Instantiate(_boltPrefab);
            bolt.SetActive(false); 
            _boltPool.Enqueue(bolt);
        }
    }
  
    private void HandleShooting()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= _nextFire)
        {
            _nextFire = Time.time + _fireRate;

            GameObject bolt = GetPooledBolt(); 
            bolt.transform.position = _boltSpawn.position; 
            bolt.transform.rotation = _boltSpawn.rotation; 
            bolt.SetActive(true);
                
            DestroyByContact destroyByContact = bolt.GetComponent<DestroyByContact>();
            if (destroyByContact != null)
            {
                destroyByContact.OnReturnToPool = ReturnBoltToPool; 
            }
        }
    
    }
    private GameObject GetPooledBolt()
    {
        if (_boltPool.Count > 0) 
        {
            return _boltPool.Dequeue();
        }
        else 
        {
            GameObject bolt = Instantiate(_boltPrefab);
            bolt.SetActive(false);
            return bolt;
        }
    }
    public void ReturnBoltToPool(GameObject bolt)
    {
        bolt.SetActive(false); 
        _boltPool.Enqueue(bolt); 
    }
    private void HandleMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        _rg.linearVelocity = movement * _speed;

        _rg.position = new Vector3
        (
            Mathf.Clamp(_rg.position.x, _boundary.xMin, _boundary.xMax),
            0.0f,
            Mathf.Clamp(_rg.position.z, _boundary.zMin, _boundary.zMax)
        );
        _rg.rotation = Quaternion.Euler(0f, 0f, _tiltAngle * -moveHorizontal);
    }
}
    