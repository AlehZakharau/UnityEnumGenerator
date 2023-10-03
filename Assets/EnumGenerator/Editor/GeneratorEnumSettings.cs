namespace UnityEditor.Generator
{
    public class GeneratorEnumSettings
    {
        public GeneratorEnumSettings(string filePath, string nameSpace, string enumName)
        {
            FilePath = filePath;
            NameSpace = nameSpace;
            EnumName = enumName;
        }

        public string EnumName { get; private set; }
        public string NameSpace { get; private set; }
        public string FilePath { get; private set; }
    }
}