using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionDetector : MonoBehaviour
{
    private IInteractable closestInteractable = null;
    
    public void OnInteract(InputAction.CallbackContext context)
    {
        closestInteractable?.Interact();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable) && interactable.CanInteract())
        {
            Debug.Log("Interactable Detected: " + collision.gameObject.name);
            closestInteractable = interactable;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable) && interactable == closestInteractable)
        {
            closestInteractable = null;
        }
    }

}
