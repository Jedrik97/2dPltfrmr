using UnityEngine;

public class BoltController : MonoBehaviour
{
  
    Rigidbody _rigidbody;
    [SerializeField] private float _speed = 8f;
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.linearVelocity = Vector3.forward * _speed;
    }

    
    void Update()
    {
        
    }
}
