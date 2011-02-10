using System.Linq;
using NUnit.Framework;

namespace NChanges.Core.Tests
{
    [TestFixture]
    public class Describe_Reporter
    {
        [Test]
        public void It_doesnt_report_types_as_added_for_the_first_version()
        {
            var reporter = new Reporter
                           {
                               Assemblies =
                                   {
                                       new AssemblyInfo
                                       {
                                           Name = "MyAssembly",
                                           Version = "1",
                                           Types =
                                               {
                                                   new TypeInfo
                                                   {
                                                       Name = "MyType"
                                                   }
                                               }
                                       }
                                   }
                           };

            var report = reporter.GenerateReport();

            Assert.AreEqual(0, report.Types.Single().Changes.Count);
        }

        [Test]
        public void It_doesnt_report_existing_types_multiple_times()
        {
            var reporter = new Reporter
                           {
                               Assemblies =
                                   {
                                       new AssemblyInfo
                                       {
                                           Name = "MyAssembly",
                                           Version = "1",
                                           Types =
                                               {
                                                   new TypeInfo
                                                   {
                                                       Name = "MyType1"
                                                   }
                                               }
                                       },
                                       new AssemblyInfo
                                       {
                                           Name = "MyAssembly",
                                           Version = "2",
                                           Types =
                                               {
                                                   new TypeInfo
                                                   {
                                                       Name = "MyType1"
                                                   },
                                                   new TypeInfo
                                                   {
                                                       Name = "MyType2"
                                                   }
                                               }
                                       }
                                   }
                           };

            var report = reporter.GenerateReport();

            Assert.AreEqual(1, report.Types.Count(t => t.Name == "MyType1"));
        }

        [Test]
        public void It_reports_new_types_as_added_for_the_second_version()
        {
            var reporter = new Reporter
                           {
                               Assemblies =
                                   {
                                       new AssemblyInfo
                                       {
                                           Name = "MyAssembly",
                                           Version = "1",
                                           Types =
                                               {
                                                   new TypeInfo
                                                   {
                                                       Name = "MyType1"
                                                   }
                                               }
                                       },
                                       new AssemblyInfo
                                       {
                                           Name = "MyAssembly",
                                           Version = "2",
                                           Types =
                                               {
                                                   new TypeInfo
                                                   {
                                                       Name = "MyType1"
                                                   },
                                                   new TypeInfo
                                                   {
                                                       Name = "MyType2"
                                                   }
                                               }
                                       }
                                   }
                           };

            var report = reporter.GenerateReport();

            Assert.AreEqual(TypeChangeKind.Added, report.Types.Single(t => t.Name == "MyType2").Changes.Single().Kind);
            Assert.AreEqual("2", report.Types.Single(t => t.Name == "MyType2").Changes.Single().Version);
        }

        [Test]
        public void It_reports_missing_types_as_removed_for_the_second_version()
        {
            var reporter = new Reporter
            {
                Assemblies =
                                   {
                                       new AssemblyInfo
                                       {
                                           Name = "MyAssembly",
                                           Version = "1",
                                           Types =
                                               {
                                                   new TypeInfo
                                                   {
                                                       Name = "MyType"
                                                   }
                                               }
                                       },
                                       new AssemblyInfo
                                       {
                                           Name = "MyAssembly",
                                           Version = "2",
                                       }
                                   }
            };

            var report = reporter.GenerateReport();

            Assert.AreEqual(TypeChangeKind.Removed, report.Types.Single(t => t.Name == "MyType").Changes.Last().Kind);
            Assert.AreEqual("2", report.Types.Single(t => t.Name == "MyType").Changes.Last().Version);
        }

