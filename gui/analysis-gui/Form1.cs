using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace analysis_gui
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Set n-gram control to invisible if not n-gram
            nCountControl.Visible = label3.Visible = (analysisTypeBox.Text == "N-Gram");

            //Build command and refresh
            buildCommand();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            //Set index to 0
            analysisTypeBox.SelectedIndex = 0;

            //Set scripts folder from the registry
            scriptsFolderText.Text = (string)RegistryManager.ReadEntry("analysis-settings", "scripts-path");
            inputFileLocationText.Text = (string)RegistryManager.ReadEntry("analysis-settings", "input-file-path");
            outputFileLocationText.Text = (string)RegistryManager.ReadEntry("analysis-settings", "output-file");

            //Build command
            buildCommand();
        }

        private void buildCommand()
        {
            //Reset text
            builtCommandLabel.Text = "";

            //Command always starts with python
            string command = "python ";

            //Append script type
            command += scriptsFolderText.Text + "/" + AnalysisType.getAnalysisScript(analysisTypeBox.SelectedIndex);

            builtCommandLabel.AppendText(command, Color.Gray, false);
            command = "";

            //Set column-index flag
            command += " --column-index " + columnIndexControl.Value.ToString();

            if(columnIndexControl.Focused)
                builtCommandLabel.AppendText(command, Color.Black, true);
            else
                builtCommandLabel.AppendText(command, Color.Gray, false);

            command = "";

            //Set other arguments
            var arguments = OptionalArguments.getArgumentsForScript(AnalysisType.getAnalysisScript(analysisTypeBox.SelectedIndex));

            foreach(var argument in arguments)
            {
                var uncastedValue = stringToControlValue(argument.name);
                var control = stringToControl(argument.name);

                dynamic casted = Convert.ChangeType(uncastedValue, argument.type);

                command = "";

                if (casted is bool && casted)
                    command = " " + argument.argument;

                else if(!(casted is bool))
                    command += " " + argument.argument + " " + casted;

                if (control.Focused)
                    builtCommandLabel.AppendText(command, Color.Black, true);
                else
                    builtCommandLabel.AppendText(command, Color.Gray, false);

                command = "";
            }
            //---


            //Set input file names
            command += " " + inputFileLocationText.Text;

            if (inputFileLocationText.Focused)
                builtCommandLabel.AppendText(command, Color.Black, true);
            else
                builtCommandLabel.AppendText(command, Color.Gray, false);

            command = "";

            //Base path for output file name
            string basePath = "";

            //Is input file location null?
            if(inputFileLocationText.Text != "")
                basePath = System.IO.Path.GetDirectoryName(inputFileLocationText.Text);

            //Add this into the command
            command += " " + basePath + @"\" + outputFileLocationText.Text;

            if (outputFileLocationText.Focused)
                builtCommandLabel.AppendText(command, Color.Black, true);
            else
                builtCommandLabel.AppendText(command, Color.Gray, false);

            command = "";

            //And set the text of the status strip label
            builtCommandLabel.AppendText(command, Color.LightBlue);
        }

        private void scriptsFolderText_TextChanged(object sender, EventArgs e)
        {
            //Build command and refresh
            buildCommand();
        }

        private void scriptsFolderBrowse_Click(object sender, EventArgs e)
        {
            scriptLocationDialog.SelectedPath = scriptsFolderText.Text;
            scriptLocationDialog.RootFolder = Environment.SpecialFolder.Desktop;

            DialogResult result = scriptLocationDialog.ShowDialog();

            if(result == DialogResult.OK)
            {
                scriptsFolderText.Text = scriptLocationDialog.SelectedPath;
                RegistryManager.WriteEntry("analysis-settings", "scripts-path", scriptsFolderText.Text);
            }
        }

        private void browseButtonInput_Click(object sender, EventArgs e)
        {
            csvSelectionDialog.InitialDirectory = inputFileLocationText.Text;

            DialogResult result = csvSelectionDialog.ShowDialog();

            if(result == DialogResult.OK)
            {
                inputFileLocationText.Text = csvSelectionDialog.FileName;
                RegistryManager.WriteEntry("analysis-settings", "input-file-path", inputFileLocationText.Text);
            }
        }

        private void outputFileLocationText_TextChanged(object sender, EventArgs e)
        {
            RegistryManager.WriteEntry("analysis-settings", "output-file", outputFileLocationText.Text);
            buildCommand();
        }

        private void columnIndexControl_ValueChanged(object sender, EventArgs e)
        {
            buildCommand();
        }

        private Control stringToControl(string ctrl)
        {
            if (ctrl == "stopwords")
                return stopwordsCheckbox;

            else if (ctrl == "lowest-frequency")
                return lowestFrequencyControl;

            else if (ctrl == "thread-count")
                return threadCountControl;

            else if (ctrl == "n")
                return nCountControl;

            else
                return null;
        }

        private object stringToControlValue(string ctrl)
        {
            if (ctrl == "stopwords")
                return stopwordsCheckbox.Checked;

            else if (ctrl == "lowest-frequency")
                return lowestFrequencyControl.Value;

            else if (ctrl == "thread-count")
                return threadCountControl.Value;

            else if (ctrl == "n")
                return nCountControl.Value;

            else
                return null;
        }

        private void stopwordsCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            buildCommand();
        }

        private void nCountControl_ValueChanged(object sender, EventArgs e)
        {
            buildCommand();
        }

        private void threadCountControl_ValueChanged(object sender, EventArgs e)
        {
            buildCommand();
        }

        private void lowestFrequencyControl_ValueChanged(object sender, EventArgs e)
        {
            buildCommand();
        }

        private void openFileButton_Click(object sender, EventArgs e)
        {
            //Base path for output file name
            string basePath = "";

            //Is input file location null?
            if (inputFileLocationText.Text != "")
                basePath = System.IO.Path.GetDirectoryName(inputFileLocationText.Text);

            //Add this into the command
            var final = basePath + @"\" + outputFileLocationText.Text;

            try
            {
                Process.Start(final);
            }
            catch(Exception ex)
            {
                if (ex is System.ComponentModel.Win32Exception)
                    MessageBox.Show("Looks like that file doesn't exist yet! Whoops!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            //Create a new process
            var proc = new Process();

            //Start filename, arguments
            proc.StartInfo.FileName = "python.exe";
            proc.StartInfo.Arguments = builtCommandLabel.Text.Split(' ').Skip(1).Aggregate((a, b) => a + " " + b);

            //And working directory..
            //proc.StartInfo.WorkingDirectory = @"C:\git\gitlab\twitter-scrape";

            //Start process off and wait for it to finish
            proc.Start();
        }
    }
}
