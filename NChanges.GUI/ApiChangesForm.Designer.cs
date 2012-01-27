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
            this.btnViewExcelOutput = new System.Windows.Forms.Button();
            this.btnExportToExcel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtExcelOutput = new System.Windows.Forms.TextBox();
            this.btnCreateReports = new System.Windows.Forms.Button();
            this.editPathButton = new System.Windows.Forms.Button();
            this.editVersionButton = new System.Windows.Forms.Button();
            this.removeAssemblyButton = new System.Windows.Forms.Button();
            this.addAssemblyButton = new System.Windows.Forms.Button();
            this.assembliesListView = new System.Windows.Forms.ListView();
            this.assemblyColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.versionColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label16 = new System.Windows.Forms.Label();
            this.txtTypesToExclude = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCreateSnapshots = new System.Windows.Forms.Button();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recentProjectsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recentProjectsToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.closeProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.saveProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.menuStrip.SuspendLayout();
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
            this.tabControl1.Location = new System.Drawing.Point(12, 27);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(604, 534);
            this.tabControl1.TabIndex = 23;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnViewExcelOutput);
            this.tabPage1.Controls.Add(this.btnExportToExcel);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.txtExcelOutput);
            this.tabPage1.Controls.Add(this.btnCreateReports);
            this.tabPage1.Controls.Add(this.editPathButton);
            this.tabPage1.Controls.Add(this.editVersionButton);
            this.tabPage1.Controls.Add(this.removeAssemblyButton);
            this.tabPage1.Controls.Add(this.addAssemblyButton);
            this.tabPage1.Controls.Add(this.assembliesListView);
            this.tabPage1.Controls.Add(this.label16);
            this.tabPage1.Controls.Add(this.txtTypesToExclude);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.btnCreateSnapshots);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(596, 508);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Snapshot";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnViewExcelOutput
            // 
            this.btnViewExcelOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnViewExcelOutput.Location = new System.Drawing.Point(100, 444);
            this.btnViewExcelOutput.Name = "btnViewExcelOutput";
            this.btnViewExcelOutput.Size = new System.Drawing.Size(124, 23);
            this.btnViewExcelOutput.TabIndex = 53;
            this.btnViewExcelOutput.Text = "View Excel Output";
            this.btnViewExcelOutput.UseVisualStyleBackColor = false;
            this.btnViewExcelOutput.Click += new System.EventHandler(this.btnViewExcelOutput_Click);
            // 
            // btnExportToExcel
            // 
            this.btnExportToExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExportToExcel.Location = new System.Drawing.Point(100, 415);
            this.btnExportToExcel.Name = "btnExportToExcel";
            this.btnExportToExcel.Size = new System.Drawing.Size(124, 23);
            this.btnExportToExcel.TabIndex = 52;
            this.btnExportToExcel.Text = "Export to Excel";
            this.btnExportToExcel.UseVisualStyleBackColor = false;
            this.btnExportToExcel.Click += new System.EventHandler(this.btnExportToExcel_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 381);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 51;
            this.label1.Text = "Excel Output:";
            // 
            // txtExcelOutput
            // 
            this.txtExcelOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtExcelOutput.Location = new System.Drawing.Point(100, 378);
            this.txtExcelOutput.Name = "txtExcelOutput";
            this.txtExcelOutput.Size = new System.Drawing.Size(427, 20);
            this.txtExcelOutput.TabIndex = 50;
            this.txtExcelOutput.TextChanged += new System.EventHandler(this.txtExcelOutput_TextChanged);
            // 
            // btnCreateReports
            // 
            this.btnCreateReports.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCreateReports.Location = new System.Drawing.Point(100, 317);
            this.btnCreateReports.Name = "btnCreateReports";
            this.btnCreateReports.Size = new System.Drawing.Size(124, 23);
            this.btnCreateReports.TabIndex = 49;
            this.btnCreateReports.Text = "Create Reports";
            this.btnCreateReports.UseVisualStyleBackColor = false;
            this.btnCreateReports.Click += new System.EventHandler(this.btnCreateReports_Click);
            // 
            // editPathButton
            // 
            this.editPathButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.editPathButton.Location = new System.Drawing.Point(213, 215);
            this.editPathButton.Name = "editPathButton";
            this.editPathButton.Size = new System.Drawing.Size(75, 23);
            this.editPathButton.TabIndex = 48;
            this.editPathButton.Text = "Edit Path";
            this.editPathButton.UseVisualStyleBackColor = true;
            this.editPathButton.Click += new System.EventHandler(this.editPathButton_Click);
            // 
            // editVersionButton
            // 
            this.editVersionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.editVersionButton.Location = new System.Drawing.Point(294, 215);
            this.editVersionButton.Name = "editVersionButton";
            this.editVersionButton.Size = new System.Drawing.Size(75, 23);
            this.editVersionButton.TabIndex = 47;
            this.editVersionButton.Text = "Edit Version";
            this.editVersionButton.UseVisualStyleBackColor = true;
            this.editVersionButton.Click += new System.EventHandler(this.editVersionButton_Click);
            // 
            // removeAssemblyButton
            // 
            this.removeAssemblyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.removeAssemblyButton.Location = new System.Drawing.Point(375, 215);
            this.removeAssemblyButton.Name = "removeAssemblyButton";
            this.removeAssemblyButton.Size = new System.Drawing.Size(110, 23);
            this.removeAssemblyButton.TabIndex = 46;
            this.removeAssemblyButton.Text = "Remove";
            this.removeAssemblyButton.UseVisualStyleBackColor = true;
            this.removeAssemblyButton.Click += new System.EventHandler(this.removeAssemblyButton_Click);
            // 
            // addAssemblyButton
            // 
            this.addAssemblyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addAssemblyButton.Location = new System.Drawing.Point(100, 215);
            this.addAssemblyButton.Name = "addAssemblyButton";
            this.addAssemblyButton.Size = new System.Drawing.Size(107, 23);
            this.addAssemblyButton.TabIndex = 45;
            this.addAssemblyButton.Text = "Add";
            this.addAssemblyButton.UseVisualStyleBackColor = true;
            this.addAssemblyButton.Click += new System.EventHandler(this.addAssemblyButton_Click);
            // 
            // assembliesListView
            // 
            this.assembliesListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.assembliesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.assemblyColumnHeader,
            this.versionColumnHeader});
            this.assembliesListView.FullRowSelect = true;
            this.assembliesListView.Location = new System.Drawing.Point(100, 6);
            this.assembliesListView.Name = "assembliesListView";
            this.assembliesListView.Size = new System.Drawing.Size(483, 203);
            this.assembliesListView.TabIndex = 44;
            this.assembliesListView.UseCompatibleStateImageBehavior = false;
            this.assembliesListView.View = System.Windows.Forms.View.Details;
            // 
            // assemblyColumnHeader
            // 
            this.assemblyColumnHeader.Text = "Assembly";
            this.assemblyColumnHeader.Width = 240;
            // 
            // versionColumnHeader
            // 
            this.versionColumnHeader.Text = "Version";
            this.versionColumnHeader.Width = 80;
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(-2, 254);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(96, 13);
            this.label16.TabIndex = 42;
            this.label16.Text = "Types To Exclude:";
            // 
            // txtTypesToExclude
            // 
            this.txtTypesToExclude.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtTypesToExclude.Location = new System.Drawing.Point(100, 251);
            this.txtTypesToExclude.Name = "txtTypesToExclude";
            this.txtTypesToExclude.Size = new System.Drawing.Size(478, 20);
            this.txtTypesToExclude.TabIndex = 41;
            this.txtTypesToExclude.TextChanged += new System.EventHandler(this.txtTypesToExclude_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 32;
            this.label4.Text = "Assemblies:";
            // 
            // btnCreateSnapshots
            // 
            this.btnCreateSnapshots.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCreateSnapshots.Location = new System.Drawing.Point(100, 288);
            this.btnCreateSnapshots.Name = "btnCreateSnapshots";
            this.btnCreateSnapshots.Size = new System.Drawing.Size(124, 23);
            this.btnCreateSnapshots.TabIndex = 27;
            this.btnCreateSnapshots.Text = "Create Snapshots";
            this.btnCreateSnapshots.UseVisualStyleBackColor = true;
            this.btnCreateSnapshots.Click += new System.EventHandler(this.btnCreateSnapshots_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(628, 24);
            this.menuStrip.TabIndex = 24;
            this.menuStrip.Text = "menuStrip";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.recentProjectsToolStripMenuItem,
            this.toolStripSeparator2,
            this.closeProjectToolStripMenuItem,
            this.toolStripSeparator3,
            this.saveProjectToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItem1.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.openToolStripMenuItem.Text = "&Open Project";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // recentProjectsToolStripMenuItem
            // 
            this.recentProjectsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.recentProjectsToolStripSeparator,
            this.clearToolStripMenuItem});
            this.recentProjectsToolStripMenuItem.Name = "recentProjectsToolStripMenuItem";
            this.recentProjectsToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.recentProjectsToolStripMenuItem.Text = "Open &Recent Project";
            // 
            // recentProjectsToolStripSeparator
            // 
            this.recentProjectsToolStripSeparator.Name = "recentProjectsToolStripSeparator";
            this.recentProjectsToolStripSeparator.Size = new System.Drawing.Size(98, 6);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(179, 6);
            // 
            // closeProjectToolStripMenuItem
            // 
            this.closeProjectToolStripMenuItem.Name = "closeProjectToolStripMenuItem";
            this.closeProjectToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.closeProjectToolStripMenuItem.Text = "&Close Project";
            this.closeProjectToolStripMenuItem.Click += new System.EventHandler(this.closeProjectToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(179, 6);
            // 
            // saveProjectToolStripMenuItem
            // 
            this.saveProjectToolStripMenuItem.Name = "saveProjectToolStripMenuItem";
            this.saveProjectToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.saveProjectToolStripMenuItem.Text = "&Save Project";
            this.saveProjectToolStripMenuItem.Click += new System.EventHandler(this.saveProjectToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(179, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            // 
            // ApiChangesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(628, 573);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "ApiChangesForm";
            this.Text = "NChanges GUI";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCreateSnapshots;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtTypesToExclude;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recentProjectsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem saveProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator recentProjectsToolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ListView assembliesListView;
        private System.Windows.Forms.ColumnHeader assemblyColumnHeader;
        private System.Windows.Forms.ColumnHeader versionColumnHeader;
        private System.Windows.Forms.Button editVersionButton;
        private System.Windows.Forms.Button removeAssemblyButton;
        private System.Windows.Forms.Button addAssemblyButton;
        private System.Windows.Forms.Button editPathButton;
        private System.Windows.Forms.Button btnCreateReports;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtExcelOutput;
        private System.Windows.Forms.Button btnExportToExcel;
        private System.Windows.Forms.Button btnViewExcelOutput;
    }
}

