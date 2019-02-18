using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scrapey_gui
{
    public class Scraper
    {
        /// <summary>
        /// The source character
        /// </summary>
        private char sourceChar = ' ';

        /// <summary>
        /// Source
        /// </summary>
        public string source = "";

        /// <summary>
        /// The search term
        /// </summary>
        public string searchTerm = "";

        /// <summary>
        /// Creates a new scraper instance.
        /// </summary>
        public Scraper(string source, string term)
        {
            //Set up source character and term
            this.sourceChar = this.getSourceChar(source);
            this.searchTerm = term.Replace("\"", "\\\"");
            this.source = source;
        }

        /// <summary>
        /// Gets the source character for scrape spec.
        /// </summary>
        private char getSourceChar(string source)
        {
            //Just return the lowercase version of the first character
            return source.ToLower()[0];
        }

        /// <summary>
        /// Returns the string representation of this scraper (for scraper spec)
        /// </summary>
        public override string ToString()
        {
            return "" + this.sourceChar;
        }
    }
}
