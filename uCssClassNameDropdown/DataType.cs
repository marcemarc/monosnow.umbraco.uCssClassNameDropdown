using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using umbraco.cms.businesslogic.datatype;

namespace monosnow.umbraco.uCssClassNameDropdown
{
    /// <summary>
    /// DataType for uCssClassNameDropdown implements Abstract Data Editor
    /// </summary>
    public class DataType : AbstractDataEditor
    {

        /// <summary>
        /// Reference to the Control
        /// </summary>
        private uCssClassNameDropdownControl control = new uCssClassNameDropdownControl();
   
      /// <summary>
      /// Path to Css File configuration option
      /// </summary>
        [DataEditorSetting("PathToCssFile",
            description = "Path To Css File, eg /css/font-awesome.css",
            defaultValue = "/css/font-awesome.css")]
        public string PathToCssFile { get; set; }
         
        /// <summary>
        /// Regex to match the css Classnames in the file configuration option
        /// </summary>
        [DataEditorSetting("CssClassNameRegex",
            description = "Regex to match the class name's in the css file, eg \\.icon-([^:]*?):before",
            defaultValue = "\\.icon-([^:]*?):before")]
        public string CssClassNameRegEx { get; set; }

        /// <summary>
        /// List of css names to ignore congifuration option
        /// </summary>
         [DataEditorSetting("ExceptionList",
            description = "a pipe | delimited list of matched class names to ignore",
            defaultValue = "large")]
        public string ExceptionList { get; set; }


        /// <summary>
        /// Unique GUID of the datatype
        /// </summary>
        public override Guid Id
        {
            get
            {
                return new Guid("5ee6d542-f8d7-4b6e-9161-fe95b1866ad1");
            }
        }

        /// <summary>
        /// Name of Datatype for the dropdown
        /// </summary>
        public override string DataTypeName
        {
            get
            {
                return "uCssClassNameDropdown";
            }
        }

        /// <summary>
        /// DataType constructor
        /// setup control, wire up Init and Save events
        /// </summary>
        public DataType()
        {
            //set rendercontrol
            base.RenderControl = control;
            //init event
            control.Init += new EventHandler(control_Init);
            //save event
            base.DataEditorControl.OnSave +=
                new AbstractDataEditorControl.SaveEventHandler(
                    DataEditorControl_OnSave);

        }
        /// <summary>
        /// Handles the control init event
        /// set up the current value of the control
        /// and set configuration options
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void control_Init(object sender, EventArgs e)
        {

            control.SelectedValue = base.Data.Value != null ? base.Data.Value.ToString() : "";
            control.PathToCssFile = string.IsNullOrEmpty(PathToCssFile) ? "/css/font-awesome.css" : PathToCssFile;
            control.CssClassRegEx = string.IsNullOrEmpty(CssClassNameRegEx) ? "\\.icon-([^:]*?):before" : CssClassNameRegEx;
            control.Exceptions = string.IsNullOrEmpty(ExceptionList) ? "": ExceptionList;

        }

        /// <summary>
        /// Save event
        /// Save selected value of dropdown control to database
        /// </summary>
        /// <param name="e"></param>
        void DataEditorControl_OnSave(EventArgs e)
        {
            base.Data.Value = control.SelectedValue;
          
           
        }



    
       
    }

    }

