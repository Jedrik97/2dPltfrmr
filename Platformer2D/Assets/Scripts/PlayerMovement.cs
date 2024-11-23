using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float _speed;
    private SpriteRenderer _sprite;
    private Rigidbody2D _rigidbody2D;
    public float _jumpForce;
    private void Start()
    {
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
 
    void Update()
    {

        Move();
        Jump();
    }
 
    void Move()
    {
        Vector3 movement = Vector3.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, movement + transform.position, _speed * Time.deltaTime);

        if (movement.x < 0)
        {
            _sprite.flipX = true;
            
        }
        else if (movement.x > 0)
        {
            _sprite.flipX = false;
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }
}
   
