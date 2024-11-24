using UnityEngine;

    public class Player : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _jumpForce = 5f;
        [SerializeField] private bool _groundCheck;
        
        public SpriteRenderer _sprite;
        private Rigidbody2D _rigidBody2D; 
        Ray _ray;
        [SerializeField] float _groundCheckRadius = 0.7f;
        [SerializeField] private LayerMask _mask;

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
            _rigidBody2D.GetComponentInChildren<SpriteRenderer>();
        }
        private void Update()
        {
            CheckGround();
        }
        
        internal void Move()
        {
            Vector3 movement = Vector3.right * Input.GetAxis("Horizontal");
            transform.position = Vector3.MoveTowards(transform.position, movement + transform.position, _speed * Time.deltaTime);

            if (movement.x < 0)
            {ы
                _sprite.flipX = true;
            
            }
            else if (movement.x > 0)
            {
                _sprite.flipX = false;
            }
        }

        internal void Jump()
        {
            _groundCheck = false;
            _rigidBody2D.linearVelocity = new Vector2(_rigidBody2D.linearVelocity.x, _jumpForce);
        }

        public bool CheckGround()
        {
            var hit = Physics2D.Raycast(transform.position, Vector2.down, _groundCheckRadius, _mask);
            Debug.DrawRay(transform.position, Vector2.down * _groundCheckRadius, Color.red);
            return hit.collider != null;
        }

        private void OnDisable()
        {
            InputController._horizontalInputAction -= Move;
        }
    }