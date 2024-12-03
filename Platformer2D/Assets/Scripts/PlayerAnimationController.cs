using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [Header("Components")]
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private PlayerMovement _playerMovement;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        HandleFlip();
        UpdateAnimationState();
    }

    private void HandleFlip()
    {
        if (!_playerMovement.IsGrounded() && _playerMovement.IsFalling())
            return; 

        _spriteRenderer.flipX = !_playerMovement.IsFacingRight();
    }

    private void UpdateAnimationState()
    {
        if (!_playerMovement.IsGrounded())
        {
            if (_playerMovement.IsJumpingUp())
            {
                _animator.Play("PlayerJumpUp");
            }
            else if (_playerMovement.IsFalling())
            {
                _animator.Play("PlayerJumpFall");
            }
        }
        else if (_playerMovement.IsMoving())
        {
            _animator.Play("PlayerRun");
        }
        else
        {
            _animator.Play("PlayerIdle");
        }
    }
}