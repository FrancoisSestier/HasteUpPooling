using System;
using Unity.VisualScripting;
using UnityEngine;

namespace HasteUp.Pooling
{
    public interface IPool<Poolable, PoolableData> : IDisposable where Poolable : IPoolable
        where PoolableData : IPoolableData
    {
        public void Init(Transform factoryTransform, PoolableData poolableData);

        public Poolable Get();

        public void Add(Poolable poolable);

        public void ResetPool();
    }
}