        [Test]
        public void It_sorts_assemblies_by_version_before_generating_the_report()
        {
            var reporter = new Reporter
                           {
                               Assemblies =
                                   {
                                       new AssemblyInfo
                                       {
                                           Name = "MyAssembly",
                                           Version = "2",
                                           Types =
                                               {
                                                   new TypeInfo
                                                   {
                                                       Name = "MyType1"
                                                   },
                                                   new TypeInfo
                                                   {
                                                       Name = "MyType2"
                                                   }
                                               }
                                       },
                                       new AssemblyInfo
                                       {
                                           Name = "MyAssembly",
                                           Version = "1",
                                           Types =
                                               {
                                                   new TypeInfo
                                                   {
                                                       Name = "MyType1"
                                                   }
                                               }
                                       },
                                   }
                           };

            var report = reporter.GenerateReport();

            Assert.AreEqual(0, report.Types.Single(t => t.Name == "MyType1").Changes.Count);
            Assert.AreEqual("2", report.Types.Single(t => t.Name == "MyType2").Changes.First().Version);
        }

        [Test]
        public void It_uses_natural_sorting_for_the_version_numbers()
        {
            var reporter = new Reporter
                           {
                               Assemblies =
                                   {
                                       new AssemblyInfo
                                       {
                                           Name = "MyAssembly",
                                           Version = "1.2.10",
                                           Types =
                                               {
                                                   new TypeInfo
                                                   {
                                                       Name = "MyType1"
                                                   },
                                                   new TypeInfo
                                                   {
                                                       Name = "MyType2"
                                                   }
                                               }
                                       },
                                       new AssemblyInfo
                                       {
                                           Name = "MyAssembly",
                                           Version = "1.2.9",
                                           Types =
                                               {
                                                   new TypeInfo
                                                   {
                                                       Name = "MyType1"
                                                   }
                                               }
                                       },
                                   }
                           };

            var report = reporter.GenerateReport();

            Assert.AreEqual(0, report.Types.Single(t => t.Name == "MyType1").Changes.Count);
            Assert.AreEqual("1.2.10", report.Types.Single(t => t.Name == "MyType2").Changes.First().Version);
        }

        [Test]
        public void It_reports_new_methods_as_added()
        {
            var reporter = new Reporter
                           {
                               Assemblies =
                                   {
                                       new AssemblyInfo
                                       {
                                           Name = "MyAssembly",
                                           Version = "1",
                                           Types =
                                               {
                                                   new TypeInfo
                                                   {
                                                       Name = "MyType",
                                                       Members =
                                                           {
                                                               new MemberInfo
                                                               {
                                                                   Name = "MyMethod1",
                                                                   Kind = MemberKind.Method
                                                               }
                                                           }
                                                   }
                                               }
                                       },
                                       new AssemblyInfo
                                       {
                                           Name = "MyAssembly",
                                           Version = "2",
                                           Types =
                                               {
                                                   new TypeInfo
                                                   {
                                                       Name = "MyType",
                                                       Members =
                                                           {
                                                               new MemberInfo
                                                               {
                                                                   Name = "MyMethod1",
                                                                   Kind = MemberKind.Method
                                                               },
                                                               new MemberInfo
                                                               {
                                                                   Name = "MyMethod2",
                                                                   Kind = MemberKind.Method
                                                               }
                                                           }
                                                   }
                                               }
                                       }
                                   }
                           };

            var report = reporter.GenerateReport();

            Assert.AreEqual(MemberChangeKind.Added, report.Types.Single().Members.Single(m => m.Name == "MyMethod2").Changes.Single().Kind);
            Assert.AreEqual("2", report.Types.Single().Members.Single(m => m.Name == "MyMethod2").Changes.Single().Version);
        }

