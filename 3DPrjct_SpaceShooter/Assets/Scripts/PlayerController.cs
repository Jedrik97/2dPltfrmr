using UnityEngine;

[System.Serializable]
public class Boundary
{   
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject _shotPrefab;
    [SerializeField] private Transform _shotSpawn;
    [SerializeField] private float _fareRate = 0.5f;
    private float _nextFire; 
    private Rigidbody _rg;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _titlAngle = 45f;
    [SerializeField] private Boundary _boundary;
    void Start()
    {
        _rg = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= _nextFire)
        {
            _nextFire = Time.time + _fareRate;
            Instantiate(_shotPrefab, _shotSpawn.position, _shotSpawn.rotation);  
        }
    }
    void FixedUpdate()
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
        
        _rg.rotation = Quaternion.Euler(0f, 0f, _titlAngle * -moveHorizontal);
    }
}