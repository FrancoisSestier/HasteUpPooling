using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace HasteUp.Reflection
{
    public static class ReflectionHelper
    {
        // Start is called before the first frame update
        public static List<Type> GetEnumerableOfType<T>() where T : class
        {
            HashSet<Type> objects = new HashSet<Type>();
            foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type type in
                         Assembly.GetAssembly(typeof(T)).GetTypes()
                             .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(T))))
                {
                    objects.Add(type);
                }
            }
            

            return objects.ToList();
        }
    }
}