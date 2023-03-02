using UnityEngine;

namespace Utils
{
    [DisallowMultipleComponent()]
    public class Singleton<T> : MonoBehaviour 
        where T : MonoBehaviour
    {
        private static bool isShuttingDown;
        private static object @lock = new object();
        private static T instance;

        public static T Instance
        {
            get
            {
                if (isShuttingDown)
                {
                    return null;
                }

                lock (@lock)
                {
                    if (instance == null)
                    {
                        instance = (T)FindObjectOfType(typeof(T));

                        if (instance == null)
                        {
                            var singletonObject = new GameObject();
                            instance = singletonObject.AddComponent<T>();
                            singletonObject.name = typeof(T) + " (Singleton)";

                            DontDestroyOnLoad(singletonObject);
                        }
                    }

                    return instance;
                }
            }
        }

        protected virtual void Awake()
        {
            isShuttingDown = false;
        }

        private void OnApplicationQuit()
        {
            isShuttingDown = true;
        }

        private void OnDestroy()
        {
            isShuttingDown = true;
        }
    }
}
