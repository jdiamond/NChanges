namespace NChanges.GUI
{
    partial class ApiChangesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.TxtbxSnapshotLocation = new System.Windows.Forms.TextBox();
            this.BtnSaveSnapshotLocation = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.LblSnapshotError = new System.Windows.Forms.Label();
            this.TxtbxSelectedAssemblies = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TxtbxSnapshotsCreated = new System.Windows.Forms.TextBox();
            this.BtnCreateSnapshots = new System.Windows.Forms.Button();
            this.BtnSelectAssemblies = new System.Windows.Forms.Button();
            this.TxtbxVersion = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label9 = new System.Windows.Forms.Label();
            this.TxtbxReportNamesCreated = new System.Windows.Forms.TextBox();
            this.LblReportError = new System.Windows.Forms.Label();
            this.BtnCreateReports = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.TxtbxSelectedSnapshots = new System.Windows.Forms.TextBox();
            this.BtnSelectSnapshots = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.TxtbxExcelNames = new System.Windows.Forms.TextBox();
            this.BtnExportToExcel = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.TxtbxReportNamesSelected = new System.Windows.Forms.TextBox();
            this.BtnSelectReports = new System.Windows.Forms.Button();
            this.BtnOpenExcelReports = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(603, 358);
            this.tabControl1.TabIndex = 23;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.TxtbxSnapshotLocation);
            this.tabPage1.Controls.Add(this.BtnSaveSnapshotLocation);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.LblSnapshotError);
            this.tabPage1.Controls.Add(this.TxtbxSelectedAssemblies);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.TxtbxSnapshotsCreated);
            this.tabPage1.Controls.Add(this.BtnCreateSnapshots);
            this.tabPage1.Controls.Add(this.BtnSelectAssemblies);
            this.tabPage1.Controls.Add(this.TxtbxVersion);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(595, 332);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Snapshot";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // TxtbxSnapshotLocation
            // 
            this.TxtbxSnapshotLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtbxSnapshotLocation.Location = new System.Drawing.Point(107, 194);
            this.TxtbxSnapshotLocation.Name = "TxtbxSnapshotLocation";
            this.TxtbxSnapshotLocation.ReadOnly = true;
            this.TxtbxSnapshotLocation.Size = new System.Drawing.Size(345, 20);
            this.TxtbxSnapshotLocation.TabIndex = 40;
            // 
            // BtnSaveSnapshotLocation
            // 
            this.BtnSaveSnapshotLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSaveSnapshotLocation.Location = new System.Drawing.Point(458, 194);
            this.BtnSaveSnapshotLocation.Name = "BtnSaveSnapshotLocation";
            this.BtnSaveSnapshotLocation.Size = new System.Drawing.Size(122, 23);
            this.BtnSaveSnapshotLocation.TabIndex = 39;
            this.BtnSaveSnapshotLocation.Text = "Select Save Location";
            this.BtnSaveSnapshotLocation.UseVisualStyleBackColor = true;
            this.BtnSaveSnapshotLocation.Click += new System.EventHandler(this.BtnSaveSnapshotLocation_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(10, 197);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(95, 13);
            this.label12.TabIndex = 38;
            this.label12.Text = "Snapshot location:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(104, 53);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(480, 13);
            this.label11.TabIndex = 37;
            this.label11.Text = "       Then repeat this step for the assemblies in Version 62.18.0.0 before gener" +
                "ating a report in Excel.";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(104, 37);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(384, 13);
            this.label10.TabIndex = 36;
            this.label10.Text = "ex.: Select all the assemblies in Verion 62.17.0.0 and then create the snapshots." +
                "";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(211, 151);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 35;
            this.label5.Text = "ex.: 62.18.0.0";
            // 
            // LblSnapshotError
            // 
            this.LblSnapshotError.AutoSize = true;
            this.LblSnapshotError.ForeColor = System.Drawing.Color.Crimson;
            this.LblSnapshotError.Location = new System.Drawing.Point(235, 240);
            this.LblSnapshotError.Name = "LblSnapshotError";
            this.LblSnapshotError.Size = new System.Drawing.Size(32, 13);
            this.LblSnapshotError.TabIndex = 34;
            this.LblSnapshotError.Text = "Error:";
            this.LblSnapshotError.Visible = false;
            // 
            // TxtbxSelectedAssemblies
            // 
            this.TxtbxSelectedAssemblies.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtbxSelectedAssemblies.Location = new System.Drawing.Point(106, 73);
            this.TxtbxSelectedAssemblies.Multiline = true;
            this.TxtbxSelectedAssemblies.Name = "TxtbxSelectedAssemblies";
            this.TxtbxSelectedAssemblies.ReadOnly = true;
            this.TxtbxSelectedAssemblies.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TxtbxSelectedAssemblies.Size = new System.Drawing.Size(474, 52);
            this.TxtbxSelectedAssemblies.TabIndex = 33;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 32;
            this.label4.Text = "Assembly Names:";
            // 
            // TxtbxSnapshotsCreated
            // 
            this.TxtbxSnapshotsCreated.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtbxSnapshotsCreated.Location = new System.Drawing.Point(106, 264);
            this.TxtbxSnapshotsCreated.Multiline = true;
            this.TxtbxSnapshotsCreated.Name = "TxtbxSnapshotsCreated";
            this.TxtbxSnapshotsCreated.ReadOnly = true;
            this.TxtbxSnapshotsCreated.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TxtbxSnapshotsCreated.Size = new System.Drawing.Size(474, 52);
            this.TxtbxSnapshotsCreated.TabIndex = 31;
            this.TxtbxSnapshotsCreated.WordWrap = false;
            // 
            // BtnCreateSnapshots
            // 
            this.BtnCreateSnapshots.Enabled = false;
            this.BtnCreateSnapshots.Location = new System.Drawing.Point(105, 235);
            this.BtnCreateSnapshots.Name = "BtnCreateSnapshots";
            this.BtnCreateSnapshots.Size = new System.Drawing.Size(124, 23);
            this.BtnCreateSnapshots.TabIndex = 27;
            this.BtnCreateSnapshots.Text = "Create Snapshots";
            this.BtnCreateSnapshots.UseVisualStyleBackColor = true;
            this.BtnCreateSnapshots.Click += new System.EventHandler(this.BtnCreateSnapshots_Click);
            // 
            // BtnSelectAssemblies
            // 
            this.BtnSelectAssemblies.Location = new System.Drawing.Point(106, 11);
            this.BtnSelectAssemblies.Name = "BtnSelectAssemblies";
            this.BtnSelectAssemblies.Size = new System.Drawing.Size(124, 23);
            this.BtnSelectAssemblies.TabIndex = 26;
            this.BtnSelectAssemblies.Text = "Select Assemblies";
            this.BtnSelectAssemblies.UseVisualStyleBackColor = true;
            this.BtnSelectAssemblies.Click += new System.EventHandler(this.BtnSelectAssemblies_Click);
            // 
            // TxtbxVersion
            // 
            this.TxtbxVersion.Location = new System.Drawing.Point(105, 148);
            this.TxtbxVersion.Name = "TxtbxVersion";
            this.TxtbxVersion.Size = new System.Drawing.Size(100, 20);
            this.TxtbxVersion.TabIndex = 28;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 151);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 29;
            this.label1.Text = "Assembly Version:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 264);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 30;
            this.label2.Text = "Snapshot Names:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.TxtbxReportNamesCreated);
            this.tabPage2.Controls.Add(this.LblReportError);
            this.tabPage2.Controls.Add(this.BtnCreateReports);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.TxtbxSelectedSnapshots);
            this.tabPage2.Controls.Add(this.BtnSelectSnapshots);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(595, 332);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Report";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 262);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(78, 13);
            this.label9.TabIndex = 41;
            this.label9.Text = "Report Names:";
            // 
            // TxtbxReportNamesCreated
            // 
            this.TxtbxReportNamesCreated.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtbxReportNamesCreated.Location = new System.Drawing.Point(106, 226);
            this.TxtbxReportNamesCreated.Multiline = true;
            this.TxtbxReportNamesCreated.Name = "TxtbxReportNamesCreated";
            this.TxtbxReportNamesCreated.ReadOnly = true;
            this.TxtbxReportNamesCreated.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TxtbxReportNamesCreated.Size = new System.Drawing.Size(476, 90);
            this.TxtbxReportNamesCreated.TabIndex = 40;
            // 
            // LblReportError
            // 
            this.LblReportError.AutoSize = true;
            this.LblReportError.ForeColor = System.Drawing.Color.Crimson;
            this.LblReportError.Location = new System.Drawing.Point(235, 202);
            this.LblReportError.Name = "LblReportError";
            this.LblReportError.Size = new System.Drawing.Size(32, 13);
            this.LblReportError.TabIndex = 39;
            this.LblReportError.Text = "Error:";
            this.LblReportError.Visible = false;
            // 
            // BtnCreateReports
            // 
            this.BtnCreateReports.Enabled = false;
            this.BtnCreateReports.Location = new System.Drawing.Point(105, 197);
            this.BtnCreateReports.Name = "BtnCreateReports";
            this.BtnCreateReports.Size = new System.Drawing.Size(124, 23);
            this.BtnCreateReports.TabIndex = 38;
            this.BtnCreateReports.Text = "Create Reports";
            this.BtnCreateReports.UseVisualStyleBackColor = true;
            this.BtnCreateReports.Click += new System.EventHandler(this.BtnCreateReports_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(105, 60);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(210, 13);
            this.label8.TabIndex = 37;
            this.label8.Text = "       Acme.Example-62.18.0.0-snapshot.xml";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(106, 42);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(209, 13);
            this.label7.TabIndex = 36;
            this.label7.Text = "ex.: Acme.Example-62.17.0.0-snapshot.xml";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 80);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 13);
            this.label6.TabIndex = 35;
            this.label6.Text = "Snapshot Names:";
            // 
            // TxtbxSelectedSnapshots
            // 
            this.TxtbxSelectedSnapshots.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtbxSelectedSnapshots.Location = new System.Drawing.Point(106, 80);
            this.TxtbxSelectedSnapshots.Multiline = true;
            this.TxtbxSelectedSnapshots.Name = "TxtbxSelectedSnapshots";
            this.TxtbxSelectedSnapshots.ReadOnly = true;
            this.TxtbxSelectedSnapshots.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TxtbxSelectedSnapshots.Size = new System.Drawing.Size(476, 90);
            this.TxtbxSelectedSnapshots.TabIndex = 33;
            // 
            // BtnSelectSnapshots
            // 
            this.BtnSelectSnapshots.Location = new System.Drawing.Point(105, 15);
            this.BtnSelectSnapshots.Name = "BtnSelectSnapshots";
            this.BtnSelectSnapshots.Size = new System.Drawing.Size(124, 23);
            this.BtnSelectSnapshots.TabIndex = 34;
            this.BtnSelectSnapshots.Text = "Select Snapshots";
            this.BtnSelectSnapshots.UseVisualStyleBackColor = true;
            this.BtnSelectSnapshots.Click += new System.EventHandler(this.BtnSelectSnapshots_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.TxtbxExcelNames);
            this.tabPage3.Controls.Add(this.BtnExportToExcel);
            this.tabPage3.Controls.Add(this.label13);
            this.tabPage3.Controls.Add(this.label14);
            this.tabPage3.Controls.Add(this.label15);
            this.tabPage3.Controls.Add(this.TxtbxReportNamesSelected);
            this.tabPage3.Controls.Add(this.BtnSelectReports);
            this.tabPage3.Controls.Add(this.BtnOpenExcelReports);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(595, 332);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Export To Excel";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // TxtbxExcelNames
            // 
            this.TxtbxExcelNames.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtbxExcelNames.Location = new System.Drawing.Point(102, 203);
            this.TxtbxExcelNames.Multiline = true;
            this.TxtbxExcelNames.Name = "TxtbxExcelNames";
            this.TxtbxExcelNames.ReadOnly = true;
            this.TxtbxExcelNames.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TxtbxExcelNames.Size = new System.Drawing.Size(476, 70);
            this.TxtbxExcelNames.TabIndex = 44;
            // 
            // BtnExportToExcel
            // 
            this.BtnExportToExcel.Enabled = false;
            this.BtnExportToExcel.Location = new System.Drawing.Point(101, 174);
            this.BtnExportToExcel.Name = "BtnExportToExcel";
            this.BtnExportToExcel.Size = new System.Drawing.Size(124, 23);
            this.BtnExportToExcel.TabIndex = 43;
            this.BtnExportToExcel.Text = "Export To Excel";
            this.BtnExportToExcel.UseVisualStyleBackColor = true;
            this.BtnExportToExcel.Click += new System.EventHandler(this.BtnExportToExcel_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(101, 60);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(175, 13);
            this.label13.TabIndex = 42;
            this.label13.Text = "       Acme.Test-62.18.0.0-report.xml";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(102, 42);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(193, 13);
            this.label14.TabIndex = 41;
            this.label14.Text = "ex.: Acme.Example-62.18.0.0-report.xml";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(12, 80);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(78, 13);
            this.label15.TabIndex = 40;
            this.label15.Text = "Report Names:";
            // 
            // TxtbxReportNamesSelected
            // 
            this.TxtbxReportNamesSelected.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtbxReportNamesSelected.Location = new System.Drawing.Point(102, 80);
            this.TxtbxReportNamesSelected.Multiline = true;
            this.TxtbxReportNamesSelected.Name = "TxtbxReportNamesSelected";
            this.TxtbxReportNamesSelected.ReadOnly = true;
            this.TxtbxReportNamesSelected.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TxtbxReportNamesSelected.Size = new System.Drawing.Size(476, 70);
            this.TxtbxReportNamesSelected.TabIndex = 38;
            // 
            // BtnSelectReports
            // 
            this.BtnSelectReports.Location = new System.Drawing.Point(101, 15);
            this.BtnSelectReports.Name = "BtnSelectReports";
            this.BtnSelectReports.Size = new System.Drawing.Size(124, 23);
            this.BtnSelectReports.TabIndex = 39;
            this.BtnSelectReports.Text = "Select Reports";
            this.BtnSelectReports.UseVisualStyleBackColor = true;
            this.BtnSelectReports.Click += new System.EventHandler(this.BtnSelectReports_Click);
            // 
            // BtnOpenExcelReports
            // 
            this.BtnOpenExcelReports.Location = new System.Drawing.Point(101, 296);
            this.BtnOpenExcelReports.Name = "BtnOpenExcelReports";
            this.BtnOpenExcelReports.Size = new System.Drawing.Size(124, 23);
            this.BtnOpenExcelReports.TabIndex = 29;
            this.BtnOpenExcelReports.Text = "Open Excel Reports";
            this.BtnOpenExcelReports.UseVisualStyleBackColor = true;
            this.BtnOpenExcelReports.Click += new System.EventHandler(this.BtnOpenExcelReports_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 203);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 25;
            this.label3.Text = "Excel Names:";
            // 
            // ApiChangesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(626, 383);
            this.Controls.Add(this.tabControl1);
            this.Name = "ApiChangesForm";
            this.Text = "NChanges GUI";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox TxtbxSnapshotLocation;
        private System.Windows.Forms.Button BtnSaveSnapshotLocation;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label LblSnapshotError;
        private System.Windows.Forms.TextBox TxtbxSelectedAssemblies;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TxtbxSnapshotsCreated;
        private System.Windows.Forms.Button BtnCreateSnapshots;
        private System.Windows.Forms.Button BtnSelectAssemblies;
        private System.Windows.Forms.TextBox TxtbxVersion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox TxtbxReportNamesCreated;
        private System.Windows.Forms.Label LblReportError;
        private System.Windows.Forms.Button BtnCreateReports;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TxtbxSelectedSnapshots;
        private System.Windows.Forms.Button BtnSelectSnapshots;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox TxtbxReportNamesSelected;
        private System.Windows.Forms.Button BtnSelectReports;
        private System.Windows.Forms.Button BtnOpenExcelReports;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TxtbxExcelNames;
        private System.Windows.Forms.Button BtnExportToExcel;
    }
}

