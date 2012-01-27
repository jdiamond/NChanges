using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using NChanges.Core;

namespace NChanges.GUI
{
    public partial class ApiChangesForm : Form
    {
        private const string NCHANGES_TOOL_EXE = "NChanges.Tool.exe";
        private const string TITLE_SUFFIX = "NChanges GUI";
        private const string TITLE_SEPARATOR = " - ";
        private const string PROJECT_FILTER = "NChanges Files (*.nchanges)|*.nchanges|MSBuild Files (*.msbuild)|*.msbuld|All Files (*.*)|*.*";
        private const string ASSEMBLY_FILTER = "Assembly Files (*.dll)|*.dll";

        private string _currentProjectPath;
        private bool _dirty = false;

        public ApiChangesForm()
        {
            InitializeComponent();
            UpdateRecentProjects();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var d = new OpenFileDialog();
            SetFileDialogProperties(d);

            var result = d.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                LoadProject(d.FileName);
            }
        }

        private void closeProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateGUI(new Project());
            _currentProjectPath = null;
            SetTitle();
        }

        private void saveProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_currentProjectPath == null)
            {
                var d = new SaveFileDialog();
                SetFileDialogProperties(d);

                var result = d.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    _currentProjectPath = d.FileName;
                }
            }

            if (_currentProjectPath != null)
            {
                SaveProject(_currentProjectPath);
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.ClearRecentProjects();
            UpdateRecentProjects();
        }

        private static void SetFileDialogProperties(FileDialog dialog)
        {
            dialog.Filter = PROJECT_FILTER;

            var recentProjects = Settings.Default.RecentProjects;

            if (recentProjects != null && recentProjects.Count > 0)
            {
                dialog.InitialDirectory = Path.GetDirectoryName(recentProjects[0]);
            }
        }

        private void LoadProject(string path)
        {
            var project = new Project();
            project.ReadXml(path);

            UpdateGUI(project);

            Settings.Default.AddRecentProject(path);

            UpdateRecentProjects();

            _currentProjectPath = path;

            SetTitle();

            _dirty = false;
        }

        private void UpdateGUI(Project project)
        {
            foreach (var assemblyToSnapshot in project.AssembliesToSnapshot)
            {
                var item = new ListViewItem(new []
                                            {
                                                assemblyToSnapshot.Path,
                                                assemblyToSnapshot.Version
                                            });

                assembliesListView.Items.Add(item);
            }

            txtTypesToExclude.Text = project.TypesToExcludePattern;
            txtExcelOutput.Text = project.ExcelOutputPath;
        }

        private void SaveProject(string path)
        {
            var project = new Project
            {
                NChangesToolPath = GetNChangesToolPath(),
                TypesToExcludePattern = txtTypesToExclude.Text,
                ExcelOutputPath = txtExcelOutput.Text,
            };

            foreach (var assemblyToSnapshot in assembliesListView
                                                    .Items
                                                    .Cast<ListViewItem>()
                                                    .Select(i => new AssemblyToSnapshot
                                                    {
                                                        Path = i.SubItems[0].Text,
                                                        Version = i.SubItems[1].Text
                                                    }))
            {
                project.AssembliesToSnapshot.Add(assemblyToSnapshot);
            }

            project.WriteXml(path);

            Settings.Default.AddRecentProject(path);

            UpdateRecentProjects();

            SetTitle();

            _dirty = false;
        }

        private void UpdateRecentProjects()
        {
            var projects = Settings.Default.RecentProjects;

            recentProjectsToolStripMenuItem.DropDownItems.Clear();

            if (projects != null)
            {
                foreach (var project in projects)
                {
                    var item = new ToolStripMenuItem
                               {
                                   Text = project
                               };

                    item.Click += (s, e) => LoadProject(item.Text);

                    recentProjectsToolStripMenuItem.DropDownItems.Add(item);
                }
            }
            else
            {
                recentProjectsToolStripMenuItem.DropDownItems.Add(new ToolStripMenuItem
                                                                  {
                                                                      Text = "(empty)",
                                                                      Enabled = false
                                                                  });
            }

            recentProjectsToolStripMenuItem.DropDownItems.Add(recentProjectsToolStripSeparator);
            recentProjectsToolStripMenuItem.DropDownItems.Add(clearToolStripMenuItem);
        }

        private void SetTitle()
        {
            if (_currentProjectPath == null)
            {
                Text = TITLE_SUFFIX;
            }
            else
            {
                Text = string.Format("{0}{1}{2}", Path.GetFileNameWithoutExtension(_currentProjectPath), TITLE_SEPARATOR, TITLE_SUFFIX);
            }
        }

        private string GetNChangesToolPath()
        {
            string dir = Path.GetDirectoryName(Application.ExecutablePath);
            return Path.Combine(dir, NCHANGES_TOOL_EXE);
        }

        private void addAssemblyButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = ASSEMBLY_FILTER;

            var result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                assembliesListView.Items.Add(new ListViewItem(new[]
                                                              {
                                                                  openFileDialog1.FileName,
                                                                  ""
                                                              }));

                _dirty = true;
            }
        }

        private void editPathButton_Click(object sender, EventArgs e)
        {
            EditAssemblySubItem(0, "Assembly Path");
        }

        private void editVersionButton_Click(object sender, EventArgs e)
        {
            EditAssemblySubItem(1, "Assembly Version");
        }

        private void EditAssemblySubItem(int subItemIndex, string prompt)
        {
            if (assembliesListView.SelectedItems.Count == 0)
            {
                return;
            }

            var first = assembliesListView.SelectedItems[0];

            string value = first.SubItems[subItemIndex].Text;

            if (InputBox.Show(TITLE_SUFFIX, prompt, ref value) == DialogResult.OK)
            {
                foreach (ListViewItem item in assembliesListView.SelectedItems)
                {
                    item.SubItems[subItemIndex].Text = value;
                }

                _dirty = true;
            }
        }

        private void removeAssemblyButton_Click(object sender, EventArgs e)
        {
            if (assembliesListView.SelectedItems.Count == 0)
            {
                return;
            }

            var itemsToRemove = new List<ListViewItem>(assembliesListView.SelectedItems.Cast<ListViewItem>());

            foreach (var item in itemsToRemove)
            {
                assembliesListView.Items.Remove(item);

                _dirty = true;
            }
        }

        private void btnCreateSnapshots_Click(object sender, EventArgs e)
        {
            RunTask(Project.SnapshotTaskName);
        }

        private void btnCreateReports_Click(object sender, EventArgs e)
        {
            RunTask(Project.ReportTaskName);
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            RunTask(Project.ExcelTaskName);
        }

        private void RunTask(string taskName)
        {
            if (VerifyProject())
            {
                Project.Run(_currentProjectPath, taskName);
            }
        }

        private bool VerifyProject()
        {
            if (_currentProjectPath == null)
            {
                MessageBox.Show(
                    "You need to open or save a project first...",
                    "Sorry!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            if (_dirty)
            {
                SaveProject(_currentProjectPath);
            }

            return true;
        }

        private void btnViewExcelOutput_Click(object sender, EventArgs e)
        {
            if (VerifyProject())
            {
                var path = Path.Combine(Path.GetDirectoryName(_currentProjectPath), txtExcelOutput.Text);
                Process.Start(path);
            }
        }

        private void txtTypesToExclude_TextChanged(object sender, EventArgs e)
        {
            _dirty = true;
        }

        private void txtExcelOutput_TextChanged(object sender, EventArgs e)
        {
            _dirty = true;
        }
    }
}