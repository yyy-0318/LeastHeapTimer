using System;
using UnityEngine;

public abstract class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    private static object _lock = new object();

    public static T Instance
    {
        get
        {
            if (_applicationIsQuitting)
            {               
                return null;
            }

            lock (_lock)
            {
                if (_instance != null)
                    return _instance;

                _instance = (T)FindObjectOfType(typeof(T));

                if (FindObjectsOfType(typeof(T)).Length > 1)
                {                
                    return _instance;
                }

                if (_instance != null)
                    return _instance;

                var singleton = new GameObject();
                _instance = singleton.AddComponent<T>();
                singleton.name = "(singleton) " + typeof(T).ToString();
           
                return _instance;
            }
        }
    }

    private static bool _applicationIsQuitting = false;

    private void Awake()
    {
        if (Instance == this)
        {
            DontDestroyOnLoad(transform.gameObject);
            InitInstance();
        }
        else
            Destroy(this.gameObject);
    }

    /// <summary>
    /// When Unity quits, it destroys objects in a random order.
    /// In principle, a Singleton is only destroyed when application quits.
    /// If any script calls Instance after it have been destroyed, 
    ///   it will create a buggy ghost object that will stay on the Editor scene
    ///   even after stopping playing the Application. Really bad!
    /// So, this was made to be sure we're not creating that buggy ghost object.
    /// </summary>
    public virtual void OnDestroy()
    {
        if (_instance == this)
            _applicationIsQuitting = true;
    }

    public virtual void InitInstance()
    {
    }
}