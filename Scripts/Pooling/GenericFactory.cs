using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Experimental.GlobalIllumination;

namespace HasteUp.Pooling
{
    public abstract class GenericFactory<PooledComponent, PoolDataType, PoolType> : MonoBehaviour,
        IFactory<PooledComponent, PoolDataType>
        where PooledComponent : IPoolable
        where PoolDataType : IPoolableData
        where PoolType : IPool<PooledComponent, PoolDataType>, new()
    {
        [SerializeField] private List<PoolDataType> poolDatas;
        [SerializeField] private bool createOnAwake = false;
        private Dictionary<string, PoolType> pools;
        private List<ObjectTimeStamp> toRemove = new List<ObjectTimeStamp>();

        private class ObjectTimeStamp
        {
            public readonly PooledComponent pooledComponent;
            public readonly float lifeTime;
            public readonly float spawnTime;

            public ObjectTimeStamp(PooledComponent pooledComponent, float spawnTime, float lifeTime)
            {
                this.pooledComponent = pooledComponent;
                this.lifeTime = lifeTime;
                this.spawnTime = spawnTime;
            }
        }

        public List<PoolDataType> PoolDatas
        {
            get => poolDatas;
            set => poolDatas = value;
        }

        private void Awake()
        {
            if (createOnAwake)
            {
                CreatePools();
            }
        }

        public void ResetPools()
        {
            foreach (PoolType pool in pools.Values)
            {
                pool.ResetPool();
            }
        }

        public void CreatePools()
        {
            pools = new Dictionary<string, PoolType>();
            foreach (var poolData in PoolDatas)
            {
                PoolType pool = new PoolType();
                pool.Init(transform, poolData);
                pools.Add(poolData.Tag, pool);
            }
        }

        public void Update()
        {
            toRemove.RemoveAll((match) =>
            {
                if (match.spawnTime + match.lifeTime <= Time.time)
                {
                    DeSpawn(match.pooledComponent);
                    return true;
                }

                return false;
            });
        }

        public virtual PooledComponent Spawn(string tag, Vector3 position, Quaternion rotation, Vector3 scale)
        {
            Assert.IsTrue(pools.ContainsKey(tag), tag);
            PooledComponent component = pools[tag].Get();
            component.Transform.position = position;
            component.Transform.rotation = rotation;
            component.Transform.localScale = scale;
            return component;
        }

        public virtual PooledComponent Spawn(string tag, Transform parent)
        {
            Assert.IsTrue(pools.ContainsKey(tag), tag);
            PooledComponent component = pools[tag].Get();
            component.Transform.parent = parent;
            return component;
        }


        public virtual void DeSpawnAfter(PooledComponent component, float delay)
        {
            toRemove.Add(new ObjectTimeStamp(component, Time.time, delay));
        }

        public virtual void DeSpawn(PooledComponent component)
        {
            Assert.IsTrue(pools.ContainsKey(component.PoolableData.Tag));
            pools[component.PoolableData.Tag].Add(component);
        }
    };
}