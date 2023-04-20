using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HasteUp.Pooling
{
    public interface IPoolable
    {
        GameObject GameObject { get; }
        Transform Transform { get; }
        int PoolableID { get; set; }
        IPoolableData PoolableData { get; set; }
    }
    
}