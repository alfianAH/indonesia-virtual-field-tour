using UnityEngine;

public class SingletonBaseClass<T>: MonoBehaviour where T: MonoBehaviour 
{
    private static T instance;

    public static T Instance
    {
        get
        {
            string log = typeof(T).Name;

            if (instance != null) return instance;
            
            instance = FindObjectOfType<T>();
            if (instance == null)
            {
                Debug.LogError($"{log} tidak ditemukan");
            }

            return instance;
        }
    }
}