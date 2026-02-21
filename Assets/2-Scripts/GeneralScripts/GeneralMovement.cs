using UnityEngine;
using UnityEngine.InputSystem;

public class GeneralMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _moveSpeed = 5f;

    [Header("Optional Settings")]
    [Tooltip("If true, movement will be frame-rate independent using FixedUpdate")]
    [SerializeField] private bool _useFixedUpdate = true;
    [Tooltip("If true, You need to set up directional animations")]
    [SerializeField] private bool _useAnimator = true;

    [Header("Input Settings")]
    [Tooltip("Input Action Asset to use (leave empty for default)")]
    [SerializeField] private InputActionAsset _inputActionAsset;
    [Tooltip("Action Map name in the Input Action Asset")]
    [SerializeField] private string _actionMapName = "Player";
    [Tooltip("Move action name")]
    [SerializeField] private string _moveActionName = "Move";

    //Variables
    private Rigidbody2D _rb;
    private Vector2 _moveInput;
    private Animator _animator;
    private PlayerInput _playerInput;
    private InputAction _moveAction;

    private void Awake()
    {
        SetupRigidbody();
        SetupAnimator();
        SetupPlayerInput();
    }

    private void OnEnable()
    {
        
        if (_moveAction != null)
        {
            _moveAction.performed += OnMove;
            _moveAction.canceled += OnMove;
            _moveAction.Enable();
        }
    }

    private void OnDisable()
    {
        
        if (_moveAction != null)
        {
            _moveAction.performed -= OnMove;
            _moveAction.canceled -= OnMove;
            _moveAction.Disable();
        }
    }

    private void SetupRigidbody()
    {
        _rb = GetComponent<Rigidbody2D>();

        if (_rb == null)
        {
            _rb = gameObject.AddComponent<Rigidbody2D>();
            _rb.gravityScale = 0f;
            Debug.Log($"Rigidbody2D automatically added to {gameObject.name}");
        }
    }

    private void SetupAnimator()
    {
        if (_useAnimator)
        {
            _animator = GetComponent<Animator>();
            if (_animator == null)
            {
                Debug.LogWarning($"Animator component not found on {gameObject.name}. Animation will be disabled.");
                _useAnimator = false;
            }
        }
    }

    private void SetupPlayerInput()
    {
        _playerInput = GetComponent<PlayerInput>();

        
        if (_playerInput == null)
        {
            _playerInput = gameObject.AddComponent<PlayerInput>();
            Debug.Log($"PlayerInput automatically added to {gameObject.name}");
        }

       
        if (_inputActionAsset != null)
        {
            _playerInput.actions = _inputActionAsset;
        }

        
        _playerInput.notificationBehavior = PlayerNotifications.InvokeUnityEvents;

        
        if (_playerInput.actions != null)
        {
            _moveAction = _playerInput.actions.FindAction(_moveActionName);

            if (_moveAction == null)
            {
                Debug.LogError($"Move action '{_moveActionName}' not found in action map '{_actionMapName}'!");
            }
        }
        else
        {
            Debug.LogWarning($"No Input Action Asset assigned to PlayerInput on {gameObject.name}. Please assign one manually.");
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

    private void OnMove(InputAction.CallbackContext callback)
    {
        if (_useAnimator && _animator != null)
        {
            _animator.SetBool("isWalking", !callback.canceled);

            if (callback.canceled)
            {
                _animator.SetFloat("LastInputX", _moveInput.x);
                _animator.SetFloat("LastInputY", _moveInput.y);
            }
            else
            {
                _moveInput = callback.ReadValue<Vector2>();
                _animator.SetFloat("InputX", _moveInput.x);
                _animator.SetFloat("InputY", _moveInput.y);
            }
        }
        else
        {
            _moveInput = callback.ReadValue<Vector2>();
            if (callback.canceled)
            {
                _moveInput = Vector2.zero;
            }
        }
    }

    // Public API - Eski callback methodunu da destekle (backward compatibility)
    public void Move(InputAction.CallbackContext callback)
    {
        OnMove(callback);
    }

    public void SetMoveSpeed(float newSpeed)
    {
        _moveSpeed = Mathf.Max(0f, newSpeed);
    }
}

