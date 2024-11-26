using UnityEngine;
// ReSharper disable All
public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _speed= 5f;
    [SerializeField] private float _jumpForce = 5f;

    [Header("Ground Check")]
    [SerializeField] private float _groundCheckRadius = 0.7f;
    [SerializeField] private LayerMask _groundMask;

    [Header("Components")]
    private Rigidbody2D _rigidBody2D;
    private FlipController _flipController;

    private bool _canDoubleJump = false;

    public static Player Instance;
    private void OnEnable()
    {
        InputController._horizontalInputAction += Move;
    }
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _flipController = GetComponent<FlipController>();
    }
    private void Update()
    {
        if (IsGrounded())
        {
            _canDoubleJump = true;
        }
    }
    internal void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        _rigidBody2D.linearVelocity = new Vector2(horizontal * _speed, _rigidBody2D.linearVelocity.y);
        
       _flipController.FlipX(_rigidBody2D.linearVelocity.x);
    }
    internal void Jump()
    {
        if (IsGrounded())
        {
            _rigidBody2D.linearVelocity = new Vector2(_rigidBody2D.linearVelocity.x, _jumpForce);
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
       
        return hit.collider;
    }
    private void OnDisable()
    {
        InputController._horizontalInputAction -= Move;
    }
}