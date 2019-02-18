namespace scrapey_gui
{
    partial class mainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.scraperList = new System.Windows.Forms.ListBox();
            this.infoLabel = new System.Windows.Forms.Label();
            this.groupBoxAdd = new System.Windows.Forms.GroupBox();
            this.scrapeTypeLabel = new System.Windows.Forms.Label();
            this.scrapeType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.searchTerm = new System.Windows.Forms.TextBox();
            this.groupBoxSettings = new System.Windows.Forms.GroupBox();
            this.deleteButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.globalOptionsGroupBox = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.streamOnlyBox = new System.Windows.Forms.CheckBox();
            this.startButton = new System.Windows.Forms.Button();
            this.useCSVBox = new System.Windows.Forms.CheckBox();
            this.searchNameLabel = new System.Windows.Forms.Label();
            this.clearSheetsBox = new System.Windows.Forms.CheckBox();
            this.searchName = new System.Windows.Forms.TextBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.debugCommandLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.updateStripTimer = new System.Windows.Forms.Timer(this.components);
            this.scriptFolderButton = new System.Windows.Forms.Button();
            this.scriptFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBoxAdd.SuspendLayout();
            this.groupBoxSettings.SuspendLayout();
            this.globalOptionsGroupBox.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // scraperList
            // 
            this.scraperList.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scraperList.FormattingEnabled = true;
            this.scraperList.ItemHeight = 17;
            this.scraperList.Location = new System.Drawing.Point(466, 217);
            this.scraperList.Name = "scraperList";
            this.scraperList.Size = new System.Drawing.Size(120, 106);
            this.scraperList.TabIndex = 0;
            this.scraperList.SelectedValueChanged += new System.EventHandler(this.scraperList_SelectedValueChanged);
            // 
            // infoLabel
            // 
            this.infoLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.infoLabel.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.infoLabel.Location = new System.Drawing.Point(166, 41);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Padding = new System.Windows.Forms.Padding(10);
            this.infoLabel.Size = new System.Drawing.Size(146, 106);
            this.infoLabel.TabIndex = 1;
            // 
            // groupBoxAdd
            // 
            this.groupBoxAdd.Controls.Add(this.scrapeTypeLabel);
            this.groupBoxAdd.Controls.Add(this.scrapeType);
            this.groupBoxAdd.Controls.Add(this.label1);
            this.groupBoxAdd.Controls.Add(this.searchTerm);
            this.groupBoxAdd.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxAdd.Location = new System.Drawing.Point(24, 176);
            this.groupBoxAdd.Name = "groupBoxAdd";
            this.groupBoxAdd.Size = new System.Drawing.Size(317, 211);
            this.groupBoxAdd.TabIndex = 3;
            this.groupBoxAdd.TabStop = false;
            this.groupBoxAdd.Text = "Add scraper";
            // 
            // scrapeTypeLabel
            // 
            this.scrapeTypeLabel.AutoSize = true;
            this.scrapeTypeLabel.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scrapeTypeLabel.Location = new System.Drawing.Point(20, 43);
            this.scrapeTypeLabel.Name = "scrapeTypeLabel";
            this.scrapeTypeLabel.Size = new System.Drawing.Size(54, 20);
            this.scrapeTypeLabel.TabIndex = 3;
            this.scrapeTypeLabel.Text = "Source";
            // 
            // scrapeType
            // 
            this.scrapeType.FormattingEnabled = true;
            this.scrapeType.Items.AddRange(new object[] {
            "Twitter",
            "Reddit",
            "Steam"});
            this.scrapeType.Location = new System.Drawing.Point(24, 67);
            this.scrapeType.Name = "scrapeType";
            this.scrapeType.Size = new System.Drawing.Size(211, 28);
            this.scrapeType.TabIndex = 2;
            this.scrapeType.Text = "Twitter";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Search term";
            // 
            // searchTerm
            // 
            this.searchTerm.Location = new System.Drawing.Point(24, 146);
            this.searchTerm.Name = "searchTerm";
            this.searchTerm.Size = new System.Drawing.Size(211, 27);
            this.searchTerm.TabIndex = 0;
            // 
            // groupBoxSettings
            // 
            this.groupBoxSettings.Controls.Add(this.deleteButton);
            this.groupBoxSettings.Controls.Add(this.infoLabel);
            this.groupBoxSettings.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxSettings.Location = new System.Drawing.Point(435, 176);
            this.groupBoxSettings.Name = "groupBoxSettings";
            this.groupBoxSettings.Size = new System.Drawing.Size(336, 211);
            this.groupBoxSettings.TabIndex = 4;
            this.groupBoxSettings.TabStop = false;
            this.groupBoxSettings.Text = "Scrapers";
            // 
            // deleteButton
            // 
            this.deleteButton.BackColor = System.Drawing.SystemColors.Control;
            this.deleteButton.FlatAppearance.BorderSize = 2;
            this.deleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deleteButton.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteButton.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.deleteButton.Location = new System.Drawing.Point(200, 166);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(112, 33);
            this.deleteButton.TabIndex = 0;
            this.deleteButton.Text = "DELETE";
            this.deleteButton.UseVisualStyleBackColor = false;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // addButton
            // 
            this.addButton.BackColor = System.Drawing.SystemColors.Control;
            this.addButton.FlatAppearance.BorderSize = 2;
            this.addButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addButton.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addButton.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.addButton.Location = new System.Drawing.Point(356, 253);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(64, 44);
            this.addButton.TabIndex = 1;
            this.addButton.Text = "➡️";
            this.addButton.UseVisualStyleBackColor = false;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // globalOptionsGroupBox
            // 
            this.globalOptionsGroupBox.Controls.Add(this.button1);
            this.globalOptionsGroupBox.Controls.Add(this.streamOnlyBox);
            this.globalOptionsGroupBox.Controls.Add(this.startButton);
            this.globalOptionsGroupBox.Controls.Add(this.useCSVBox);
            this.globalOptionsGroupBox.Controls.Add(this.searchNameLabel);
            this.globalOptionsGroupBox.Controls.Add(this.clearSheetsBox);
            this.globalOptionsGroupBox.Controls.Add(this.searchName);
            this.globalOptionsGroupBox.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.globalOptionsGroupBox.Location = new System.Drawing.Point(24, 403);
            this.globalOptionsGroupBox.Name = "globalOptionsGroupBox";
            this.globalOptionsGroupBox.Size = new System.Drawing.Size(747, 169);
            this.globalOptionsGroupBox.TabIndex = 4;
            this.globalOptionsGroupBox.TabStop = false;
            this.globalOptionsGroupBox.Text = "Global options";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Control;
            this.button1.FlatAppearance.BorderSize = 2;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.button1.Location = new System.Drawing.Point(611, 50);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 41);
            this.button1.TabIndex = 8;
            this.button1.Text = "COPY TEXT";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // streamOnlyBox
            // 
            this.streamOnlyBox.AutoSize = true;
            this.streamOnlyBox.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.streamOnlyBox.Location = new System.Drawing.Point(299, 59);
            this.streamOnlyBox.Name = "streamOnlyBox";
            this.streamOnlyBox.Size = new System.Drawing.Size(142, 24);
            this.streamOnlyBox.TabIndex = 7;
            this.streamOnlyBox.Text = "Live stream only?";
            this.streamOnlyBox.UseVisualStyleBackColor = true;
            // 
            // startButton
            // 
            this.startButton.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.startButton.FlatAppearance.BorderSize = 2;
            this.startButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startButton.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startButton.ForeColor = System.Drawing.SystemColors.Control;
            this.startButton.Location = new System.Drawing.Point(611, 97);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(112, 41);
            this.startButton.TabIndex = 1;
            this.startButton.Text = "SCRAPE";
            this.startButton.UseVisualStyleBackColor = false;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // useCSVBox
            // 
            this.useCSVBox.AutoSize = true;
            this.useCSVBox.Checked = true;
            this.useCSVBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.useCSVBox.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.useCSVBox.Location = new System.Drawing.Point(299, 89);
            this.useCSVBox.Name = "useCSVBox";
            this.useCSVBox.Size = new System.Drawing.Size(95, 24);
            this.useCSVBox.TabIndex = 6;
            this.useCSVBox.Text = "Use CSVs?";
            this.useCSVBox.UseVisualStyleBackColor = true;
            // 
            // searchNameLabel
            // 
            this.searchNameLabel.AutoSize = true;
            this.searchNameLabel.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchNameLabel.Location = new System.Drawing.Point(20, 59);
            this.searchNameLabel.Name = "searchNameLabel";
            this.searchNameLabel.Size = new System.Drawing.Size(209, 20);
            this.searchNameLabel.TabIndex = 5;
            this.searchNameLabel.Text = "Search name / Spreadsheet ID";
            // 
            // clearSheetsBox
            // 
            this.clearSheetsBox.AutoSize = true;
            this.clearSheetsBox.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clearSheetsBox.Location = new System.Drawing.Point(299, 119);
            this.clearSheetsBox.Name = "clearSheetsBox";
            this.clearSheetsBox.Size = new System.Drawing.Size(260, 24);
            this.clearSheetsBox.TabIndex = 0;
            this.clearSheetsBox.Text = "Clear existing data before starting?";
            this.clearSheetsBox.UseVisualStyleBackColor = true;
            // 
            // searchName
            // 
            this.searchName.Location = new System.Drawing.Point(24, 84);
            this.searchName.Name = "searchName";
            this.searchName.Size = new System.Drawing.Size(211, 27);
            this.searchName.TabIndex = 4;
            this.searchName.Text = "example-search";
            this.searchName.TextChanged += new System.EventHandler(this.searchName_TextChanged);
            // 
            // statusStrip
            // 
            this.statusStrip.AutoSize = false;
            this.statusStrip.BackColor = System.Drawing.Color.White;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.debugCommandLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 586);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(801, 57);
            this.statusStrip.TabIndex = 5;
            // 
            // debugCommandLabel
            // 
            this.debugCommandLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.debugCommandLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.debugCommandLabel.ForeColor = System.Drawing.Color.DimGray;
            this.debugCommandLabel.Name = "debugCommandLabel";
            this.debugCommandLabel.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.debugCommandLabel.Size = new System.Drawing.Size(786, 52);
            this.debugCommandLabel.Spring = true;
            this.debugCommandLabel.Text = "python scrapey.py";
            this.debugCommandLabel.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.debugCommandLabel.ToolTipText = "\r\n";
            // 
            // updateStripTimer
            // 
            this.updateStripTimer.Enabled = true;
            this.updateStripTimer.Tick += new System.EventHandler(this.updateStripTimer_Tick);
            // 
            // scriptFolderButton
            // 
            this.scriptFolderButton.BackColor = System.Drawing.Color.DarkSlateGray;
            this.scriptFolderButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.scriptFolderButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scriptFolderButton.ForeColor = System.Drawing.Color.LightSlateGray;
            this.scriptFolderButton.Location = new System.Drawing.Point(655, 38);
            this.scriptFolderButton.Name = "scriptFolderButton";
            this.scriptFolderButton.Size = new System.Drawing.Size(92, 50);
            this.scriptFolderButton.TabIndex = 7;
            this.scriptFolderButton.Text = "SET SCRIPT FOLDER";
            this.scriptFolderButton.UseVisualStyleBackColor = false;
            this.scriptFolderButton.Click += new System.EventHandler(this.scriptFolderButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.DarkSlateGray;
            this.pictureBox1.Image = global::scrapey_gui.Properties.Resources.scrapey_logo;
            this.pictureBox1.InitialImage = global::scrapey_gui.Properties.Resources.scrapey;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(451, 144);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkSlateGray;
            this.panel1.Location = new System.Drawing.Point(415, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(386, 144);
            this.panel1.TabIndex = 8;
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 643);
            this.Controls.Add(this.scriptFolderButton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.globalOptionsGroupBox);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.scraperList);
            this.Controls.Add(this.groupBoxSettings);
            this.Controls.Add(this.groupBoxAdd);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "mainForm";
            this.Text = "Scrapey";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.mainForm_FormClosed);
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.groupBoxAdd.ResumeLayout(false);
            this.groupBoxAdd.PerformLayout();
            this.groupBoxSettings.ResumeLayout(false);
            this.globalOptionsGroupBox.ResumeLayout(false);
            this.globalOptionsGroupBox.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox scraperList;
        private System.Windows.Forms.Label infoLabel;
        private System.Windows.Forms.GroupBox groupBoxAdd;
        private System.Windows.Forms.Label scrapeTypeLabel;
        private System.Windows.Forms.ComboBox scrapeType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox searchTerm;
        private System.Windows.Forms.GroupBox groupBoxSettings;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.GroupBox globalOptionsGroupBox;
        private System.Windows.Forms.CheckBox useCSVBox;
        private System.Windows.Forms.Label searchNameLabel;
        private System.Windows.Forms.CheckBox clearSheetsBox;
        private System.Windows.Forms.TextBox searchName;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.CheckBox streamOnlyBox;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel debugCommandLabel;
        private System.Windows.Forms.Timer updateStripTimer;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button scriptFolderButton;
        private System.Windows.Forms.FolderBrowserDialog scriptFolderDialog;
        private System.Windows.Forms.Panel panel1;
    }
}

