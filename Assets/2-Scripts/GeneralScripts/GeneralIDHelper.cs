using UnityEngine;

public static class GeneralIDHelper 
{
    public static string GenerateUniqueID(GameObject gameObject)
    {
        return $"{gameObject.name}_{gameObject.transform.position.x}_{gameObject.transform.position.y}";
    }

}
