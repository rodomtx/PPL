using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class About : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }


    public void redbuttonIMG_Click(object sender, EventArgs e)
    {
     string javasentence =   "alert('Se iniciara PPL')";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "k2", javasentence, true);
        Response.Redirect("DatosPPL.aspx");
    }
}