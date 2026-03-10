using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickInputHandler : MonoBehaviour
{
    public static event Action<Collider2D> OnLeftMousePressed;
    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    public void OnClick(InputAction.CallbackContext callbackContext)
    {
        
        if (!callbackContext.started) return;

        var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (!rayHit.collider) return;

        Debug.Log("Hit: " + rayHit.collider.name);
        OnLeftMousePressed?.Invoke(rayHit.collider);
    }

}
