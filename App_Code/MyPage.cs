using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for MyPage
/// </summary>
public class MyPage : Page
{
    public override void VerifyRenderingInServerForm(Control control)
    {
        GridView grid = control as GridView;
        if (grid != null && grid.ID == "GridView1")
            return;
        else
            base.VerifyRenderingInServerForm(control);

    }
}