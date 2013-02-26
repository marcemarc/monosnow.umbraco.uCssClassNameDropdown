using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using monosnow.umbraco.uCssClassNameDropdown.Services;

namespace monosnow.umbraco.uCssClassNameDropdown.usercontrols
{
    public partial class BootstrapIconPicker : System.Web.UI.UserControl
    {
     protected override void OnPreRender(EventArgs e) 
   {
        bool linkIncluded = false;
        foreach (Control c in Page.Header.Controls)
        {
            if (c.ID == "BootstrapStylesheet")
            {
                linkIncluded = true;
            }
        }
        if (!linkIncluded)
        {
            HtmlGenericControl csslink = new HtmlGenericControl("link");
            csslink.ID = "BootstrapStylesheet";
            csslink.Attributes.Add("href", "/css/bootstrap.css");
            csslink.Attributes.Add("type", "text/css");
            csslink.Attributes.Add("rel", "stylesheet");
            Page.Header.Controls.Add(csslink);
        }
    }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                ClassNameRetrievalService _service = new ClassNameRetrievalService("/css/bootstrap.css", "bar", @".icon-(\S*?)\s{");
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
