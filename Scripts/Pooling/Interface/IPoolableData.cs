using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HasteUp.Pooling
{
    public interface IPoolableData : IEquatable<PoolData>
    {
        string Tag { get; }

        GameObject Prefab { get; }

        int StartPoolSize { get; }

        Vector3 DefaultScale { get; }

        string PoolType { get; set; }

        string PoolTypeAssembly { get; set; }
    }
}