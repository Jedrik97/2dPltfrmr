using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [Header("Components")]
    private Animator _animator;
    private PlayerMovement _playerMovement;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        Flip();
        UpdateAnimationState();
    }

    private void Flip()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput > 0)
        {
            _spriteRenderer.flipX = false; 
        }
        else if (horizontalInput < 0)
        {
            _spriteRenderer.flipX = true;
        }
    }

    private void UpdateAnimationState()
    {
        float verticalVelocity = _playerMovement.GetComponent<Rigidbody2D>().linearVelocity.y;
        float horizontalInput = Mathf.Abs(Input.GetAxis("Horizontal"));

        if (!_playerMovement.IsGrounded())
        {
            if (verticalVelocity > 0)
            {
                _animator.Play("PlayerJumpUp");
            }
            else if (verticalVelocity < 0)
            {
                _animator.Play("PlayerJumpFall");
            }
        }
        else if (horizontalInput > 0.1f) 
        {
            _animator.Play("PlayerRun");
        }
        else
        {
            _animator.Play("PlayerIdle");
        }
    }
}