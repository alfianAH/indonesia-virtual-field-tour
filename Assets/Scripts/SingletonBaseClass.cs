using UnityEngine;

public class SingletonBaseClass<T>: MonoBehaviour where T: MonoBehaviour 
{
    private static T instance;
    private const string LOG = nameof(T);

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.Log($"Find {LOG}");
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    Debug.LogError($"{LOG} tidak ditemukan");
                }
            }
            
            Debug.Log($"Get {LOG}");

            return instance;
        }
    }
}