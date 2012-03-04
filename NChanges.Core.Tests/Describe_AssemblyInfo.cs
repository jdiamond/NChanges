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
                                               Obsolete = true,
                                               ObsoleteMessage = "I'm obsolete!",
                                               Members =
                                                   {
                                                       new MemberInfo
                                                       {
                                                           Name = "MyMethod",
                                                           Kind = MemberKind.Method,
                                                           Type = "System.String",
                                                           Obsolete = true,
                                                           ObsoleteMessage = "I'm also obsolete!",
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
                                                                       Kind = MemberChangeKind.AddedMember,
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
                                                           Kind = TypeChangeKind.AddedType,
                                                           Version="1"
                                                       }
                                                   }
                                           }
                                       }
                               };

            var xml = XML.UseWriter(assemblyInfo.WriteXml);

            Assert.AreEqual(
@"<assembly name=""MyAssembly"" version=""1"">
  <type name=""MyClass"" namespace=""MyNamespace"" kind=""Class"" obsolete=""True"" obsoleteMessage=""I'm obsolete!"">
    <change kind=""AddedType"" version=""1"" />
    <member name=""MyMethod"" kind=""Method"" type=""System.String"" obsolete=""True"" obsoleteMessage=""I'm also obsolete!"">
      <change kind=""AddedMember"" version=""1"" old=""old value"" new=""new value"" />
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
  <type name=""MyClass"" namespace=""MyNamespace"" kind=""Class"" obsolete=""True"" obsoleteMessage=""I'm obsolete!"">
    <change kind=""AddedType"" version=""1"" />
    <member name=""MyMethod"" kind=""Method"" type=""System.String"" obsolete=""True"" obsoleteMessage=""I'm also obsolete!"">
      <change kind=""AddedMember"" version=""1"" old=""old value"" new=""new value"" />
      <param name=""myParameter"" type=""System.Int32"" />
    </member>
  </type>
</assembly>");

            Assert.AreEqual("MyAssembly", assemblyInfo.Name);
            Assert.AreEqual("1", assemblyInfo.Version);
            Assert.AreEqual("MyClass", assemblyInfo.Types.Single().Name);
            Assert.AreEqual("MyNamespace", assemblyInfo.Types.Single().Namespace);
            Assert.AreEqual(TypeKind.Class, assemblyInfo.Types.Single().Kind);
            Assert.IsTrue(assemblyInfo.Types.Single().Obsolete);
            Assert.AreEqual("I'm obsolete!", assemblyInfo.Types.Single().ObsoleteMessage);
            Assert.AreEqual(TypeChangeKind.AddedType, assemblyInfo.Types.Single().Changes.Single().Kind);
            Assert.AreEqual("1", assemblyInfo.Types.Single().Changes.Single().Version);
            Assert.AreEqual("MyMethod", assemblyInfo.Types.Single().Members.Single().Name);
            Assert.AreEqual(MemberKind.Method, assemblyInfo.Types.Single().Members.Single().Kind);
            Assert.AreEqual("System.String", assemblyInfo.Types.Single().Members.Single().Type);
            Assert.IsTrue(assemblyInfo.Types.Single().Members.Single().Obsolete);
            Assert.AreEqual("I'm also obsolete!", assemblyInfo.Types.Single().Members.Single().ObsoleteMessage);
            Assert.AreEqual(MemberChangeKind.AddedMember, assemblyInfo.Types.Single().Members.Single().Changes.Single().Kind);
            Assert.AreEqual("1", assemblyInfo.Types.Single().Members.Single().Changes.Single().Version);
            Assert.AreEqual("old value", assemblyInfo.Types.Single().Members.Single().Changes.Single().Old);
            Assert.AreEqual("new value", assemblyInfo.Types.Single().Members.Single().Changes.Single().New);
            Assert.AreEqual("myParameter", assemblyInfo.Types.Single().Members.Single().Parameters.Single().Name);
            Assert.AreEqual("System.Int32", assemblyInfo.Types.Single().Members.Single().Parameters.Single().Type);
        }

        [Test]
        public void It_strips_the_assembly_version_culture_and_public_key_token_from_parameter_types()
        {
            var assemblyInfo = new AssemblyInfo();

            assemblyInfo.ReadAssembly(Compiler.GetAssembly(
@"[assembly: System.Reflection.AssemblyVersion(""1.2.3.4"")]

namespace MyNamespace
{
    public class MyClass
    {
        public void MyMethod(System.Collections.Generic.IEnumerable<MyClass> obj) { }
    }
}"));

            Assert.AreEqual(
                "System.Collections.Generic.IEnumerable`1[[MyNamespace.MyClass]]",
                assemblyInfo.Types.Single().Members.Get("MyMethod").Parameters.Single().Type);
        }

        [Test]
        public void It_can_exclude_specified_types()
        {
            var assemblyInfo = new AssemblyInfo();

            assemblyInfo.ReadAssembly(Compiler.GetAssembly(
@"namespace MyNamespace
{
    public class MyClass1 { }

    public class MyClass2 { }
}"), "Class2");

            Assert.AreEqual("MyClass1", assemblyInfo.Types.Single().Name);
        }
    }
}