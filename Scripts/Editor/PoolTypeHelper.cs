using System;
using System.Collections.Generic;
using System.Linq;
using HasteUp.Reflection;
using UnityEditor;
using UnityEngine;

namespace HasteUp.Pooling
{
    [InitializeOnLoad]
    public static class PoolTypeHelper
    {
        public readonly static Type[] poolTypes;
        public readonly static Dictionary<string, Type> poolTypeDict;
        public readonly static string[] poolTypeNames;

        static PoolTypeHelper()
        {
            poolTypes = ReflectionHelper.GetEnumerableOfType<AbstractPool>().ToArray();
            poolTypeDict = new Dictionary<string, Type>();
            foreach (var poolType in poolTypes)
            {
                Debug.Log(poolType.Name);
                poolTypeDict.Add(poolType.Name, poolType);
            }

            poolTypeNames = poolTypes.Select((type => type.Name)).ToArray();
        }
    }
}