        [Test]
        public void It_reports_missing_methods_as_removed()
        {
            var reporter = new Reporter
                           {
                               Assemblies =
                                   {
                                       new AssemblyInfo
                                       {
                                           Name = "MyAssembly",
                                           Version = "1",
                                           Types =
                                               {
                                                   new TypeInfo
                                                   {
                                                       Name = "MyType",
                                                       Members =
                                                           {
                                                               new MemberInfo
                                                               {
                                                                   Name = "MyMethod1",
                                                                   Kind = MemberKind.Method
                                                               },
                                                               new MemberInfo
                                                               {
                                                                   Name = "MyMethod2",
                                                                   Kind = MemberKind.Method
                                                               }
                                                           }
                                                   }
                                               }
                                       },
                                       new AssemblyInfo
                                       {
                                           Name = "MyAssembly",
                                           Version = "2",
                                           Types =
                                               {
                                                   new TypeInfo
                                                   {
                                                       Name = "MyType",
                                                       Members =
                                                           {
                                                               new MemberInfo
                                                               {
                                                                   Name = "MyMethod1",
                                                                   Kind = MemberKind.Method
                                                               }
                                                           }
                                                   }
                                               }
                                       }
                                   }
                           };

            var report = reporter.GenerateReport();

            Assert.AreEqual(MemberChangeKind.Removed, report.Types.Single().Members.Single(m => m.Name == "MyMethod2").Changes.Single().Kind);
            Assert.AreEqual("2", report.Types.Single().Members.Single(m => m.Name == "MyMethod2").Changes.Single().Version);
        }

        [Test]
        public void It_detects_when_method_parameters_are_added()
        {
            var reporter = new Reporter
                           {
                               Assemblies =
                                   {
                                       new AssemblyInfo
                                       {
                                           Name = "MyAssembly",
                                           Version = "1",
                                           Types =
                                               {
                                                   new TypeInfo
                                                   {
                                                       Name = "MyType",
                                                       Members =
                                                           {
                                                               new MemberInfo
                                                               {
                                                                   Name = "MyMethod",
                                                                   Kind = MemberKind.Method
                                                               }
                                                           }
                                                   }
                                               }
                                       },
                                       new AssemblyInfo
                                       {
                                           Name = "MyAssembly",
                                           Version = "2",
                                           Types =
                                               {
                                                   new TypeInfo
                                                   {
                                                       Name = "MyType",
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
                                                                               Name = "myParam",
                                                                               Type = "System.Int32"
                                                                           }
                                                                       }
                                                               }
                                                           }
                                                   }
                                               }
                                       }
                                   }
                           };

            var report = reporter.GenerateReport();

            Assert.AreEqual(MemberChangeKind.AddedParameter, report.Types.Single().Members.Get("MyMethod").Changes.Single().Kind);
            Assert.AreEqual("2", report.Types.Single().Members.Get("MyMethod").Changes.Single().Version);
            Assert.AreEqual("myParam", report.Types.Single().Members.Get("MyMethod").Changes.Single().New);
        }

        [Test]
        public void It_detects_when_method_parameters_are_removed()
        {
            var reporter = new Reporter
                           {
                               Assemblies =
                                   {
                                       new AssemblyInfo
                                       {
                                           Name = "MyAssembly",
                                           Version = "1",
                                           Types =
                                               {
                                                   new TypeInfo
                                                   {
                                                       Name = "MyType",
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
                                                                               Name = "myParam",
                                                                               Type = "System.Int32"
                                                                           }
                                                                       }
                                                               }
                                                           }
                                                   }
                                               }
                                       },
                                       new AssemblyInfo
                                       {
                                           Name = "MyAssembly",
                                           Version = "2",
                                           Types =
                                               {
                                                   new TypeInfo
                                                   {
                                                       Name = "MyType",
                                                       Members =
                                                           {
                                                               new MemberInfo
                                                               {
                                                                   Name = "MyMethod",
                                                                   Kind = MemberKind.Method
                                                               }
                                                           }
                                                   }
                                               }
                                       }
                                   }
                           };

            var report = reporter.GenerateReport();

            Assert.AreEqual(MemberChangeKind.RemovedParameter, report.Types.Single().Members.Get("MyMethod").Changes.Single().Kind);
            Assert.AreEqual("2", report.Types.Single().Members.Get("MyMethod").Changes.Single().Version);
            Assert.AreEqual("myParam", report.Types.Single().Members.Get("MyMethod").Changes.Single().Old);
        }
    }
}