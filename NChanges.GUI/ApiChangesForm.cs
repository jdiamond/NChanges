using System;
using System.Linq;
using System.Threading;
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
                TxtbxSelectedAssemblies.Text = string.Join(" ", openFileDialog1.SafeFileNames);
                BtnCreateSnapshots.Enabled = true;
            }
        }

        private void BtnCreateSnapshots_Click(object sender, EventArgs e)
        {
            if (TxtbxVersion.Text != string.Empty)
            {
                var snapshotNames = new string[openFileDialog1.SafeFileNames.Count()];

                for (var i = 0; i < openFileDialog1.SafeFileNames.Count(); i++)
                {
                    snapshotNames[i] = openFileDialog1.SafeFileNames[i].Substring(0, openFileDialog1.SafeFileNames[i].Length - 4) +
                                       "-" +
                                       TxtbxVersion.Text +
                                       ".xml";

                    var strCmdLine = @"snapshot " + openFileDialog1.FileNames[i] + " -v=" + TxtbxVersion.Text;

                    System.Diagnostics.Process.Start(N_CHANGES_EXE_PATH, strCmdLine);
                }

                TxtbxSnapshotsCreated.Text = string.Join(" ", snapshotNames);
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
            openFileDialog1.Filter = @"XML (*.xml)|*.xml";
            openFileDialog1.Multiselect = true;
            //openFileDialog1.InitialDirectory = "";

            var result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                TxtbxSelectedSnapshots.Text = string.Join(" ", openFileDialog1.SafeFileNames);
                btnExportToExcel.Enabled = true;
            }
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            CreateReport();
            Thread.Sleep(300);
            ExportReportToExcel();
        }

        private void CreateReport()
        {
            try
            {
                var cmdLineParams = @"report " + string.Join(" ", openFileDialog1.SafeFileNames);

                System.Diagnostics.Process.Start(N_CHANGES_EXE_PATH, cmdLineParams);
            }
            catch (Exception e)
            {
                LblExportError.Text = e.Message;
                LblExportError.Visible = true;
            }
        }

        private void ExportReportToExcel()
        {
            try
            {
                var olderReportName = openFileDialog1.SafeFileNames.Last();
                var reportName = olderReportName.Substring(0, olderReportName.Length - 4) +
                                 "-report.xml";
                var cmdLineParams = @"export " + reportName;

                System.Diagnostics.Process.Start(N_CHANGES_EXE_PATH, cmdLineParams);

                txtExcelName.Text = reportName.Substring(0, reportName.Length - 3) + "xls";
                LblExportError.Visible = false;

            }
            catch (Exception e)
            {
                LblExportError.Text = e.Message;
                LblExportError.Visible = true;
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
    }
}
