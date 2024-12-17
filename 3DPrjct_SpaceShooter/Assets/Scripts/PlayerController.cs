using UnityEngine;

[System.Serializable]
public class Boundary
{   
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{


    private Rigidbody _rg;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _titlAngle = 45f;
    [SerializeField] private Boundary _boundary;
    void Start()
    {
        _rg = GetComponent<Rigidbody>();
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