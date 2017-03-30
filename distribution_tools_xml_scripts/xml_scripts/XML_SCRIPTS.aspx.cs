using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AutoFeedXML;

namespace RunFeed
{
	public partial class XML_SCRIPTS : System.Web.UI.Page
	{
    
    protected void Page_Load(object sender, EventArgs e)
		{
		}

    protected void btnRead_Click(object sender, EventArgs e)
    {
      ClassXMLSettings clsXML = new ClassXMLSettings();

      string strSetting = clsXML.GetXMLSettings(Request.Form["txtSection"], Request.Form["txtKey"], "Setting not found.");

      Response.Write(strSetting);

      clsXML.Dispose();
      clsXML = null;
    }

    protected void btnWrite_Click(object sender, EventArgs e)
    {
      ClassXMLSettings clsXML = new ClassXMLSettings();

      clsXML.InsertUpdateXMLSettings(Request.Form["txtSection"], Request.Form["txtKey"], Request.Form["txtSetting"]);

      clsXML.Dispose();
      clsXML = null;
    }
	}
}