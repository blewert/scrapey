using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;


namespace scrapey_gui
{
    public partial class mainForm : Form
    {
        /// <summary>
        /// Scrapers for this particular search
        /// </summary>
        public List<Scraper> scrapers = new List<Scraper>();

        /// <summary>
        /// Process for scraper
        /// </summary>
        public Process proc;

        //Script folder
        private static string scriptFolder = "";

        public mainForm()
        {
            InitializeComponent();
        }


        /// <summary>
        /// When the add button is clicked.
        /// </summary>
        private void addButton_Click(object sender, EventArgs e)
        {
            //Empty or invalid scraper?
            if(!scrapeType.Items.Contains(scrapeType.Text))
            {
                showError("Couldn't add source '" + scrapeType.Text + "'. You need to choose one from the list of sources.");
                return;
            }

            //Empty search
            if(string.IsNullOrEmpty(searchTerm.Text))
            {
                showError("The search term cannot be empty or whitespace.");
                return;
            }

            //Duplicate search?
            if(scrapers.Any(x => x.source.Equals(scrapeType.Text) && x.searchTerm.Equals(searchTerm.Text)))
            {
                showError("You are already searching for this term, with this source! (duplicate search)");
                return;
            }

            //Make a new scraper
            var scraper = new Scraper(scrapeType.Text, searchTerm.Text);

            //Add it to the list of scrapers
            scrapers.Add(scraper);

            //Refresh list of scrapers
            refreshScraperList();

            //Clear inputs
            searchTerm.Text = "";
            scrapeType.Text = "Twitter";
        }

        /// <summary>
        /// Build command to run
        /// </summary>
        /// <returns></returns>
        private string buildCommand()
        {
            //Build string
            string buildString = "python " + scriptFolder + "scrapey.py ";

            //Global options: using csv?
            if (useCSVBox.Checked)
                buildString += "--csv ";

            //Global options: stream only?
            if (streamOnlyBox.Checked)
                buildString += "--streamonly ";

            //Global options: clear sheets before hand?
            if (clearSheetsBox.Checked)
                buildString += "--clear ";

            //Search name
            buildString += searchName.Text + " ";

            //Number of scrapers = 0? Return
            if (scrapers.Count == 0)
                return buildString;

            //Add scrape spec
            buildString += scrapers.Select(x => x.ToString()).Aggregate((a, b) => a + b) + " ";

            //Add search terms
            buildString += scrapers.Select(x => $"\"{x.searchTerm}\"").Aggregate((a, b) => a + " " + b);

            //Return built string
            return buildString;
        }

        /// <summary>
        /// Shows an error
        /// </summary>
        /// <param name="text"></param>
        private void showError(string text)
        {
            MessageBox.Show(text, "Scrape error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        /// <summary>
        /// Refreshes the list of scrapers
        /// </summary>
        private void refreshScraperList()
        {
            //Clear the list
            scraperList.Items.Clear();

            //Add them
            foreach (var scraper in scrapers)
                scraperList.Items.Add(scraper.source);

            //Refresh
            scraperList.Refresh();
        }

        /// <summary>
        /// Called when the timer ticks
        /// </summary>
        private void updateStripTimer_Tick(object sender, EventArgs e)
        {
            //Build command
            var text = buildCommand();

            if (mainForm.ActiveForm == null)
                return;

            var ptToPx = 0.8f;
            var amountOfChars = (mainForm.ActiveForm.Width) / (debugCommandLabel.Font.SizeInPoints * ptToPx);

            for(var i = 0; i < text.Length; i++)
            {
                if (i % (int)Math.Floor(amountOfChars) == 0)
                    text = text.Insert(i, "\n");
            }

            debugCommandLabel.Text = text;
        }

        /// <summary>
        /// Called when the search name is changed
        /// </summary>
        private void searchName_TextChanged(object sender, EventArgs e)
        {            
            foreach(var c in searchName.Text)
            {
                //Run through each character. If it's not a letter, digit, or dash:
                //..

                if (!(char.IsLetterOrDigit(c) || c == '-'))
                {
                    //Find selection start
                    var start = searchName.SelectionStart;

                    //Replace all instances
                    searchName.Text = searchName.Text.Replace(c, '-');

                    //Reset selection start back to where the cursor was
                    searchName.SelectionStart = start;
                }
            }            
        }

        /// <summary>
        /// Called when another item in the scraper list is selected
        /// </summary>
        private void scraperList_SelectedValueChanged(object sender, EventArgs e)
        {
            //Find the scraper for this selected item
            var scraper = scrapers[scraperList.SelectedIndex];

            //Set text to null
            infoLabel.Text = "";

            //And show the search term
            infoLabel.Text += "Search term: " + scraper.searchTerm + "\n";
        }

        /// <summary>
        /// Called when the delete button has been clicked
        /// </summary>
        private void deleteButton_Click(object sender, EventArgs e)
        {
            //Remove scraper at this index
            scrapers.RemoveAt(scraperList.SelectedIndex);

            //And refresh
            refreshScraperList();

            //Reset info text
            infoLabel.Text = "";
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            //Create a new process
            proc = new Process();

            //Start filename, arguments
            proc.StartInfo.FileName = "python.exe";
            proc.StartInfo.Arguments = buildCommand().Split(' ').Skip(1).Aggregate((a, b) => a + " " + b);

            //And working directory..
            //proc.StartInfo.WorkingDirectory = @"C:\git\gitlab\twitter-scrape";
            proc.StartInfo.WorkingDirectory = scriptFolder;

            //Set opacity
            startButton.Enabled = false;
            startButton.Text = "SCRAPING";

            //When the process exits, exit
            proc.EnableRaisingEvents = true;
            proc.Exited += new EventHandler(Proc_Exited);
            proc.SynchronizingObject = this;

            //Start process off and wait for it to finish
            proc.Start();
        }

        private void Proc_Exited(object sender, EventArgs e)
        {
            startButton.Enabled = true;
            startButton.Text = "START SCRAPE";
            proc = null;
        }

        private void mainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (proc != null)
                proc.Kill();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Clipboard.SetText(debugCommandLabel.Text.Replace("\n", ""));
            MessageBox.Show("Copied text: " + debugCommandLabel.Text.Replace("\n", ""));
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            //Check the registry. If the script folder is null, it hasn't be set -- so alert the user that this is the
            //case
            if (RegistryManager.ReadEntry("scrapeygui", "scriptfolder") == null)
                MessageBox.Show("The script folder has not been set up. To do this, click the 'Set Script Folder' in "
                              + "the top right of the application. Then, navigate and select the folder where scrapey.py "
                              + "is located.", "Set script folder", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //Otherwise set script folder up
            else
                scriptFolder = (string)RegistryManager.ReadEntry("scrapeygui", "scriptfolder");
            
        }

        private void scriptFolderButton_Click(object sender, EventArgs e)
        {
            //Set up initial start up folder
            scriptFolderDialog.RootFolder = Environment.SpecialFolder.Desktop;

            if (scriptFolderDialog.ShowDialog() == DialogResult.OK)
            {
                //The user has selected a folder. Now its time to find it:
                var path = scriptFolderDialog.SelectedPath + @"\";

                //Set script folder
                scriptFolder = path;

                //Write it into the registry
                RegistryManager.WriteEntry("scrapeygui", "scriptfolder", path);
            }
        }
    }
}
