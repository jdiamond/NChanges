using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace NChanges.GUI
{
    public class Settings : ApplicationSettingsBase
    {
        private static readonly Settings _default = new Settings();

        private const int MaxRecentProjects = 10;

        public static Settings Default
        {
            get { return _default; }
        }

        [UserScopedSetting]
        public List<string> RecentProjects
        {
            get { return (List<string>)this["RecentProjects"]; }
            set { this["RecentProjects"] = value; }
        }

        public void AddRecentProject(string path)
        {
            var projects = RecentProjects ?? new List<string>();
            projects.Remove(path);
            projects.Insert(0, path);
            RecentProjects = projects.Take(MaxRecentProjects).ToList();
            Save();
        }

        public void ClearRecentProjects()
        {
            RecentProjects = null;
            Save();
        }
    }
}