using UnityEngine;

public class Tree : MonoBehaviour
{

    [SerializeField] private int _treeHealth = 3;
    [SerializeField] private GameObject _woodPrefab;

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
       
                   
            if (_treeHealth != 0)
            {
                Debug.Log("Tree hit!");
                this._treeHealth--;
            }
            else
            {
               
                Destroy(collision.gameObject);

                for (int i = 0; i <= 3; i++)
                {
                    Instantiate(_woodPrefab,gameObject.transform.parent);
                }
            }
        
    }

}
