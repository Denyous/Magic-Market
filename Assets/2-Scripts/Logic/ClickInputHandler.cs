using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class ClickInputHandler : MonoBehaviour
{
    public static ClickInputHandler Instance { get; private set; }
    public static event Action<Collider2D> OnLeftMousePressed;
    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }
    private void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void OnClick(InputAction.CallbackContext callbackContext)
    {
        
        if (!callbackContext.started) return;

        var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (!rayHit.collider) return;

        IClickable clickable = rayHit.collider.GetComponent<IClickable>();

        if (clickable == null) return;

        Debug.Log("Object Hit : " + rayHit.collider.gameObject.name);
        OnLeftMousePressed?.Invoke(rayHit.collider);
    }

}
