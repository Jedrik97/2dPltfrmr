using UnityEngine;

public class EnemyFrogController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float moveDistance = 2f;
    [SerializeField] private float jumpInterval = 2f;

    [Header("References")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Rigidbody2D _rigidbody2D;
    private bool _movingLeft = true;
    private float _groundCheckRadius = 0.5f;
    private float _nextJumpTime;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _nextJumpTime = Time.time + jumpInterval;
    }

    void Update()
    {
        if (Time.time >= _nextJumpTime && IsGrounded())
        {
            Jump();
            _nextJumpTime = Time.time + jumpInterval;
        }

        UpdateAnimationState();
    }

    private void Jump()
    {
        Vector2 jumpDirection = _movingLeft ? Vector2.left : Vector2.right;
        _rigidbody2D.linearVelocity = new Vector2(jumpDirection.x * moveDistance, jumpForce);
        
        FlipSprite(_movingLeft);

        _movingLeft = !_movingLeft;
        animator.Play("FrogJumpUp");
    }

    private void UpdateAnimationState()
    {
        if (!IsGrounded())
        {
            if (_rigidbody2D.linearVelocity.y > 0)
            {
                animator.Play("FrogJumpUp");
            }
            else if (_rigidbody2D.linearVelocity.y < 0)
            {
                animator.Play("FrogJumpFall");
            }
        }
        else
        {
            animator.Play("FrogIdle");
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, _groundCheckRadius, groundMask);
        return hit.collider != null;
    }

    private void FlipSprite(bool facingLeft)
    {
        spriteRenderer.flipX = facingLeft;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            if (collision.contacts[0].point.y > transform.position.y + 0.1f) 
            {
                Destroy(gameObject); 
            }
            else
            {
                
                PlayerHealthController playerHealth = collision.gameObject.GetComponent<PlayerHealthController>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(1);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyHead")) 
        {
            Destroy(gameObject); 
        }
    }
}
