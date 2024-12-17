using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    Rigidbody _rigidbody;
    [SerializeField] private float _rotationSpeed = 2f;
    [SerializeField] private float _speed = 5f;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.linearVelocity = Vector3.back * _speed;
        _rigidbody.angularVelocity = Random.insideUnitSphere * _rotationSpeed;
    }
    
}
