namespace UnityEditor.Generator
{
    public class GeneratorEnumSettings
    {
        public string EnumName { get; set; }
        public string DirectoryPath { get; set; }
        public string NameSpace { get; set; }

        public GeneratorEnumSettings(string directoryPath, string enumName, string nameSpace = "")
        {
            EnumName = enumName;
            DirectoryPath = directoryPath;
            NameSpace = nameSpace; // optional
        }
    }
}