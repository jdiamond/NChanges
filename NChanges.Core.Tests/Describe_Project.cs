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
                TypesToExcludePattern = "Internal$",
                ExcelOutputPath = "Changes.xls",
                AssembliesToSnapshot =
                {
                    new AssemblyToSnapshot { Path = @"C:\path\to\Assembly1.dll" },
                    new AssemblyToSnapshot { Path = @"C:\path\to\Assembly2.dll", Version = "1.2.3.4" },
                }
            };

            var xml = XML.UseWriter(project.WriteXml);

            Assert.AreEqual(
@"<Project DefaultTargets=""Excel"" xmlns=""http://schemas.microsoft.com/developer/msbuild/2003"">
  <PropertyGroup>
    <NChangesTool>C:\path\to\NChanges.Tool.exe</NChangesTool>
    <TypesToExclude>Internal$</TypesToExclude>
    <ExcelOutput>Changes.xls</ExcelOutput>
  </PropertyGroup>
  <ItemGroup>
    <Assembly Include=""C:\path\to\Assembly1.dll"" />
    <Assembly Include=""C:\path\to\Assembly2.dll"">
      <Version>1.2.3.4</Version>
    </Assembly>
  </ItemGroup>
  <Target Name=""Snapshot"">
    <Exec Command=""$(NChangesTool) snapshot %(Assembly.Identity) -v=%(Version) -x=$(TypesToExclude)"" />
  </Target>
  <Target Name=""Report"">
    <Exec Command=""$(NChangesTool) report *-snapshot.xml"" />
  </Target>
  <Target Name=""Excel"">
    <Exec Command=""$(NChangesTool) excel *-report.xml -o=$(ExcelOutput)"" />
  </Target>
  <Target Name=""Clean"">
    <Delete Files=""%(Assembly.Filename)-%(Version)-snapshot.xml"" />
    <Delete Files=""%(Assembly.Filename)-%(Version)-report.xml"" />
    <Delete Files=""$(ExcelOutput)"" />
  </Target>
</Project>",
                xml);
        }
    }
}