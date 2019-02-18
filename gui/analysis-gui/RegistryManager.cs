using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Win32;
using System.Windows.Forms;

namespace analysis_gui
{
    static class RegistryManager
    {
        /// <summary>
        /// Base key -- HKLM
        /// </summary>
        private const string REGISTRY_BASE_PATH = "HKEY_LOCAL_MACHINE";


        /// <summary>
        /// Base path to keys for scrapey
        /// </summary>
        private const string REGISTRY_SOFTWARE_BASE_PATH = "SOFTWARE\\Scrapey";


        /// <summary>
        /// The full base path
        /// </summary>
        private const string registryBasePath = REGISTRY_BASE_PATH + "\\" + REGISTRY_SOFTWARE_BASE_PATH + "\\";


        /// <summary>
        /// Software base path is the same but without HKLM
        /// </summary>
        private const string softwareBasePath = REGISTRY_SOFTWARE_BASE_PATH + "\\";


        /// <summary>
        /// Writes an entry to the registry for this application
        /// </summary>
        /// <param name="keyStr">The key path</param>
        /// <param name="value">The value name</param>
        /// <param name="actualValue">The value to set</param>
        public static void WriteEntry(string keyStr, string value, object actualValue)
        {
            try
            {
                //Just set the value
                Registry.SetValue(registryBasePath + keyStr, value, actualValue);
            }
            catch (Exception e)
            {
                //Is this an access exception?
                if (e is UnauthorizedAccessException)
                    MessageBox.Show("This application does not have acccess to write to the registry. You probably"
                                  + "need to restart this application as an administrator (Run as Administrator) "
                                  + "for it to work correctly.", "Access error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                //Otherwise it's some kind of other exception
                else
                    MessageBox.Show("An unknown error occurred.", "Unknown error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Reads an entry from the registry, for this application
        /// </summary>
        /// <param name="key">The key path</param>
        /// <param name="value">The value name</param>
        /// <returns></returns>
        public static object ReadEntry(string key, string value)
        {
            //Just return the value
            return Registry.GetValue(registryBasePath + key, value, null);
        }
    }
}