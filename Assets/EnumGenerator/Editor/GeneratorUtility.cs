using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEditor.Generator
{
    public static class GeneratorUtility
    {
        public static List<string> PrepareEnumsForGenerator<T>(string enumType) where T : Enum
        {
            Array array = Enum.GetValues(typeof(T));
            List<string> newEnumInstances = (from object enumInstance in array select enumInstance.ToString()).ToList();
            newEnumInstances.Add(enumType);
            return newEnumInstances;
        }
    }
}