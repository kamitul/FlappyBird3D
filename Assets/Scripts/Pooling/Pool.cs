using System.Collections.Generic;
using UnityEngine;

namespace Pooling
{
    public class Pool<T>
        where T : MonoBehaviour, IPoolable
    {
        private readonly Transform parentTransform;
        private readonly T pooledPrefab;
        private readonly List<T> pooledObjects = new List<T>();

        public Pool(T pooledObj, int initialPoolSize, Transform parent)
        {
            parentTransform = parent;
            pooledObjects = new List<T>();

            for (int i = 0; i < initialPoolSize; i++)
            {
                SpawnObj(pooledObj);
            }
            pooledPrefab = pooledObj;
        }

        private void SpawnObj(T pooledObj)
        {
            var nObj = Object.Instantiate(pooledObj.gameObject, Vector3.zero, Quaternion.identity, parentTransform);
            nObj.SetActive(false);
            pooledObjects.Add(nObj.GetComponent<T>());
        }

        public T GetObject(bool shouldActivateObject)
        {
            var pooledObject = pooledObjects.Find(x => x.IsTaken == false);

            if (pooledObject == null)
            {
                GrowPool(pooledObjects.Count);
                pooledObject = pooledObjects.Find(x => x.IsTaken == false);
            }

            if (shouldActivateObject) pooledObject.gameObject.SetActive(true);
            pooledObject.Acquire();
            return pooledObject;
        }

        private void GrowPool(int count)
        {
            for (int i = 0; i < count; i++)
            {
                SpawnObj(pooledPrefab);
            }
        }

        public void DisableAllObjects()
        {
            pooledObjects.ForEach(item =>
            {
                item.Release();
                item.gameObject.SetActive(false);
                item.transform.SetParent(parentTransform);
            });
        }

        public void ClearObjects()
        {
            pooledObjects.ForEach(x => Object.Destroy(x.gameObject));
            pooledObjects.Clear();
        }
    }
}


