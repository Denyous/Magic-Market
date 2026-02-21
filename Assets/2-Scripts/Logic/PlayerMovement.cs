using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _moveSpeed = 5f;

    [Header("Optional Settings")]
    [Tooltip("If true, movement will be frame-rate independent using FixedUpdate")]
    [SerializeField] private bool _useFixedUpdate = true;
    [Tooltip("If true, You need to set up directional animations")]
    [SerializeField] private bool _useAnimator = true;

    //Variables 
    private Rigidbody2D _rb;
    private Vector2 _moveInput;
    private Animator _animator;

    private void Awake()
    {
        
        _rb = GetComponent<Rigidbody2D>();
        
        if (_useAnimator)
        {
            _animator = GetComponent<Animator>();
        }
            

        
        if (_rb == null)
        {
            _rb = gameObject.AddComponent<Rigidbody2D>();
            _rb.gravityScale = 0f; 
        }
    }

    private void Update()
    {
        if (!_useFixedUpdate)
        {
            ApplyMovement();
        }
    }

    private void FixedUpdate()
    {
        if (_useFixedUpdate)
        {
            ApplyMovement();
        }
    }

    private void ApplyMovement()
    {
        _rb.linearVelocity = _moveSpeed * _moveInput;
    }

    public void Move(InputAction.CallbackContext callback)
    {
        if(_useAnimator)
        {
            _animator.SetBool("isWalking", true);


            if (callback.canceled)
            {
                _animator.SetBool("isWalking", false);
                _animator.SetFloat("LastInputX", _moveInput.x);
                _animator.SetFloat("LastInputY", _moveInput.y);
            }

            _moveInput = callback.ReadValue<Vector2>();

            _animator.SetFloat("InputX", _moveInput.x);
            _animator.SetFloat("InputY", _moveInput.y);


        }
        else
        {
            _moveInput = callback.ReadValue<Vector2>();
        }

    }

    // Opsiyonel: Script üzerinden hız değiştirme buff vs için kullanılabilir
    public void SetMoveSpeed(float newSpeed)
    {
        
        _moveSpeed = Mathf.Max(0f, newSpeed);
    }
}

