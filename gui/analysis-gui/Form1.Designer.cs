namespace analysis_gui
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.analysisTypeBox = new System.Windows.Forms.ComboBox();
            this.analysisTypeLabel = new System.Windows.Forms.Label();
            this.inputOutputFilesBox = new System.Windows.Forms.GroupBox();
            this.openFileButton = new System.Windows.Forms.Button();
            this.outputFileLabel = new System.Windows.Forms.Label();
            this.outputFileLocationText = new System.Windows.Forms.TextBox();
            this.inputFileLabel = new System.Windows.Forms.Label();
            this.browseButtonInput = new System.Windows.Forms.Button();
            this.inputFileLocationText = new System.Windows.Forms.TextBox();
            this.csvSelectionDialog = new System.Windows.Forms.OpenFileDialog();
            this.scriptLocationDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.scriptsFolderBrowse = new System.Windows.Forms.Button();
            this.scriptsFolderLabel = new System.Windows.Forms.Label();
            this.scriptsFolderText = new System.Windows.Forms.TextBox();
            this.columnIndexControl = new System.Windows.Forms.NumericUpDown();
            this.columnIndexLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.nCountControl = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.lowestFrequencyControl = new System.Windows.Forms.NumericUpDown();
            this.stopwordsCheckbox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.threadCountControl = new System.Windows.Forms.NumericUpDown();
            this.builtCommandLabel = new System.Windows.Forms.RichTextBox();
            this.runButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.inputOutputFilesBox.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.columnIndexControl)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nCountControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lowestFrequencyControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.threadCountControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // analysisTypeBox
            // 
            this.analysisTypeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.analysisTypeBox.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.analysisTypeBox.FormattingEnabled = true;
            this.analysisTypeBox.Items.AddRange(new object[] {
            "N-Gram",
            "Pairwise / Word neighbourhood",
            "Word frequency"});
            this.analysisTypeBox.Location = new System.Drawing.Point(15, 193);
            this.analysisTypeBox.Name = "analysisTypeBox";
            this.analysisTypeBox.Size = new System.Drawing.Size(281, 28);
            this.analysisTypeBox.Sorted = true;
            this.analysisTypeBox.TabIndex = 0;
            this.analysisTypeBox.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // analysisTypeLabel
            // 
            this.analysisTypeLabel.AutoSize = true;
            this.analysisTypeLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.analysisTypeLabel.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.analysisTypeLabel.Location = new System.Drawing.Point(14, 166);
            this.analysisTypeLabel.Name = "analysisTypeLabel";
            this.analysisTypeLabel.Size = new System.Drawing.Size(131, 21);
            this.analysisTypeLabel.TabIndex = 1;
            this.analysisTypeLabel.Text = "Type of analysis";
            // 
            // inputOutputFilesBox
            // 
            this.inputOutputFilesBox.Controls.Add(this.openFileButton);
            this.inputOutputFilesBox.Controls.Add(this.outputFileLabel);
            this.inputOutputFilesBox.Controls.Add(this.outputFileLocationText);
            this.inputOutputFilesBox.Controls.Add(this.inputFileLabel);
            this.inputOutputFilesBox.Controls.Add(this.browseButtonInput);
            this.inputOutputFilesBox.Controls.Add(this.inputFileLocationText);
            this.inputOutputFilesBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputOutputFilesBox.Location = new System.Drawing.Point(12, 298);
            this.inputOutputFilesBox.Name = "inputOutputFilesBox";
            this.inputOutputFilesBox.Size = new System.Drawing.Size(423, 190);
            this.inputOutputFilesBox.TabIndex = 2;
            this.inputOutputFilesBox.TabStop = false;
            this.inputOutputFilesBox.Text = "Input / Output files";
            // 
            // openFileButton
            // 
            this.openFileButton.BackColor = System.Drawing.SystemColors.Control;
            this.openFileButton.FlatAppearance.BorderSize = 2;
            this.openFileButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.openFileButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openFileButton.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.openFileButton.Location = new System.Drawing.Point(307, 135);
            this.openFileButton.Name = "openFileButton";
            this.openFileButton.Size = new System.Drawing.Size(94, 29);
            this.openFileButton.TabIndex = 6;
            this.openFileButton.Text = "OPEN";
            this.openFileButton.UseVisualStyleBackColor = false;
            this.openFileButton.Click += new System.EventHandler(this.openFileButton_Click);
            // 
            // outputFileLabel
            // 
            this.outputFileLabel.AutoSize = true;
            this.outputFileLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outputFileLabel.Location = new System.Drawing.Point(12, 116);
            this.outputFileLabel.Name = "outputFileLabel";
            this.outputFileLabel.Size = new System.Drawing.Size(201, 17);
            this.outputFileLabel.TabIndex = 5;
            this.outputFileLabel.Text = "Output file name (in same folder)";
            // 
            // outputFileLocationText
            // 
            this.outputFileLocationText.Location = new System.Drawing.Point(12, 135);
            this.outputFileLocationText.Name = "outputFileLocationText";
            this.outputFileLocationText.Size = new System.Drawing.Size(289, 29);
            this.outputFileLocationText.TabIndex = 3;
            this.outputFileLocationText.TextChanged += new System.EventHandler(this.outputFileLocationText_TextChanged);
            // 
            // inputFileLabel
            // 
            this.inputFileLabel.AutoSize = true;
            this.inputFileLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputFileLabel.Location = new System.Drawing.Point(12, 39);
            this.inputFileLabel.Name = "inputFileLabel";
            this.inputFileLabel.Size = new System.Drawing.Size(108, 17);
            this.inputFileLabel.TabIndex = 2;
            this.inputFileLabel.Text = "Input file location";
            // 
            // browseButtonInput
            // 
            this.browseButtonInput.BackColor = System.Drawing.SystemColors.Control;
            this.browseButtonInput.FlatAppearance.BorderSize = 2;
            this.browseButtonInput.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.browseButtonInput.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.browseButtonInput.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.browseButtonInput.Location = new System.Drawing.Point(307, 58);
            this.browseButtonInput.Name = "browseButtonInput";
            this.browseButtonInput.Size = new System.Drawing.Size(94, 29);
            this.browseButtonInput.TabIndex = 1;
            this.browseButtonInput.Text = "BROWSE";
            this.browseButtonInput.UseVisualStyleBackColor = false;
            this.browseButtonInput.Click += new System.EventHandler(this.browseButtonInput_Click);
            // 
            // inputFileLocationText
            // 
            this.inputFileLocationText.Location = new System.Drawing.Point(12, 58);
            this.inputFileLocationText.Name = "inputFileLocationText";
            this.inputFileLocationText.Size = new System.Drawing.Size(289, 29);
            this.inputFileLocationText.TabIndex = 0;
            // 
            // csvSelectionDialog
            // 
            this.csvSelectionDialog.DefaultExt = "csv";
            this.csvSelectionDialog.FileName = "input_file.csv";
            this.csvSelectionDialog.Filter = "CSV Files|*.csv";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSlateGray;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.scriptsFolderBrowse);
            this.panel1.Controls.Add(this.scriptsFolderLabel);
            this.panel1.Controls.Add(this.scriptsFolderText);
            this.panel1.Controls.Add(this.columnIndexControl);
            this.panel1.Controls.Add(this.columnIndexLabel);
            this.panel1.Controls.Add(this.analysisTypeLabel);
            this.panel1.Controls.Add(this.analysisTypeBox);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(869, 250);
            this.panel1.TabIndex = 3;
            // 
            // scriptsFolderBrowse
            // 
            this.scriptsFolderBrowse.FlatAppearance.BorderSize = 2;
            this.scriptsFolderBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.scriptsFolderBrowse.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scriptsFolderBrowse.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.scriptsFolderBrowse.Location = new System.Drawing.Point(525, 192);
            this.scriptsFolderBrowse.Name = "scriptsFolderBrowse";
            this.scriptsFolderBrowse.Size = new System.Drawing.Size(94, 29);
            this.scriptsFolderBrowse.TabIndex = 6;
            this.scriptsFolderBrowse.Text = "BROWSE";
            this.scriptsFolderBrowse.UseVisualStyleBackColor = true;
            this.scriptsFolderBrowse.Click += new System.EventHandler(this.scriptsFolderBrowse_Click);
            // 
            // scriptsFolderLabel
            // 
            this.scriptsFolderLabel.AutoSize = true;
            this.scriptsFolderLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scriptsFolderLabel.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.scriptsFolderLabel.Location = new System.Drawing.Point(350, 167);
            this.scriptsFolderLabel.Name = "scriptsFolderLabel";
            this.scriptsFolderLabel.Size = new System.Drawing.Size(111, 21);
            this.scriptsFolderLabel.TabIndex = 7;
            this.scriptsFolderLabel.Text = "Scripts folder";
            // 
            // scriptsFolderText
            // 
            this.scriptsFolderText.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scriptsFolderText.Location = new System.Drawing.Point(354, 193);
            this.scriptsFolderText.Name = "scriptsFolderText";
            this.scriptsFolderText.Size = new System.Drawing.Size(165, 27);
            this.scriptsFolderText.TabIndex = 6;
            this.scriptsFolderText.TextChanged += new System.EventHandler(this.scriptsFolderText_TextChanged);
            // 
            // columnIndexControl
            // 
            this.columnIndexControl.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.columnIndexControl.Location = new System.Drawing.Point(802, 193);
            this.columnIndexControl.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.columnIndexControl.Name = "columnIndexControl";
            this.columnIndexControl.Size = new System.Drawing.Size(43, 27);
            this.columnIndexControl.TabIndex = 4;
            this.columnIndexControl.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.columnIndexControl.ValueChanged += new System.EventHandler(this.columnIndexControl_ValueChanged);
            // 
            // columnIndexLabel
            // 
            this.columnIndexLabel.AutoSize = true;
            this.columnIndexLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.columnIndexLabel.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.columnIndexLabel.Location = new System.Drawing.Point(655, 166);
            this.columnIndexLabel.Name = "columnIndexLabel";
            this.columnIndexLabel.Size = new System.Drawing.Size(196, 21);
            this.columnIndexLabel.TabIndex = 3;
            this.columnIndexLabel.Text = "Comment Column Index";
            this.columnIndexLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.nCountControl);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lowestFrequencyControl);
            this.groupBox1.Controls.Add(this.stopwordsCheckbox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.threadCountControl);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(455, 298);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(396, 190);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Arguments";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(181, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(155, 17);
            this.label3.TabIndex = 12;
            this.label3.Text = "n (bigrams, trigrams, etc)";
            // 
            // nCountControl
            // 
            this.nCountControl.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nCountControl.Location = new System.Drawing.Point(184, 60);
            this.nCountControl.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.nCountControl.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nCountControl.Name = "nCountControl";
            this.nCountControl.Size = new System.Drawing.Size(43, 27);
            this.nCountControl.TabIndex = 13;
            this.nCountControl.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nCountControl.ValueChanged += new System.EventHandler(this.nCountControl_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(22, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 17);
            this.label2.TabIndex = 10;
            this.label2.Text = "Lowest frequency";
            // 
            // lowestFrequencyControl
            // 
            this.lowestFrequencyControl.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lowestFrequencyControl.Location = new System.Drawing.Point(25, 135);
            this.lowestFrequencyControl.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.lowestFrequencyControl.Name = "lowestFrequencyControl";
            this.lowestFrequencyControl.Size = new System.Drawing.Size(43, 27);
            this.lowestFrequencyControl.TabIndex = 11;
            this.lowestFrequencyControl.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.lowestFrequencyControl.ValueChanged += new System.EventHandler(this.lowestFrequencyControl_ValueChanged);
            // 
            // stopwordsCheckbox
            // 
            this.stopwordsCheckbox.AutoSize = true;
            this.stopwordsCheckbox.Checked = true;
            this.stopwordsCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.stopwordsCheckbox.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.stopwordsCheckbox.FlatAppearance.BorderSize = 2;
            this.stopwordsCheckbox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.stopwordsCheckbox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stopwordsCheckbox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.stopwordsCheckbox.Location = new System.Drawing.Point(184, 116);
            this.stopwordsCheckbox.Name = "stopwordsCheckbox";
            this.stopwordsCheckbox.Size = new System.Drawing.Size(143, 21);
            this.stopwordsCheckbox.TabIndex = 9;
            this.stopwordsCheckbox.Text = "Remove stopwords?";
            this.stopwordsCheckbox.UseVisualStyleBackColor = true;
            this.stopwordsCheckbox.CheckedChanged += new System.EventHandler(this.stopwordsCheckbox_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(22, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Thread count";
            // 
            // threadCountControl
            // 
            this.threadCountControl.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.threadCountControl.Location = new System.Drawing.Point(25, 60);
            this.threadCountControl.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.threadCountControl.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.threadCountControl.Name = "threadCountControl";
            this.threadCountControl.Size = new System.Drawing.Size(43, 27);
            this.threadCountControl.TabIndex = 8;
            this.threadCountControl.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.threadCountControl.ValueChanged += new System.EventHandler(this.threadCountControl_ValueChanged);
            // 
            // builtCommandLabel
            // 
            this.builtCommandLabel.BackColor = System.Drawing.Color.White;
            this.builtCommandLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.builtCommandLabel.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.builtCommandLabel.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.builtCommandLabel.Location = new System.Drawing.Point(0, 515);
            this.builtCommandLabel.Name = "builtCommandLabel";
            this.builtCommandLabel.ReadOnly = true;
            this.builtCommandLabel.Size = new System.Drawing.Size(749, 50);
            this.builtCommandLabel.TabIndex = 7;
            this.builtCommandLabel.Text = "python";
            // 
            // runButton
            // 
            this.runButton.BackColor = System.Drawing.SystemColors.ControlDark;
            this.runButton.FlatAppearance.BorderSize = 2;
            this.runButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.runButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.runButton.ForeColor = System.Drawing.Color.Black;
            this.runButton.Location = new System.Drawing.Point(751, 515);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(118, 50);
            this.runButton.TabIndex = 9;
            this.runButton.Text = "RUN";
            this.runButton.UseVisualStyleBackColor = false;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.DarkSlateGray;
            this.pictureBox1.Image = global::analysis_gui.Properties.Resources.scrapey_logo;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(435, 139);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DarkSlateGray;
            this.panel2.Location = new System.Drawing.Point(432, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(437, 139);
            this.panel2.TabIndex = 9;
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 565);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.builtCommandLabel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.inputOutputFilesBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "mainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Scrapey Text Analysis Tool";
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.inputOutputFilesBox.ResumeLayout(false);
            this.inputOutputFilesBox.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.columnIndexControl)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nCountControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lowestFrequencyControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.threadCountControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox analysisTypeBox;
        private System.Windows.Forms.Label analysisTypeLabel;
        private System.Windows.Forms.GroupBox inputOutputFilesBox;
        private System.Windows.Forms.Label outputFileLabel;
        private System.Windows.Forms.TextBox outputFileLocationText;
        private System.Windows.Forms.Label inputFileLabel;
        private System.Windows.Forms.Button browseButtonInput;
        private System.Windows.Forms.TextBox inputFileLocationText;
        private System.Windows.Forms.OpenFileDialog csvSelectionDialog;
        private System.Windows.Forms.FolderBrowserDialog scriptLocationDialog;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown columnIndexControl;
        private System.Windows.Forms.Label columnIndexLabel;
        private System.Windows.Forms.Button scriptsFolderBrowse;
        private System.Windows.Forms.Label scriptsFolderLabel;
        private System.Windows.Forms.TextBox scriptsFolderText;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nCountControl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown lowestFrequencyControl;
        private System.Windows.Forms.CheckBox stopwordsCheckbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown threadCountControl;
        private System.Windows.Forms.RichTextBox builtCommandLabel;
        private System.Windows.Forms.Button openFileButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.Panel panel2;
    }
}

