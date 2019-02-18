using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace analysis_gui
{
    static class AnalysisType
    {
        /// <summary>
        /// The analysis script names for each index. The indices
        /// in this array must be the same as the ones in the combobox.
        /// </summary>
        private static string[] analysisScriptTypes = new string[]
        {
            "ngrams",
            "pairwise-frequency",
            "word-frequency"
        };


        /// <summary>
        /// The extension for python files
        /// </summary>
        private static string pythonExtension = ".py";


        /// <summary>
        /// Gets the analysis python script name from a given index
        /// </summary>
        /// <param name="index"></param>
        public static string getAnalysisScript(int index)
        {
            //Just return name + extension, i.e. "ngrams" + ".py"
            return analysisScriptTypes[index] + pythonExtension;
        }

        public static void AppendText(this RichTextBox box, string text, Color color, bool bold=false)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;

            if (bold)
                box.SelectionFont = new Font(box.Font, FontStyle.Bold);

            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
            box.SelectionFont = new Font(box.Font, FontStyle.Regular);
        }
    }
}
