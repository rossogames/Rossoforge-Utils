using Rossoforge.Utils.Logger;
using UnityEngine;

namespace Rossoforge.Utils.Singleton
{
    public abstract class SingletonComponent<T> : MonoBehaviour where T : SingletonComponent<T>
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindFirstObjectByType<T>();
                    if (_instance == null)
                        RossoLogger.Error($"Singleton of type {typeof(T).Name} not found in scene.");
                }

                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else if (_instance != this)
            {
                RossoLogger.Warning($"Duplicate singleton {typeof(T).Name} detected. Destroying new instance.");
                Destroy(gameObject);
            }
        }

        protected virtual void OnDestroy()
        {
            if (_instance == this)
                _instance = null;
        }
    }
}