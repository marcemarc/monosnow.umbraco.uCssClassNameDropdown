using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.IO;
using System.Web;
using monosnow.umbraco.uCssClassNameDropdown.Services;

namespace monosnow.umbraco.uCssClassNameDropdown
{
    /// <summary>
    /// Control to render dropdown list of CssClass Names
    /// </summary>
    public class uCssClassNameDropdownControl : System.Web.UI.WebControls.Panel
    {
        private DropDownList CssClassNameList;
        private Label StatusMessage;

        private ClassNameRetrievalService _retrievalService;

        /// <summary>
        /// Method to populate Dropdown list
        /// </summary>
        /// <param name="classNames">enumerable list of classnames</param>
        private void populateDropdown(IEnumerable<string> classNames){             
            CssClassNameList.DataSource = classNames;
            CssClassNameList.DataBind();
            if (CssClassNameList.Items.Count > 0)
            {
                CssClassNameList.Items.Insert(0, new ListItem("Choose...", ""));
            }
             CssClassNameList.CssClass = "umbEditorDropDownList";
            CssClassNameList.ID = "CssClassNameList_" + this.ClientID;
        }

        /// <summary>
        /// Init event of the control
        /// Get the Classnames using ClassNameRetrievalService and bind to dropdown
        /// Set currently selected value
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            bool isError = false;
            string statusMessage = "";
            base.OnInit(e);
    
            _retrievalService = new ClassNameRetrievalService(this.PathToCssFile, this.Exceptions, this.CssClassRegEx);
            if (_retrievalService.CssFileExists){
            populateDropdown(_retrievalService.GetClassNames());
          //  _retrievalService = new ClassNameRetrievalService(this.PathToCssFile, this.Exceptions, this.CssClassRegEx);
            if (CssClassNameList.Items.Count > 0)
            {                
                if (!String.IsNullOrEmpty(this.SelectedValue))
                {
                    CssClassNameList.SelectedValue = this.SelectedValue;
                }
            }
            else {
                isError = true;
                statusMessage = "No Class names found matching the Regex: " + this.CssClassRegEx;
            }
           }
           else {
               isError= true;
                statusMessage = "Stylesheet: '" + this.PathToCssFile + "' not found";            

            }          
      
                this.Controls.Add(CssClassNameList);
                 if (isError){
                     StatusMessage = new Label();
                StatusMessage.Text = statusMessage;
                StatusMessage.Visible = true;
                StatusMessage.CssClass = "alert alert-warning error";
                this.Controls.Add(StatusMessage);
                 }

            }
     
        /// <summary>
        /// Path to css file
        /// </summary>
        private string _pathToCssFile;
        public string PathToCssFile
        {
            get
            {
                return _pathToCssFile;
            }
            set
            {
                _pathToCssFile = value;
            }
        }
        private string _exceptions;
        /// <summary>
        /// List of Class Name's to ignore
        /// </summary>
        public string Exceptions
        {
            get
            {
                return _exceptions;
            }
            set
            {
                _exceptions = value;
            }
        }
        private string _cssClassRegEx;
        /// <summary>
        /// Regex to match the class names for the dropdown
        /// </summary>
        public string CssClassRegEx
        {
            get
            {
                return _cssClassRegEx;
            }
            set
            {
                _cssClassRegEx = value;
            }
        }

        /// <summary>
        /// Currently selected value of the control
        /// </summary>
        public string _selectedValue;
        public string SelectedValue
        {
            get
            {
                return CssClassNameList.SelectedValue;
            }
            set
            {
                if (CssClassNameList == null)
                {
                    CssClassNameList = new DropDownList();
                    _retrievalService = new ClassNameRetrievalService(this.PathToCssFile, this.Exceptions, this.CssClassRegEx);
                    populateDropdown(_retrievalService.GetClassNames());                   
                }
                if (!String.IsNullOrEmpty(value))
                {
                    _selectedValue = value;
                    CssClassNameList.SelectedValue = value;
                }
               
             }
              
        }
    }


    }

