using System.Linq;
using NUnit.Framework;

namespace NChanges.Core.Tests
{
    [TestFixture]
    public class Describe_Project
    {
        [Test]
        public void It_writes_XML()
        {
            var project = new Project
            {
                NChangesToolPath = @"C:\path\to\NChanges.Tool.exe",
                TypesToExclude = "Internal$",
                AssembliesToSnapshot =
                {
                    new AssemblyToSnapshot { Path = @"C:\path\to\Assembly1.dll" },
                    new AssemblyToSnapshot { Path = @"C:\path\to\Assembly2.dll", Version = "1.2.3.4" },
                }
            };

            var xml = XML.UseWriter(project.WriteXml);

            Assert.AreEqual(
@"<Project xmlns=""http://schemas.microsoft.com/developer/msbuild/2003"">
  <PropertyGroup>
    <NChangesTool>C:\path\to\NChanges.Tool.exe</NChangesTool>
    <TypesToExclude>Internal$</TypesToExclude>
  </PropertyGroup>
  <ItemGroup>
    <Assembly Include=""C:\path\to\Assembly1.dll"" />
    <Assembly Include=""C:\path\to\Assembly2.dll"">
      <Version>1.2.3.4</Version>
    </Assembly>
  </ItemGroup>
</Project>",
                xml);
        }
    }
}