using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Web;
using System.Runtime.Caching;

namespace monosnow.umbraco.uCssClassNameDropdown.Services
{
    /// <summary>
    /// Service responsible for applying the regex and retrieving the class names
    /// </summary>
    public class ClassNameRetrievalService
    {
        private string _pathToCss = "/css/font-awesome.css";
        private string _exceptions = "large";
        private string _cssClassRegEx = @"\.icon-([^:]*?):before";
        public ClassNameRetrievalService()
        {


        }
        /// <summary>
        /// Checks if filename exists
        /// </summary>
        public Boolean CssFileExists { get { return (File.Exists(HttpContext.Current.Server.MapPath(_pathToCss))); } }
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pathToCss">path to Css File Name</param>
        public ClassNameRetrievalService(string pathToCss)
        {
            _pathToCss = String.IsNullOrEmpty(pathToCss) ? _pathToCss : pathToCss; ;

        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pathToCss">Path to Css File name</param>
        /// <param name="exceptions">List of classes to ignore</param>
        public ClassNameRetrievalService(string pathToCss, string exceptions)
        {
           
            _pathToCss = String.IsNullOrEmpty(pathToCss) ? _pathToCss : pathToCss;
            _exceptions = String.IsNullOrEmpty(exceptions) ? _exceptions : exceptions;

        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pathToCss">Path to Css File name</param>
        /// <param name="exceptions">List of classes to ignore</param>
        /// <param name="cssClassRegEx">Regex to match classnames, first capture group is the text used</param>
        public ClassNameRetrievalService(string pathToCss, string exceptions, string cssClassRegEx)
        {
            _pathToCss = String.IsNullOrEmpty(pathToCss) ? _pathToCss : pathToCss; ;
            _cssClassRegEx = String.IsNullOrEmpty(cssClassRegEx) ? _cssClassRegEx : cssClassRegEx;
            _exceptions = String.IsNullOrEmpty(exceptions) ? _exceptions : exceptions;

        }
        /// <summary>
        /// Method to get the CssClass names from the file by applying the regex
        /// </summary>
        /// <returns>An enumeration of class names</returns>
        public IEnumerable<string> GetClassNames()
        {
            string cssContents = getCssFileContents();
            // use regex to find all the class names
            List<string> _cssClassNames = new List<string>();

            // Define a regular expression for repeated words.
            Regex rx = new Regex(_cssClassRegEx,
              RegexOptions.Compiled | RegexOptions.IgnoreCase);

            // Find matches.
            MatchCollection matches = rx.Matches(cssContents);
            string[] exceptions = _exceptions.Split(new char[]{'|',','},StringSplitOptions.RemoveEmptyEntries);
            // loop through all the matches 
            foreach (Match match in matches)
            {
                // read in the first capture group of the matching text
                string _matchingText = match.Groups[1].Value.Trim();
                // check class is longer that 2 chars and not in the exception list and not already in the list of matched css names
                if (_matchingText.Length > 2 && !exceptions.Contains(_matchingText.ToLower()) && !_cssClassNames.Contains(_matchingText))
                {
                    _cssClassNames.Add(_matchingText);
                }
            }
           
            //sort the list alphabetically
            _cssClassNames.Sort();
            return _cssClassNames;

        }
        /// <summary>
        /// method to read the contents of the css file from disc
        /// contents are added to server cache, and cache is attempted to be read first
        /// until file changes.
        /// </summary>
        /// <returns>string containing contents of css file</returns>
        private string getCssFileContents()
        {
            string cssContents = String.Empty;
            if (this.CssFileExists)
            {
                ObjectCache cache = MemoryCache.Default;
                 cssContents = (string)cache[_pathToCss + "CssFileContents"];
                if (String.IsNullOrEmpty(cssContents))
                {
                    if (File.Exists(HttpContext.Current.Server.MapPath(_pathToCss)))
                    {
                        StreamReader cssFileStream = new StreamReader(HttpContext.Current.Server.MapPath(_pathToCss));
                        cssContents = cssFileStream.ReadToEnd();
                        cssFileStream.Close();
                        if (!String.IsNullOrEmpty(cssContents))
                        {
                            CacheItemPolicy policy = new CacheItemPolicy();

                            List<string> filePaths = new List<string>();
                            filePaths.Add(HttpContext.Current.Server.MapPath(_pathToCss));

                            policy.ChangeMonitors.Add(new
                            HostFileChangeMonitor(filePaths));


                            cache.Add(_pathToCss + "CssFileContents", cssContents, policy);
                        }
                    }
                }

            }
            return cssContents;

        }

    }
}


