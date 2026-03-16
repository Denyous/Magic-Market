using UnityEngine;

public class Gold : Resources, IClickable,IBreakable
{
    public int _health { get ; set ; }

    private void OnEnable()
    {
        ClickInputHandler.OnLeftMousePressed += TakeDamage;
    }
    private void OnDisable()
    {
        ClickInputHandler.OnLeftMousePressed -= TakeDamage;
    }

    private void Awake()
    {
        _health = 4;
    }
    public void OnClicked(int count)
    {
       
    }

    public void TakeDamage(Collider2D collision)
    {
        if (collision.gameObject != gameObject) return;

        _health--;

        if (_health <= 0)
        {
            Debug.Log(gameObject.name);
            DestroyObject();
            SpawnResource();
        }
    }

    override protected void SpawnResource()
    {
        base.SpawnResource();
        Debug.Log("Gold spawned");
    }


    override protected void DestroyObject()
    {
        base.DestroyObject();
        Debug.Log("Gold destroyed");
    }
}
