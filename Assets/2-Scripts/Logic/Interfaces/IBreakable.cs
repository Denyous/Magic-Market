using UnityEngine;

public interface IBreakable 
{
    public int _health { get; set; }

    public void TakeDamage(Collider2D collision);
}
