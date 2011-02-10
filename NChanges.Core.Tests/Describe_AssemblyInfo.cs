using System.Linq;
using NUnit.Framework;

namespace NChanges.Core.Tests
{
    [TestFixture]
    public class Describe_AssemblyInfo
    {
        [Test]
        public void It_reads_the_assembly_name()
        {
            var assemblyInfo = new AssemblyInfo();

            assemblyInfo.ReadAssembly(Compiler.GetAssembly("MyAssembly.dll",
@"namespace MyNamespace
{
    public class MyClass { }
}"));

            Assert.AreEqual("MyAssembly", assemblyInfo.Name);
        }

        [Test]
        public void It_reads_the_assembly_version()
        {
            var assemblyInfo = new AssemblyInfo();

            assemblyInfo.ReadAssembly(Compiler.GetAssembly(
@"[assembly: System.Reflection.AssemblyVersion(""1.2.3.4"")]

namespace MyNamespace
{
    public class MyClass { }
}"));

            Assert.AreEqual("1.2.3.4", assemblyInfo.Version);
        }

        [Test]
        public void It_finds_public_types()
        {
            var assemblyInfo = new AssemblyInfo();

            assemblyInfo.ReadAssembly(Compiler.GetAssembly(
@"namespace MyNamespace
{
    public class MyClass { }
}"));

            Assert.AreEqual(1, assemblyInfo.Types.Count);
        }

        [Test]
        public void It_doesnt_find_internal_types()
        {
            var assemblyInfo = new AssemblyInfo();

            assemblyInfo.ReadAssembly(Compiler.GetAssembly(
@"namespace MyNamespace
{
    internal class MyInternalClass { }
}"));

            Assert.AreEqual(0, assemblyInfo.Types.Count);
        }

        [Test]
        public void It_writes_XML()
        {
            var assemblyInfo = new AssemblyInfo
                               {
                                   Name = "MyAssembly",
                                   Version = "1",
                                   Types =
                                       {
                                           new TypeInfo
                                           {
                                               Name = "MyClass",
                                               Namespace = "MyNamespace",
                                               Kind = TypeKind.Class,
                                               Members =
                                                   {
                                                       new MemberInfo
                                                       {
                                                           Name = "MyMethod",
                                                           Kind = MemberKind.Method,
                                                           Parameters =
                                                               {
                                                                   new ParameterInfo
                                                                   {
                                                                       Name = "myParameter",
                                                                       Type = "System.Int32"
                                                                   }
                                                               },
                                                           Changes =
                                                               {
                                                                   new MemberChangeInfo
                                                                   {
                                                                       Kind = MemberChangeKind.Added,
                                                                       Version = "1",
                                                                       Old = "old value",
                                                                       New = "new value"
                                                                   }
                                                               }
                                                       }
                                                   },
                                               Changes =
                                                   {
                                                       new TypeChangeInfo
                                                       {
                                                           Kind = TypeChangeKind.Added,
                                                           Version="1"
                                                       }
                                                   }
                                           }
                                       }
                               };

            var xml = XML.UseWriter(assemblyInfo.WriteXml);

            Assert.AreEqual(
@"<assembly name=""MyAssembly"" version=""1"">
  <type name=""MyClass"" namespace=""MyNamespace"" kind=""Class"">
    <change kind=""Added"" version=""1"" />
    <member name=""MyMethod"" kind=""Method"">
      <change kind=""Added"" version=""1"" old=""old value"" new=""new value"" />
      <param name=""myParameter"" type=""System.Int32"" />
    </member>
  </type>
</assembly>",
                xml);
        }

        [Test]
        public void It_reads_XML()
        {
            var assemblyInfo = new AssemblyInfo();

            XML.UseReader(assemblyInfo.ReadXml,
@"<assembly name=""MyAssembly"" version=""1"">
  <type name=""MyClass"" namespace=""MyNamespace"" kind=""Class"">
    <member name=""MyMethod"" kind=""Method"">
      <param name=""myParameter"" type=""System.Int32"" />
    </member>
  </type>
</assembly>");

            Assert.AreEqual("MyAssembly", assemblyInfo.Name);
            Assert.AreEqual("1", assemblyInfo.Version);
            Assert.AreEqual("MyClass", assemblyInfo.Types.Single().Name);
            Assert.AreEqual("MyNamespace", assemblyInfo.Types.Single().Namespace);
            Assert.AreEqual(TypeKind.Class, assemblyInfo.Types.Single().Kind);
            Assert.AreEqual("MyMethod", assemblyInfo.Types.Single().Members.Single().Name);
            Assert.AreEqual(MemberKind.Method, assemblyInfo.Types.Single().Members.Single().Kind);
            Assert.AreEqual("myParameter", assemblyInfo.Types.Single().Members.Single().Parameters.Single().Name);
            Assert.AreEqual("System.Int32", assemblyInfo.Types.Single().Members.Single().Parameters.Single().Type);
        }
    }
}