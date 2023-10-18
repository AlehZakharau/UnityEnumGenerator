using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEditor.Generator
{
    public static class GeneratorUtility
    {
        /// <summary>
        /// This utility method return a list enums of existence type T + new enum
        /// </summary>
        /// <param name="enumType">New enum name</param>
        /// <typeparam name="T">type of enum</typeparam>
        /// <returns></returns>
        public static List<string> PrepareEnumsForGenerator<T>(string enumType) where T : Enum
        {
            Array array = Enum.GetValues(typeof(T));
            List<string> newEnumInstances = (from object enumInstance in array select enumInstance.ToString()).ToList();
            newEnumInstances.Add(enumType);
            return newEnumInstances;
        }
    }
}