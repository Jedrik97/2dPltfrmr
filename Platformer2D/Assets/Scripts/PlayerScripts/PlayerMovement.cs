using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _defaultSpeed = 5f;
    [SerializeField] private float _jumpForce = 5f;

    private float _currentSpeed;
    private float _maxSpeed = 8f;

    [Header("Ground Check")]
    [SerializeField] private float _groundCheckRadius = 0.5f;
    [SerializeField] private LayerMask _groundMask;

    private Rigidbody2D _rigidBody2D;
    private bool _canDoubleJump;

    private void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _currentSpeed = PlayerPrefs.GetFloat("PlayerSpeed", _defaultSpeed);
    }

    private void OnEnable()
    {
      
        InputController._horizontalInputAction += Move;
        InputController._verticalInputAction += Jump;
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        _rigidBody2D.linearVelocity = new Vector2(horizontalInput * _currentSpeed, _rigidBody2D.linearVelocity.y);
    }

    private void Jump()
    {
        if (IsGrounded())
        {
            _rigidBody2D.linearVelocity = new Vector2(_rigidBody2D.linearVelocity.x, _jumpForce);
            _canDoubleJump = true;
        }
        else if (_canDoubleJump)
        {
            _rigidBody2D.linearVelocity = new Vector2(_rigidBody2D.linearVelocity.x, _jumpForce);
            _canDoubleJump = false;
        }
    }

    public bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, _groundCheckRadius, _groundMask);
        return hit.collider != null;
    }

    public void IncreaseSpeed(float amount)
    {
        _currentSpeed = Mathf.Min(_currentSpeed + amount, _maxSpeed);
    }

    public float GetCurrentSpeed() => _currentSpeed;

    public void SetCurrentSpeed(float speed)
    {
        _currentSpeed = Mathf.Clamp(speed, _defaultSpeed, _maxSpeed);
    }
    private void OnDisable()
    {
     
        InputController._horizontalInputAction -= Move;
        InputController._verticalInputAction -= Jump;
    }
}
