
using System.Collections.Generic;
using UnityEngine;

namespace Pooling
{
    [System.Serializable]
    public class PoolingSystem<T> where T : MonoBehaviour
    {
        private List<ObjectPool<T>> _pools = new List<ObjectPool<T>>();
        private Transform _container;
        public PoolingSystem(Transform container)
        {
            _container = container;
        }
        public void DeactivateAll()
        {
            foreach (var item in _pools)
            {
                item.DeactivateAll();
            }
        }
        public void ClearPools()
        {
            foreach (var item in _pools)
            {
                item.ClearPool();
            }
            _pools.Clear();
        }
        public T Get(T prefab)
        {
            T output;
            foreach (var item in _pools)
            {
                if (item.Prefab == prefab)
                {
                    output = item.GetFreeElement();
                    return output;
                }
            }
            var newPool = new ObjectPool<T>(prefab, 0, _container);
            _pools.Add(newPool);
            output = newPool.GetFreeElement();
            return output;
        }
    }

    [System.Serializable]
    public class ObjectPool<T> where T : MonoBehaviour
    {
        public T Prefab { get; }
        public bool AutoExpand { get; set; } = true;
        public Transform Container { get; }

        private List<T> _pool;

        public ObjectPool(T prefab, int count)
        {
            Prefab = prefab;
            Container = null;
            CreatePool(count);
        }

        public ObjectPool(T prefab, int count, Transform container)
        {
            Prefab = prefab;
            Container = container;
            CreatePool(count);
        }
        public void DeactivateAll()
        {
            for (int i = 0; i < _pool.Count; i++)
            {
                _pool[i].gameObject.SetActive(false);
            }
        }
        public void ClearPool()
        {
            for (int i = 0; i < _pool.Count; i++)
            {
                GameObject.Destroy(_pool[i].gameObject);
            }
            _pool.Clear();
        }
        private void CreatePool(int count)
        {
            _pool = new List<T>();

            for (int i = 0; i < count; i++)
            {
                CreateObject();
            }
        }

        private T CreateObject(bool isActiveByDef = false)
        {
            var spawned = Object.Instantiate(Prefab, Container);
            spawned.gameObject.SetActive(isActiveByDef);
            _pool.Add(spawned);
            return spawned;
        }

        public bool HasFreeElement(out T elem)
        {
            foreach (var item in _pool)
            {
                if (!item.gameObject.activeInHierarchy)
                {
                    elem = item;
                    item.gameObject.SetActive(true);
                    return true;
                }
            }
            elem = null;
            return false;
        }

        public T GetFreeElement()
        {
            if (HasFreeElement(out var elem))
            {
                return elem;
            }
            if (AutoExpand)
                return CreateObject(true);

            throw new System.Exception("BRUH NO ELEM&&&!&!&!& (((");
        }
    }
}