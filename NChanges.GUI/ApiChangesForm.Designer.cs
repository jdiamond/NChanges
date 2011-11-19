﻿namespace NChanges.GUI
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
            this.BtnSelectAssemblies = new System.Windows.Forms.Button();
            this.BtnCreateSnapshots = new System.Windows.Forms.Button();
            this.btnExportToExcel = new System.Windows.Forms.Button();
            this.TxtbxVersion = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtExcelName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtbxSelectedSnapshots = new System.Windows.Forms.TextBox();
            this.BtnSelectSnapshots = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.LblSnapshotError = new System.Windows.Forms.Label();
            this.TxtbxSelectedAssemblies = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TxtbxSnapshotsCreated = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.LblExportError = new System.Windows.Forms.Label();
            this.BtnOpenExcelReports = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnSelectAssemblies
            // 
            this.BtnSelectAssemblies.Location = new System.Drawing.Point(117, 19);
            this.BtnSelectAssemblies.Name = "BtnSelectAssemblies";
            this.BtnSelectAssemblies.Size = new System.Drawing.Size(124, 23);
            this.BtnSelectAssemblies.TabIndex = 0;
            this.BtnSelectAssemblies.Text = "Select Assemblies";
            this.BtnSelectAssemblies.UseVisualStyleBackColor = true;
            this.BtnSelectAssemblies.Click += new System.EventHandler(this.BtnSelectAssemblies_Click);
            // 
            // BtnCreateSnapshots
            // 
            this.BtnCreateSnapshots.Enabled = false;
            this.BtnCreateSnapshots.Location = new System.Drawing.Point(115, 189);
            this.BtnCreateSnapshots.Name = "BtnCreateSnapshots";
            this.BtnCreateSnapshots.Size = new System.Drawing.Size(124, 23);
            this.BtnCreateSnapshots.TabIndex = 1;
            this.BtnCreateSnapshots.Text = "Create Snapshots";
            this.BtnCreateSnapshots.UseVisualStyleBackColor = true;
            this.BtnCreateSnapshots.Click += new System.EventHandler(this.BtnCreateSnapshots_Click);
            // 
            // btnExportToExcel
            // 
            this.btnExportToExcel.Enabled = false;
            this.btnExportToExcel.Location = new System.Drawing.Point(117, 166);
            this.btnExportToExcel.Name = "btnExportToExcel";
            this.btnExportToExcel.Size = new System.Drawing.Size(124, 23);
            this.btnExportToExcel.TabIndex = 3;
            this.btnExportToExcel.Text = "Export to Excel";
            this.btnExportToExcel.UseVisualStyleBackColor = true;
            this.btnExportToExcel.Click += new System.EventHandler(this.btnExportToExcel_Click);
            // 
            // TxtbxVersion
            // 
            this.TxtbxVersion.Location = new System.Drawing.Point(115, 144);
            this.TxtbxVersion.Name = "TxtbxVersion";
            this.TxtbxVersion.Size = new System.Drawing.Size(100, 20);
            this.TxtbxVersion.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 148);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Assembly Version:";
            // 
            // txtExcelName
            // 
            this.txtExcelName.Location = new System.Drawing.Point(117, 199);
            this.txtExcelName.Name = "txtExcelName";
            this.txtExcelName.ReadOnly = true;
            this.txtExcelName.Size = new System.Drawing.Size(491, 20);
            this.txtExcelName.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 218);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Snapshot Names:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 199);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Excel Name:";
            // 
            // TxtbxSelectedSnapshots
            // 
            this.TxtbxSelectedSnapshots.Location = new System.Drawing.Point(117, 84);
            this.TxtbxSelectedSnapshots.Multiline = true;
            this.TxtbxSelectedSnapshots.Name = "TxtbxSelectedSnapshots";
            this.TxtbxSelectedSnapshots.ReadOnly = true;
            this.TxtbxSelectedSnapshots.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TxtbxSelectedSnapshots.Size = new System.Drawing.Size(491, 56);
            this.TxtbxSelectedSnapshots.TabIndex = 14;
            // 
            // BtnSelectSnapshots
            // 
            this.BtnSelectSnapshots.Location = new System.Drawing.Point(117, 20);
            this.BtnSelectSnapshots.Name = "BtnSelectSnapshots";
            this.BtnSelectSnapshots.Size = new System.Drawing.Size(124, 23);
            this.BtnSelectSnapshots.TabIndex = 16;
            this.BtnSelectSnapshots.Text = "Select Snapshots";
            this.BtnSelectSnapshots.UseVisualStyleBackColor = true;
            this.BtnSelectSnapshots.Click += new System.EventHandler(this.BtnSelectSnapshots_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 84);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Snapshots Names:";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.LblSnapshotError);
            this.groupBox1.Controls.Add(this.TxtbxSelectedAssemblies);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.TxtbxSnapshotsCreated);
            this.groupBox1.Controls.Add(this.BtnCreateSnapshots);
            this.groupBox1.Controls.Add(this.BtnSelectAssemblies);
            this.groupBox1.Controls.Add(this.TxtbxVersion);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(14, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(614, 287);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Create Snapshots of Assemblies";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(114, 47);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(384, 13);
            this.label10.TabIndex = 21;
            this.label10.Text = "ex.: Select all the assemblies in Verion 62.17.0.0 and then create the snapshots." +
                "";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(114, 66);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(0, 13);
            this.label9.TabIndex = 20;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(245, 148);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "ex.: 62.18.0.0";
            // 
            // LblSnapshotError
            // 
            this.LblSnapshotError.AutoSize = true;
            this.LblSnapshotError.ForeColor = System.Drawing.Color.Crimson;
            this.LblSnapshotError.Location = new System.Drawing.Point(245, 194);
            this.LblSnapshotError.Name = "LblSnapshotError";
            this.LblSnapshotError.Size = new System.Drawing.Size(32, 13);
            this.LblSnapshotError.TabIndex = 17;
            this.LblSnapshotError.Text = "Error:";
            this.LblSnapshotError.Visible = false;
            // 
            // TxtbxSelectedAssemblies
            // 
            this.TxtbxSelectedAssemblies.Location = new System.Drawing.Point(115, 86);
            this.TxtbxSelectedAssemblies.Multiline = true;
            this.TxtbxSelectedAssemblies.Name = "TxtbxSelectedAssemblies";
            this.TxtbxSelectedAssemblies.ReadOnly = true;
            this.TxtbxSelectedAssemblies.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TxtbxSelectedAssemblies.Size = new System.Drawing.Size(491, 52);
            this.TxtbxSelectedAssemblies.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Assembly Names:";
            // 
            // TxtbxSnapshotsCreated
            // 
            this.TxtbxSnapshotsCreated.Location = new System.Drawing.Point(115, 218);
            this.TxtbxSnapshotsCreated.Multiline = true;
            this.TxtbxSnapshotsCreated.Name = "TxtbxSnapshotsCreated";
            this.TxtbxSnapshotsCreated.ReadOnly = true;
            this.TxtbxSnapshotsCreated.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TxtbxSnapshotsCreated.Size = new System.Drawing.Size(491, 52);
            this.TxtbxSnapshotsCreated.TabIndex = 14;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.LblExportError);
            this.groupBox2.Controls.Add(this.BtnOpenExcelReports);
            this.groupBox2.Controls.Add(this.btnExportToExcel);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.TxtbxSelectedSnapshots);
            this.groupBox2.Controls.Add(this.BtnSelectSnapshots);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtExcelName);
            this.groupBox2.Location = new System.Drawing.Point(12, 334);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(614, 285);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Generate Report in Excel";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(113, 65);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(384, 13);
            this.label8.TabIndex = 22;
            this.label8.Text = "       PayMedia.ApplicationServices.BillingEngine.ServiceContracts-62.18.0.0.xml";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(114, 50);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(383, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "ex.: PayMedia.ApplicationServices.BillingEngine.ServiceContracts-62.17.0.0.xml";
            // 
            // LblExportError
            // 
            this.LblExportError.AutoSize = true;
            this.LblExportError.ForeColor = System.Drawing.Color.Crimson;
            this.LblExportError.Location = new System.Drawing.Point(247, 171);
            this.LblExportError.Name = "LblExportError";
            this.LblExportError.Size = new System.Drawing.Size(32, 13);
            this.LblExportError.TabIndex = 20;
            this.LblExportError.Text = "Error:";
            this.LblExportError.Visible = false;
            // 
            // BtnOpenExcelReports
            // 
            this.BtnOpenExcelReports.Location = new System.Drawing.Point(117, 248);
            this.BtnOpenExcelReports.Name = "BtnOpenExcelReports";
            this.BtnOpenExcelReports.Size = new System.Drawing.Size(124, 23);
            this.BtnOpenExcelReports.TabIndex = 19;
            this.BtnOpenExcelReports.Text = "Open Excel Reports";
            this.BtnOpenExcelReports.UseVisualStyleBackColor = true;
            this.BtnOpenExcelReports.Click += new System.EventHandler(this.BtnOpenExcelReports_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(114, 66);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(480, 13);
            this.label11.TabIndex = 22;
            this.label11.Text = "       Then repeat this step for the assemblies in Version 62.18.0.0 before gener" +
                "ating a report in Excel.";
            // 
            // ApiChangesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(641, 631);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "ApiChangesForm";
            this.Text = "API Changes GUI";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnSelectAssemblies;
        private System.Windows.Forms.Button BtnCreateSnapshots;
        private System.Windows.Forms.Button btnExportToExcel;
        private System.Windows.Forms.TextBox TxtbxVersion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtExcelName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TxtbxSelectedSnapshots;
        private System.Windows.Forms.Button BtnSelectSnapshots;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox TxtbxSnapshotsCreated;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox TxtbxSelectedAssemblies;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button BtnOpenExcelReports;
        private System.Windows.Forms.Label LblSnapshotError;
        private System.Windows.Forms.Label LblExportError;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
    }
}

