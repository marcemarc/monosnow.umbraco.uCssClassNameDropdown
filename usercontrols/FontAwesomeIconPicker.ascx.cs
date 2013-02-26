using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using monosnow.umbraco.uCssClassNameDropdown.Services;
using System.Web.UI.HtmlControls;
using umbraco.editorControls.userControlGrapper;

namespace monosnow.umbraco.uCssClassNameDropdown.usercontrols
{
    public partial class FontAwesomeIconPicker : System.Web.UI.UserControl, IUsercontrolDataEditor 
    {
        protected override void OnPreRender(EventArgs e) 
   {
        bool linkIncluded = false;
        foreach (Control c in Page.Header.Controls)
        {
            if (c.ID == "FontAwesomeStylesheet")
            {
                linkIncluded = true;
            }
        }
        if (!linkIncluded)
        {
            HtmlGenericControl csslink = new HtmlGenericControl("link");
            csslink.ID = "FontAwesomeStylesheet";
            csslink.Attributes.Add("href", "/css/font-awesome.css");
            csslink.Attributes.Add("type", "text/css");
            csslink.Attributes.Add("rel", "stylesheet");
            Page.Header.Controls.Add(csslink);
        }
    }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                ClassNameRetrievalService _service = new ClassNameRetrievalService("/css/font-awesome.css","large",@"\.icon-([^:]*?):before");
                lvIcons.DataSource = _service.GetClassNames();
                lvIcons.DataBind();
                ddlIcons.DataSource = _service.GetClassNames();
                ddlIcons.DataBind();
                ddlIcons.Items.Insert(0, new ListItem("Choose...", ""));

                
            }

        }

        #region IUsercontrolDataEditor Members

        public object value
        {
            get
            {
                return ddlIcons.SelectedValue;
            }
            set
            {
                if (!String.IsNullOrEmpty(value.ToString()))
                {
                    ddlIcons.SelectedValue = value.ToString();
                }
            }
        }

        #endregion
    }
}