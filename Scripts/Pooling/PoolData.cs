using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HasteUp.Pooling
{
    [CreateAssetMenu(menuName = "Data/PoolData", fileName = "PoolData", order = 0)]
    public class PoolData : ScriptableObject, IPoolableData, IEquatable<PoolData>
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private int startPoolSize = 10;
        [SerializeField] private Vector3 defaultScale = Vector3.one;
        [SerializeField, HideInInspector] private string poolType;
        [SerializeField, HideInInspector] private string poolTypeAssembly;

        public string Tag => name;

        public GameObject Prefab => prefab;

        public int StartPoolSize => startPoolSize;

        public Vector3 DefaultScale => defaultScale;

        public string PoolType
        {
            get => poolType;
            set => poolType = value;
        }

        public string PoolTypeAssembly { get; set; }

        public bool Equals(PoolData other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && name == other.name;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PoolData)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), name);
        }
    }
}