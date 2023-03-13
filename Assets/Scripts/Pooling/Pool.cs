using System;
using UnityEngine;

namespace Pooling
{
    public class Pool<T>
        where T : MonoBehaviour, IPoolable
    {
        private readonly Transform parentTransform;
        private readonly T[] pooledObjects;

        public Pool(T pooledObj, int initialPoolSize, Transform parent)
        {
            parentTransform = parent;
            pooledObjects = new T[initialPoolSize];

            for (int i = 0; i < initialPoolSize; i++)
            {
                SpawnObj(pooledObj, i);
            }
        }

        private void SpawnObj(T pooledObj, int index)
        {
            var nObj = UnityEngine.Object.Instantiate(pooledObj.gameObject, Vector3.zero, Quaternion.identity, parentTransform);
            nObj.SetActive(false);
            pooledObjects[index] = nObj.GetComponent<T>();
        }

        public T GetObject(bool shouldActivateObject)
        {
            var pooledObject = Array.Find(pooledObjects, x => x.IsTaken == false);

            if (pooledObject == null)
            {
                Debug.LogError("There is no not taken object!");
                return null;
            }

            pooledObject.gameObject.SetActive(shouldActivateObject);
            pooledObject.Acquire();
            return pooledObject;
        }

        public void DisableAllObjects()
        {
            for(int i = 0; i < pooledObjects.Length; ++i)
            {
                pooledObjects[i].Release();
                pooledObjects[i].gameObject.SetActive(false);
                pooledObjects[i].transform.SetParent(parentTransform);
            }
        }

        public void ClearObjects()
        {
            for (int i = 0; i < pooledObjects.Length; ++i)
                UnityEngine.Object.Destroy(pooledObjects[i].gameObject);
        }
    }
}


