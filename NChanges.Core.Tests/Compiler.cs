using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.CSharp;

namespace NChanges.Core.Tests
{
    public static class Compiler
    {
        public static Type GetType(string source)
        {
            return GetAssembly(source).GetTypes().Single();
        }

        public static Type GetType(string typeName, string source)
        {
            return GetAssembly(source).GetTypes().Single(t => t.FullName == typeName);
        }

        public static Assembly GetAssembly(string source)
        {
            return GetAssembly("MyAssembly.dll", source);
        }

        public static Assembly GetAssembly(string assemblyName, string source)
        {
            var compilerOptions = new Dictionary<string, string>
                                  {
                                      { "CompilerVersion", "v3.5" }
                                  };

            var codeProvider = new CSharpCodeProvider(compilerOptions);

            var compilerParameters = new CompilerParameters
                                     {
                                         GenerateInMemory = true,
                                         OutputAssembly = assemblyName
                                     };

            var compilerResults = codeProvider.CompileAssemblyFromSource(compilerParameters, source);

            if (compilerResults.Errors.HasErrors)
            {
                throw new Exception(compilerResults.Errors[0].ErrorText);
            }

            return compilerResults.CompiledAssembly;
        }
    }
}