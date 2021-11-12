using UnityEngine;

namespace Code.Tools
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance) 
                    return instance;

                return instance = (new GameObject {name = nameof(T), hideFlags = HideFlags.HideAndDontSave}).AddComponent<T>();
            }
        }

        private void OnDestroy()
        {
            if (instance == this) { instance = null; }
        }
    }
}