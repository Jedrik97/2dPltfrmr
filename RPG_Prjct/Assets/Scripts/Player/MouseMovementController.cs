using UnityEngine;
using UnityEngine.AI;

public class MouseMovementController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private Camera _camera;

    private Rigidbody _rb;
    private NavMeshAgent _agent;
    
    private Vector3 _targetPosition;
    private bool _isMoving;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        HandleMouseInput();
    }

    private void FixedUpdate()
    {
        if (_isMoving)
        {
          //  HandleMovement();
        }
    }

    private void HandleMovement()
    {
        Vector3 direction = (_targetPosition - transform.position).normalized;
        Vector3 velocity = direction * _moveSpeed;
        
        if (Vector3.Distance(transform.position, _targetPosition) < 0.1f)
        { 
            _isMoving = false;
            _rb.linearVelocity = Vector3.zero;
            return;
        }

       
        
        _rb.linearVelocity = new Vector3(velocity.x, _rb.linearVelocity.y, velocity.z); 
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        _rb.MoveRotation(Quaternion.Slerp(_rb.rotation, targetRotation, _moveSpeed * Time.deltaTime));
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                _agent.SetDestination(hit.point); 
                
                _isMoving = true;
            }
        }
    }
}