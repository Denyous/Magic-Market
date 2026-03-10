using UnityEngine;

public class Rock : MonoBehaviour,IBreakable
{
    public int _health { get; set; }

    private void OnEnable()
    {
        ClickInputHandler.OnLeftMousePressed += TakeDamage;
    }
    private void OnDisable()
    {
        ClickInputHandler.OnLeftMousePressed -= TakeDamage;
    }

    public void TakeDamage(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Rock")
        {
            Debug.Log("Rock hit!");
        }

    }

}
