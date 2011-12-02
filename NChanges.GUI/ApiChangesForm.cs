using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace NChanges.GUI
{
    public partial class ApiChangesForm : Form
    {
        private const string N_CHANGES_EXE_PATH = @"NChanges.Tool.exe";

        public ApiChangesForm()
        {
            InitializeComponent();
        }

        private void BtnSelectAssemblies_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = @"Assemblies (*.dll)|*.dll";
            openFileDialog1.Multiselect = true;

            var result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                TxtbxSelectedAssemblies.Text = string.Join("\r\n", openFileDialog1.SafeFileNames);
                TxtbxSnapshotLocation.Text = Directory.GetCurrentDirectory();
                BtnCreateSnapshots.Enabled = true;
            }
        }

        private void BtnCreateSnapshots_Click(object sender, EventArgs e)
        {
            if (TxtbxVersion.Text != string.Empty)
            {
                var snapshotNames = new List<string>();

                foreach (var fileName in openFileDialog1.FileNames)
                {
                    var snapshotName = TxtbxSnapshotLocation.Text + 
                                       @"\" +
                                       Path.GetFileNameWithoutExtension(fileName) +
                                       "-" +
                                       TxtbxVersion.Text +
                                       "-snapshot.xml";
                    snapshotNames.Add(snapshotName);

                    var strCmdLine = @"snapshot " + 
                                     fileName + 
                                     " -v=" + 
                                     TxtbxVersion.Text +
                                     @" -o=" +
                                     snapshotName;

                    if (!string.IsNullOrEmpty(TxtbxTypesToExclude.Text))
                    {
                        strCmdLine = strCmdLine + " -x=" + TxtbxTypesToExclude.Text;
                    }

                    System.Diagnostics.Process.Start(N_CHANGES_EXE_PATH, strCmdLine);
                }

                TxtbxSnapshotsCreated.Text = string.Join("\r\n", snapshotNames.Select(Path.GetFileName).ToArray());
                TxtbxVersion.Text = string.Empty;
                LblSnapshotError.Visible = false;
            }
            else
            {
                LblSnapshotError.Text = "An assembly version is required.";
                LblSnapshotError.Visible = true;
            }
        }

        private void BtnSelectSnapshots_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = @"XML (*-snapshot.xml)|*-snapshot.xml";
            openFileDialog1.Multiselect = true;
            openFileDialog1.InitialDirectory = TxtbxSnapshotLocation.Text;

            var result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                TxtbxSelectedSnapshots.Text = string.Join(" ", openFileDialog1.SafeFileNames);
                BtnCreateReports.Enabled = true;
            }
        }

        private void BtnExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                var cmdLineParams = @"excel " + string.Join(" ", openFileDialog1.FileNames);

                System.Diagnostics.Process.Start(N_CHANGES_EXE_PATH, cmdLineParams);

                TxtbxExcelNames.Text = Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(openFileDialog1.SafeFileNames.First())) + ".xls";

            }
            catch (Exception ex)
            {
                LblReportError.Text = ex.Message;
                LblReportError.Visible = true;
            }
        }
        
        private void BtnOpenExcelReports_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = @"Excel (*.xls)|*.xls";
            openFileDialog1.Multiselect = true;

            var result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                foreach (var name in openFileDialog1.SafeFileNames)
                {
                    System.Diagnostics.Process.Start(name);
                }
            }
        }

        private void BtnSaveSnapshotLocation_Click(object sender, EventArgs e)
        {
            var result = folderBrowserDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                TxtbxSnapshotLocation.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void BtnCreateReports_Click(object sender, EventArgs e)
        {
            try
            {
                var cmdLineParams = @"report " + string.Join(" ", openFileDialog1.FileNames);

                System.Diagnostics.Process.Start(N_CHANGES_EXE_PATH, cmdLineParams);

                TxtbxReportNamesCreated.Text = string.Join("\r\n", openFileDialog1.SafeFileNames.Select(i => i.Replace("snapshot", "report")));
                LblReportError.Visible = false;
            }
            catch (Exception ex)
            {
                LblReportError.Text = ex.Message;
                LblReportError.Visible = true;
            }
        }

        private void BtnSelectReports_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = @"XML (*-report.xml)|*-report.xml";
            openFileDialog1.Multiselect = true;
            openFileDialog1.InitialDirectory = TxtbxSnapshotLocation.Text;

            var result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                TxtbxReportNamesSelected.Text = string.Join(" ", openFileDialog1.SafeFileNames);

                BtnExportToExcel.Enabled = true;
            }
        }
    }
}
