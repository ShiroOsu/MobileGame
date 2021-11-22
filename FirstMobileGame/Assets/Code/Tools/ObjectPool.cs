using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Tools
{
    public class ObjectPool
    {
        private readonly uint m_ExpandBy;
        private readonly GameObject m_Prefab;
        private readonly Transform m_Parent;
        private readonly Stack<GameObject> objects = new();

        public ObjectPool(uint initSize, GameObject prefab, Transform parent = null, uint expandBy = 1)
        {
            m_ExpandBy = expandBy < 1 ? 1 : expandBy;
            m_Prefab = prefab;
            m_Parent = parent;
            Expand(initSize < 1 ? 1 : initSize);
        }

        private void Expand(uint amount)
        {
            for (int i = 0; i < amount; i++)
            {
                var instance = Object.Instantiate(m_Prefab, m_Parent);
                instance.SetActive(false);

                var emitOnDisable = instance.AddComponent<EmitOnDisable>();
                emitOnDisable.OnDisableGameObject += UnRent;
                objects.Push(instance);
            }
        }

        private void UnRent(GameObject gameObject)
        {
            objects.Push(gameObject);
        }

        public GameObject Rent(bool activate)
        {
            if (objects.Count == 0)
            {
                Expand(m_ExpandBy);
            }

            var instance = objects.Pop();
            instance = instance != null ? instance : Rent(activate);
            instance.SetActive(activate);
            return instance;
        }
    }
}
