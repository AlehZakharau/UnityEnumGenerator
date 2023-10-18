using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

namespace UnityEditor.Generator
{
    public static class GeneratorEnum
    {
        private static readonly List<string> _logs = new();
        /// <summary>
        /// Method generates a new enum file and recompile the Unity3d editor.
        /// </summary>
        /// <param name="generatorEnumSettings">setting: enumType and directory path</param>
        /// <param name="enumInstances">enumNames</param>
        public static void GenerateEnum(GeneratorEnumSettings generatorEnumSettings, string[] enumInstances)
        {
            GenerateEnumFile(generatorEnumSettings, enumInstances);
        }
        /// <summary>
        /// Method generates a new enum file and recompile the Unity3d editor.
        /// </summary>
        /// <param name="generatorEnumSettings">setting: enumType and directory path</param>
        /// <param name="enumInstances">enumNames</param>
        public static void GenerateEnum(GeneratorEnumSettings generatorEnumSettings, List<string> enumInstances)
        {
            GenerateEnumFile(generatorEnumSettings, enumInstances.ToArray());
        }

        private static void GenerateEnumFile(GeneratorEnumSettings generatorEnumSettings, string[] enumInstances)
        {
            var name = generatorEnumSettings.EnumName;
            var filePathAndName = $"{generatorEnumSettings.DirectoryPath}/{generatorEnumSettings.EnumName}.cs";
            var nameSpace = generatorEnumSettings.NameSpace;
            var hasNameSpace = !string.IsNullOrEmpty(nameSpace);
            
            using (StreamWriter streamWriter = new StreamWriter(filePathAndName))
            {
                WriteHeader(streamWriter);
                WriteEnums(streamWriter);
                WriteEnd(streamWriter);
                PrintLog(name);
                AssetDatabase.Refresh();
            }
            
            void WriteEnums(StreamWriter streamWriter)
            {
                for (int i = 0; i < enumInstances.Length; i++)
                {
                    var checkedEnum = enumInstances[i];
                    if(string.IsNullOrEmpty(checkedEnum))
                        continue;
                    if(!CheckRepeated(checkedEnum, enumInstances, i))
                        continue;
                    if(!CheckSystemWords(checkedEnum))
                        continue;
                    checkedEnum = CheckAndCorrectEnumChar(checkedEnum);
                    checkedEnum = CheckAndCorrectFirstSymbol(checkedEnum);
                    
                    if(hasNameSpace)
                        streamWriter.WriteLine( "\t\t" + checkedEnum + "," );
                    else 
                        streamWriter.WriteLine( "\t" + checkedEnum + "," );
                }
            }
            
            void WriteHeader(StreamWriter streamWriter)
            {
                if (hasNameSpace)
                {
                    streamWriter.WriteLine($"namespace {nameSpace}");
                    streamWriter.WriteLine( "{" );
                    streamWriter.WriteLine( $"\tpublic enum {name}");
                    streamWriter.WriteLine( "\t{" );
                }
                else
                {
                    streamWriter.WriteLine( $"public enum {name}");
                    streamWriter.WriteLine( "{" );
                }
            }

            void WriteEnd(StreamWriter streamWriter)
            {
                if (hasNameSpace)
                {
                    streamWriter.WriteLine("\t}");
                    streamWriter.WriteLine("}");
                }
                else
                    streamWriter.WriteLine( "}" );
            }
        }

        private static string CheckAndCorrectFirstSymbol(string checkedEnum)
        {
            var firstSymbol = checkedEnum.ToCharArray()[0];
            if (char.IsNumber(firstSymbol))
            {
                _logs.Add($"You can start {checkedEnum} with number");
                return checkedEnum.Insert(0, "_");
            }
            else
                return checkedEnum;
        }

        private static string CheckAndCorrectEnumChar(string checkedEnum)
        {
            const string pattern = "[^A-Z|^a-z|^_|^0-9]";
            var regex = new Regex(pattern);
            if (regex.IsMatch(checkedEnum))
            {
                _logs.Add($"Not allowed symbols found in {checkedEnum}");
                return Regex.Replace(checkedEnum, pattern, "");
            }
            else
                return checkedEnum;
        }

        private static bool CheckSystemWords(string checkedEnum)
        {
            if (CsharpKeywords.Contains(checkedEnum))
            {
                _logs.Add($"You use c# system word {checkedEnum}");
                return false;
            }
            return true;
        }

        private static bool CheckRepeated(string checkedEnum, string[] enumInstances, int enumIndex)
        {
            for (int i = 0; i < enumInstances.Length; i++)
            {
                if (checkedEnum.Equals(enumInstances[i]) && i != enumIndex)
                {
                    _logs.Add( $"You typed enum {checkedEnum} twice");
                    return false;
                }
            }

            return true;
        }

        private static void PrintLog(string enumName)
        {
            var message = $"Generating enums {enumName} is done\nWarnings found: {_logs.Count}";
            foreach (var log in _logs) 
                message += $"\n{log}";
            Debug.Log($"<color=white>{message}</color>");
        }

        private static readonly HashSet<string> CsharpKeywords = new()
        {
            "abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char", "checked",
            "class", "const", "continue", "decimal", "default", "delegate", "do", "double", "else",
            "enum", "event", "explicit", "extern", "false", "finally", "fixed", "float", "for",
            "foreach", "goto", "if", "implicit", "in", "int", "interface", "internal", "is", "lock",
            "long", "namespace", "new", "null", "object", "operator", "out", "override", "params",
            "private", "protected", "public", "readonly", "ref", "return", "sbyte", "sealed",
            "short", "sizeof", "stackalloc", "static", "string", "struct", "switch", "this", "throw",
            "true", "try", "typeof", "uint", "ulong", "unchecked", "unsafe", "ushort", "using",
            "virtual", "void", "volatile", "while"
        };
    }
}