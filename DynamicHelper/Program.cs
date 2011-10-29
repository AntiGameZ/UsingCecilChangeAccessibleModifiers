using System;
using System.Linq;
using Mono.Cecil;

namespace DynamicHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            var asmFile = args[0];
            Console.WriteLine("change accessible modifiers to 'public' in '{0}'.", asmFile);
            var asmDef = AssemblyDefinition.ReadAssembly(asmFile, new ReaderParameters
            {
                ReadSymbols = true
            });
            var anonymousTypes = asmDef.Modules
                .SelectMany(m => m.Types)
                .Where(t => t.Name.Contains("<>f__AnonymousType"));
            Console.WriteLine("find {0} anonymous type(s).", anonymousTypes.Count());
            foreach (var type in anonymousTypes)
            {
                type.IsPublic = true;
            }
            asmDef.Write(asmFile, new WriterParameters
            {
                WriteSymbols = true
            });
        }
    }
}
