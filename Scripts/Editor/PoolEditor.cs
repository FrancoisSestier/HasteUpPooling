using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Unity.VisualScripting;

namespace HasteUp.Pooling
{
    [CustomEditor(typeof(PoolData))]
    public class PoolDataEditor : Editor
    {
        int valueDropdown = 0;
        PoolData poolData;

        public void OnEnable()
        {
            poolData = target as PoolData;
            if (String.IsNullOrEmpty(poolData.PoolType))
            {
                poolData.PoolType = typeof(DefaultPool).Name;
                poolData.PoolTypeAssembly = typeof(DefaultPool).Assembly.GetName().Name;
            }

            for (int i = 0; i < PoolTypeHelper.poolTypeNames.Length; i++)
            {
                if (poolData.PoolType == PoolTypeHelper.poolTypeNames[i])
                {
                    valueDropdown = i;
                }
            }
        }


        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (poolData)
            {
                EditorGUI.BeginChangeCheck();
                valueDropdown = EditorGUILayout.Popup("PoolType", valueDropdown, PoolTypeHelper.poolTypeNames);
                if (EditorGUI.EndChangeCheck())
                {
                    PoolTypeHelper.poolTypeDict.TryGetValue(PoolTypeHelper.poolTypeNames[valueDropdown], out Type type);
                    poolData.PoolType = type.Name;
                    poolData.PoolTypeAssembly = type.Assembly.GetName().Name;
                    EditorUtility.SetDirty(poolData);
                }
            }
        }
    }
}