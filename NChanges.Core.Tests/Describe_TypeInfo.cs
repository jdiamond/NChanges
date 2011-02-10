using System.Linq;
using NUnit.Framework;

namespace NChanges.Core.Tests
{
    [TestFixture]
    public class Describe_TypeInfo
    {
        [Test]
        public void It_can_create_a_deep_clone_of_itself()
        {
            var type1 = new TypeInfo
                        {
                            Name = "MyType",
                            Kind = TypeKind.Class,
                            Members =
                                {
                                    new MemberInfo
                                    {
                                        Changes = { new MemberChangeInfo() }
                                    }
                                },
                            Changes =
                                {
                                    new TypeChangeInfo()
                                }
                        };

            var type2 = type1.Clone();

            Assert.AreNotSame(type1, type2);
            Assert.AreEqual(type1.Name, type2.Name);
            Assert.AreEqual(type1.Kind, type2.Kind);
            Assert.AreNotSame(type1.Members, type2.Members);
            Assert.AreNotSame(type1.Members.Single(), type2.Members.Single());
            Assert.AreNotSame(type1.Members.Single().Changes.Single(), type2.Members.Single().Changes.Single());
            Assert.AreNotSame(type1.Changes, type2.Changes);
            Assert.AreNotSame(type1.Changes.Single(), type2.Changes.Single());
        }

