using UnityEngine;

public class Tree : MonoBehaviour,IBreakable
{
    [Header("Tree Settings")]
    [SerializeField] private GameObject _woodPrefab;
    public int _health { get; set; } = 3;

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
        if (_woodPrefab == null)
        {
            Debug.Log("Wood Prefab referans atanmamış");
        }
    }

    public void TakeDamage(Collider2D collision)
    {

        if (collision.gameObject.tag == "Tree")
        {

            if (_health != 0)
            {
               
                this._health--;
            }
            else
            {

                Destroy(collision.gameObject);

                for (int i = 0; i <= 3; i++)
                {
                    Instantiate(_woodPrefab, gameObject.transform.parent);
                }
            }
        }

    }

}
