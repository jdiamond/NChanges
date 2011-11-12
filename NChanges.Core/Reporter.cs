using System.Collections.Generic;
using System.Linq;

namespace NChanges.Core
{
    public class Reporter
    {
        private readonly List<AssemblyInfo> _assemblies = new List<AssemblyInfo>();

        public ICollection<AssemblyInfo> Assemblies { get { return _assemblies; } }

        public AssemblyInfo GenerateReport()
        {
            var report = new AssemblyInfo();

            AssemblyInfo previousAssembly = null;

            foreach (var assembly in Assemblies.OrderBy(a => a.Version.PadNumbers()))
            {
                report.Name = assembly.Name;
                report.Version = assembly.Version;

                FindAddedTypes(report, assembly, previousAssembly);
                FindRemovedTypes(report, assembly, previousAssembly);
                UpdateExistingTypes(report, assembly, previousAssembly);
                UpdateExistingTypeMembers(report, assembly, previousAssembly);

                previousAssembly = assembly;
            }

            return report;
        }

        private static void FindAddedTypes(AssemblyInfo report, AssemblyInfo thisAssembly, AssemblyInfo previousAssembly)
        {
            foreach (var type in thisAssembly.Types)
            {
                if (report.Types.Contains(type.Name))
                {
                    continue;
                }

                var newType = type.Clone();

                if (previousAssembly != null)
                {
                    newType.Changes.Add(new TypeChangeInfo
                                        {
                                            Kind = TypeChangeKind.Added,
                                            Version = thisAssembly.Version
                                        });
                }

                report.Types.Add(newType);
            }
        }

        private static void FindRemovedTypes(AssemblyInfo report, AssemblyInfo thisAssembly, AssemblyInfo previousAssembly)
        {
            if (previousAssembly != null)
            {
                foreach (var type in previousAssembly.Types)
                {
                    if (!thisAssembly.Types.Contains(type.Name))
                    {
                        report.Types.Get(type.Name).Changes.Add(new TypeChangeInfo
                                                                {
                                                                    Kind = TypeChangeKind.Removed,
                                                                    Version = thisAssembly.Version
                                                                });
                    }
                }
            }
        }

        private static void UpdateExistingTypes(AssemblyInfo report, AssemblyInfo thisAssembly, AssemblyInfo previousAssembly)
        {
            if (previousAssembly != null)
            {
                foreach (var type in previousAssembly.Types)
                {
                    if (previousAssembly.Types.Contains(type.Name) &&
                        thisAssembly.Types.Contains(type.Name))
                    {
                        var previousType = previousAssembly.Types.Get(type.Name);
                        var thisType = thisAssembly.Types.Get(type.Name);

                        // Did the type just become obsolete?
                        if (!previousType.Obsolete && thisType.Obsolete)
                        {
                            report.Types.Get(type.Name).Changes.Add(
                                new TypeChangeInfo
                                {
                                    Kind = TypeChangeKind.Obsoleted,
                                    Version = thisAssembly.Version
                                });
                        }
                    }
                }
            }
        }

        private static void UpdateExistingTypeMembers(AssemblyInfo report, AssemblyInfo thisAssembly, AssemblyInfo previousAssembly)
        {
            if (previousAssembly != null) 
            {
                foreach (var type in report.Types)
                {
                    if (previousAssembly.Types.Contains(type.Name) &&
                        thisAssembly.Types.Contains(type.Name))
                    {
                        var previousType = previousAssembly.Types.Get(type.Name);
                        var thisType = thisAssembly.Types.Get(type.Name);

                        foreach (var member in thisType.Members)
                        {
                            // Is this a new member?
                            if (!previousType.Members.Contains(member.Name))
                            {
                                var newMember = member.Clone();

                                newMember.Changes.Add(new MemberChangeInfo
                                                      {
                                                          Kind = MemberChangeKind.Added,
                                                          Version = thisAssembly.Version
                                                      });

                                type.Members.Add(newMember);
                            }
                            else
                            {
                                // The member is old. Check to see if it was modified.
                                var oldMember = previousType.Members.Get(member);

                                // Did the member just become obsolete?
                                if (!oldMember.Obsolete && member.Obsolete)
                                {
                                    type.Members.Get(member.Name).Changes.Add(
                                        new MemberChangeInfo
                                        {
                                            Kind = MemberChangeKind.Obsoleted,
                                            Version = thisAssembly.Version
                                        });
                                }

                                if (!type.Members.IsOverloaded(member.Name))
                                {
                                    if (oldMember.Parameters.Count < member.Parameters.Count)
                                    {
                                        for (var i = oldMember.Parameters.Count; i < member.Parameters.Count; i++)
                                        {
                                            var typeMember = type.Members.Get(member.Name);

                                            typeMember.Changes.Add(new MemberChangeInfo
                                                                   {
                                                                       Kind = MemberChangeKind.AddedParameter,
                                                                       Version = thisAssembly.Version,
                                                                       New = member.Parameters[i].Name
                                                                   });

                                            typeMember.UpdateParameters(member);
                                        }
                                    }
                                    else if (oldMember.Parameters.Count > member.Parameters.Count)
                                    {
                                        for (var i = member.Parameters.Count; i < oldMember.Parameters.Count; i++)
                                        {
                                            var typeMember = type.Members.Get(member.Name);

                                            typeMember.Changes.Add(new MemberChangeInfo
                                                                   {
                                                                       Kind = MemberChangeKind.RemovedParameter,
                                                                       Version = thisAssembly.Version,
                                                                       Old = oldMember.Parameters[i].Name
                                                                   });

                                            typeMember.UpdateParameters(member);
                                        }
                                    }
                                }
                            }
                        }

                        // Look for removed members.
                        foreach (var member in previousType.Members)
                        {
                            if (!thisType.Members.Contains(member.Name))
                            {
                                var removedMember = type.Members.Get(member);

                                removedMember.Changes.Add(new MemberChangeInfo
                                                          {
                                                              Kind = MemberChangeKind.Removed,
                                                              Version = thisAssembly.Version
                                                          });
                            }
                        }
                    }
                }
            }
        }
    }
}