using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace HasteUp.Pooling
{
    public abstract class GenericPool<Poolable, PoolableData> : IPool<Poolable, PoolableData> where Poolable : IPoolable
        where PoolableData : IPoolableData
    {
        private PoolableData poolableData;
        private Queue<Poolable> inPool = new Queue<Poolable>();
        private List<Poolable> outOfPool = new List<Poolable>();
        private Transform poolTransform;
        private Transform outOfPoolTransform;
        private int counter = 0;

        public GenericPool()
        {
        }

        public virtual void Init(Transform factoryTransform, PoolableData poolableData)
        {
            this.poolableData = poolableData;

            outOfPoolTransform = new GameObject(poolableData.Tag).transform;
            outOfPoolTransform.transform.parent = factoryTransform;
            poolTransform = new GameObject(poolableData.Tag + "_pool").transform;
            poolTransform.transform.parent = outOfPoolTransform.transform;

            for (int i = 0; i < poolableData.StartPoolSize; i++)
            {
                var component = Instantiate();
                component.GameObject.SetActive(false);
                component.Transform.parent = poolTransform.transform;
                inPool.Enqueue(component);
            }
        }


        public virtual Poolable Get()
        {
            Poolable component;
            if (inPool.Count == 0)
            {
                component = Instantiate();
            }
            else
            {
                component = inPool.Dequeue();
            }

            component.Transform.parent = outOfPoolTransform;
            component.GameObject.SetActive(true);
            outOfPool.Add(component);
            return component;
        }

        public virtual void Add(Poolable component)
        {
            outOfPool.Remove(component);
            component.GameObject.SetActive(false);
            component.Transform.parent = poolTransform;
            inPool.Enqueue(component);
        }
        

        public void ResetPool()
        {
            foreach (Poolable poolable in outOfPool)
            {
                poolable.GameObject.SetActive(false);
                poolable.Transform.parent = poolTransform;
                inPool.Enqueue(poolable);
            }

            outOfPool.Clear();
        }


        public void Dispose()
        {
            GameObject.Destroy(poolTransform.gameObject);
            GameObject.Destroy(outOfPoolTransform.gameObject);
        }

        protected virtual Poolable Instantiate()
        {
            var go = GameObject.Instantiate(poolableData.Prefab);
            Poolable component = go.GetComponent<Poolable>();
            component.PoolableData = poolableData;
            component.PoolableID = ++counter;
            Assert.IsTrue(component != null);
            return component;
        }
    }
}