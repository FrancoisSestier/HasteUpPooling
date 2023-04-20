using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Experimental.GlobalIllumination;

namespace HasteUp.Pooling
{
    public interface IFactory<PooledComponent, PoolDataType>
        where PooledComponent : IPoolable where PoolDataType : IPoolableData
    {
        public PooledComponent Spawn(PoolDataType poolData, Vector3 position, Quaternion rotation, Vector3 scale)
        {
            return Spawn(poolData.Tag, position, rotation, scale);
        }

        public PooledComponent Spawn(PoolDataType poolData, Vector3 position, Quaternion rotation)
        {
            return Spawn(poolData.Tag, position, rotation, poolData.DefaultScale);
        }

        public PooledComponent Spawn(PoolDataType poolData, Vector3 position)
        {
            return Spawn(poolData.Tag, position, Quaternion.identity, poolData.DefaultScale);
        }


        public PooledComponent Spawn(PoolDataType poolData, Vector3 position,
            float despawnAfter)
        {
            var comp = Spawn(poolData, position);
            DeSpawnAfter(comp, despawnAfter);
            return comp;
        }
        
        public PooledComponent Spawn(PoolDataType poolData, Vector3 position, Quaternion rotation,
            float despawnAfter)
        {
            var comp = Spawn(poolData, position, rotation);
            DeSpawnAfter(comp, despawnAfter);
            return comp;
        }
        
        public PooledComponent Spawn(PoolDataType poolData, Vector3 position, Quaternion rotation,
            Vector3 scale,
            float despawnAfter)
        {
            var comp = Spawn(poolData, position, rotation, scale);
            DeSpawnAfter(comp, despawnAfter);
            return comp;
        }

        public PooledComponent Spawn(PoolDataType poolData, Transform parent)
        {
            return Spawn(poolData.Tag, parent);
        }

        public virtual PooledComponent Spawn(string tag, Vector3 position, Quaternion rotation, Vector3 scale,
            float despawnAfter)
        {
            var comp = Spawn(tag, position, rotation, scale);
            DeSpawnAfter(comp, despawnAfter);
            return comp;
        }

        public PooledComponent Spawn(PoolDataType poolData, Transform parent, float despanwAfter)
        {
            var comp = Spawn(poolData, parent);
            DeSpawnAfter(comp, despanwAfter);
            return comp;
        }

        public T Spawn<T>(PoolDataType poolData, Vector3 position,
            float despawnAfter) where T : PooledComponent
        {
            return (T)Spawn(poolData, position, despawnAfter);
        }

        public PooledComponent Spawn<T>(PoolDataType poolData, Vector3 position, Quaternion rotation,
            float despawnAfter) where T : PooledComponent
        {
            return (T)Spawn(poolData, position, rotation, despawnAfter);
        }

        public T Spawn<T>(PoolDataType poolData, Vector3 position, Quaternion rotation,
            Vector3 scale,
            float despawnAfter) where T : PooledComponent
        {
            return (T)Spawn(poolData, position, rotation, scale, despawnAfter);
        }

        public T Spawn<T>(PoolDataType poolData, Transform parent, float despanwAfter) where T : PooledComponent
        {
            return (T)Spawn(poolData, parent, despanwAfter);
        }

        public PooledComponent Spawn<T>(PoolDataType poolData, Transform parent) where T : PooledComponent
        {
            return (T)Spawn(poolData, parent);
        }

        public T Spawn<T>(string tag, Vector3 position, Quaternion rotation, Vector3 scale) where T : PooledComponent
        {
            return (T)Spawn(tag, position, rotation, scale);
        }

        public T Spawn<T>(string tag, Transform parent) where T : PooledComponent
        {
            return (T)Spawn(tag, parent);
        }


        public PooledComponent Spawn(string tag, Vector3 position, Quaternion rotation, Vector3 scale);

        public PooledComponent Spawn(string tag, Transform parent);


        public void DeSpawnAfter(PooledComponent component, float delay);

        public void DeSpawn(PooledComponent component);
    }
}