using UnityEngine;

public abstract class Resources : MonoBehaviour,ITimeListener
{
    [SerializeField] protected GameObject _resourcePrefab;

    protected virtual void SpawnResource()
    {
        for (int i = 0; i < 3; i++)
        {
            Instantiate(_resourcePrefab, transform.position, Quaternion.identity);
        }
           
    } 
    protected virtual void DestroyObject()
    {
       gameObject.SetActive(false);
    }

    public void OnTimeOfDayChanged(TimeOfDay newTime)
    {
       
    }
    public void OnDayCompleted(int dayNumber)
    {
      SpawnSourceResource();
    }

    private void SpawnSourceResource()
    {
        gameObject.SetActive(true);
    }

    
}
