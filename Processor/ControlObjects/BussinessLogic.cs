using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for BussinessLogic
/// </summary>
public class BussinessLogic
{
    public BussinessLogic()
    {
    }

    public void ShowText(string txt, bool IsError, MasterPage masterPage)
    {
        Label lblMsg = ((Label)masterPage.FindControl("LblMsg"));
        if (IsError)
        {
            lblMsg.Text = txt.ToUpper();
            lblMsg.ForeColor = System.Drawing.Color.Green;
        }
        else
        {
            lblMsg.Text = txt.ToUpper();
            lblMsg.ForeColor = System.Drawing.Color.Green;
        }
    }

}