# UnityEnumGenerator

Generator Enum is a library that assists in generating enums in the Unity3D editor without the need for an IDE. The library includes input checks, which remove any illegal symbols that are not allowed in C#. This makes it user-friendly for users without knowledge of C#.

Please note that this library does not have a visual user interface at the moment, so you will have to integrate it into your custom tools yourself.

# Install via UPM 
Add the git url in the unity package manager:
```Http
https://github.com/AlehZakharau/UnityEnumGenerator.git?path=/Assets/EnumGenerator
```
or 
Add the line below in the manifest
```Json
"com.alehzakharau.reactivesystem": "https://github.com/AlehZakharau/UnityEnumGenerator.git?path=/Assets/EnumGenerator",
```

# How to use

First you need to create a settings instance for the generator.
```c#
var generatorEnumSettings = new GeneratorEnumSettings{
  EnumName = enumName
  NameSpace = fileNameSpace //Optional
  FilePath = directoryPath
}
```

Next, use the following method to generate a new enum file and recompile the Unity3d editor. 
This method requires the settings instance and the enum names you want to generate.
```c#
GeneratorEnum.GenerateEnum(GeneratorEnumSettings settings, string[] enumNames)
```
or
```c#
GeneratorEnum.GenerateEnum(GeneratorEnumSettings settings, List<string> enumNames)
```

This utility method return a list<string> enums of existence type T + new enum
```c#
GeneratorUtility.PrepareEnumsForGenerator<T>("newEnum")
```

# Author
### [Aleh Zakharau](https://zakharau.notion.site/Aleh-Zakharau-3823f9989352415c8901a81c84681f07)

# Licence
MIT License
