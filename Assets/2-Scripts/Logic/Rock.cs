using UnityEngine;

public class Rock : Resources, IBreakable
{
    public int _health { get; set; }

    private void Awake()
    {
        _health = 2;
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
        Debug.Log("Stone spawned");
    }

    override protected void DestroyObject()
    {
        base.DestroyObject();
        Debug.Log("Rock destroyed");
    }

    private void OnEnable()
    {
        ClickInputHandler.OnLeftMousePressed += TakeDamage;
    }
    private void OnDisable()
    {
        ClickInputHandler.OnLeftMousePressed -= TakeDamage;
    }



}