        [Test]
        public void It_finds_the_default_constructor()
        {
            var typeInfo = new TypeInfo();

            typeInfo.ReadType(Compiler.GetType(
@"namespace MyNamespace
{
    public class MyClass { }
}"));

            Assert.AreEqual(1, typeInfo.Members.Count);
        }

        [Test]
        public void It_doesnt_find_virtual_methods_defined_in_System_Object_that_arent_overridden()
        {
            var typeInfo = new TypeInfo();

            typeInfo.ReadType(Compiler.GetType(
@"namespace MyNamespace
{
    public class MyClass
    {
        private MyClass() { }
    }
}"));

            Assert.AreEqual(0, typeInfo.Members.Count);
        }

        [Test]
        public void It_doesnt_find_virtual_methods_defined_in_System_Object_that_are_overridden()
        {
            var typeInfo = new TypeInfo();

            typeInfo.ReadType(Compiler.GetType(
@"namespace MyNamespace
{
    public class MyClass
    {
        private MyClass() { }
        public override string ToString() { return null; }
    }
}"));

            Assert.AreEqual(0, typeInfo.Members.Count);
        }

        [Test]
        public void It_doesnt_find_virtual_properties_defined_in_base_types_that_arent_overridden()
        {
            var typeInfo = new TypeInfo();

            typeInfo.ReadType(Compiler.GetType("MyNamespace.MyDerivedClass",
@"namespace MyNamespace
{
    public class MyBaseClass
    {
        public virtual int MyProperty { get; set; }
    }

    public class MyDerivedClass : MyBaseClass
    {
        private MyDerivedClass() { }
    }
}"));

            Assert.AreEqual(0, typeInfo.Members.Count);
        }

        [Test]
        public void It_doesnt_find_virtual_properties_defined_in_base_types_that_are_overridden()
        {
            var typeInfo = new TypeInfo();

            typeInfo.ReadType(Compiler.GetType("MyNamespace.MyDerivedClass",
@"namespace MyNamespace
{
    public class MyBaseClass
    {
        public virtual int MyProperty { get; set; }
    }

    public class MyDerivedClass : MyBaseClass
    {
        private MyDerivedClass() { }
        public override int MyProperty { get; set; }
    }
}"));

            Assert.AreEqual(0, typeInfo.Members.Count);
        }

        [Test]
        public void It_doesnt_find_property_get_and_set_methods()
        {
            var typeInfo = new TypeInfo();

            typeInfo.ReadType(Compiler.GetType(
@"namespace MyNamespace
{
    public class MyClass
    {
        private MyClass() { }
        public int MyProperty { get { return 0; } set { } }
    }
}"));

            Assert.AreEqual(1, typeInfo.Members.Count);
        }

        [Test]
        public void It_doesnt_find_event_add_and_remove_methods()
        {
            var typeInfo = new TypeInfo();

            typeInfo.ReadType(Compiler.GetType(
@"namespace MyNamespace
{
    public class MyClass
    {
        private MyClass() { }
        public event System.EventHandler MyEvent { add { } remove { } }
    }
}"));

            Assert.AreEqual(1, typeInfo.Members.Count);
        }

        [Test]
        public void It_doesnt_find_the_special_value_field_for_enum_types()
        {
            var typeInfo = new TypeInfo();

            typeInfo.ReadType(Compiler.GetType(
@"namespace MyNamespace
{
    public enum MyEnum
    {
        MyField1,
        MyField2
    }
}"));

            Assert.AreEqual(2, typeInfo.Members.Count);
        }

        [Test]
        public void It_finds_public_members()
        {
            var typeInfo = new TypeInfo();

            typeInfo.ReadType(Compiler.GetType(
@"namespace MyNamespace
{
    public class MyClass
    {
        public MyClass() { }
        public void MyMethod() { }
        public int MyProperty { get; set; }
        public event System.EventHandler MyEvent;
        public int MyField;
    }
}"));

            Assert.AreEqual(5, typeInfo.Members.Count);
        }

        [Test]
        public void It_doesnt_find_private_members()
        {
            var typeInfo = new TypeInfo();

            typeInfo.ReadType(Compiler.GetType(
@"namespace MyNamespace
{
    public class MyClass
    {
        private MyClass() { }
        private void MyMethod() { }
        private int MyProperty { get; set; }
        private event System.EventHandler MyEvent;
        private int MyField;
    }
}"));

            Assert.AreEqual(0, typeInfo.Members.Count);
        }

        [Test]
        public void It_detects_the_type_kind()
        {
            var assemblyInfo = new AssemblyInfo();

            assemblyInfo.ReadAssembly(Compiler.GetAssembly(
@"namespace MyNamespace
{
    public class MyClass { }
    public struct MyStruct { }
    public interface MyInterface { }
    public enum MyEnum { }
    public delegate void MyDelegate();
}"));

            Assert.IsTrue(assemblyInfo.Types.Any(m => m.Name == "MyClass" && m.Kind == TypeKind.Class));
            Assert.IsTrue(assemblyInfo.Types.Any(m => m.Name == "MyStruct" && m.Kind == TypeKind.ValueType));
            Assert.IsTrue(assemblyInfo.Types.Any(m => m.Name == "MyInterface" && m.Kind == TypeKind.Interface));
            Assert.IsTrue(assemblyInfo.Types.Any(m => m.Name == "MyEnum" && m.Kind == TypeKind.Enum));
            Assert.IsTrue(assemblyInfo.Types.Any(m => m.Name == "MyDelegate" && m.Kind == TypeKind.Delegate));
        }

        [Test]
        public void It_detects_the_kind_of_each_member()
        {
            var typeInfo = new TypeInfo();

            typeInfo.ReadType(Compiler.GetType(
@"namespace MyNamespace
{
    public class MyClass
    {
        public MyClass() { }
        public void MyMethod() { }
        public int MyProperty { get; set; }
        public event System.EventHandler MyEvent;
        public int MyField;
    }
}"));

            Assert.IsTrue(typeInfo.Members.Any(m => m.Name == ".ctor" && m.Kind == MemberKind.Constructor));
            Assert.IsTrue(typeInfo.Members.Any(m => m.Name == "MyMethod" && m.Kind == MemberKind.Method));
            Assert.IsTrue(typeInfo.Members.Any(m => m.Name == "MyProperty" && m.Kind == MemberKind.Property));
            Assert.IsTrue(typeInfo.Members.Any(m => m.Name == "MyEvent" && m.Kind == MemberKind.Event));
            Assert.IsTrue(typeInfo.Members.Any(m => m.Name == "MyField" && m.Kind == MemberKind.Field));
        }
    }
}