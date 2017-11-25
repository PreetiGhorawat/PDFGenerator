using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.html;
using iTextSharp.text;
using iTextSharp.text.pdf;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    private bool CreatePDF(string HTMLContent, string OutputFilePath, out string ErrorIfAny)
    {
        bool IsSuccess = false;
        ErrorIfAny = string.Empty;
        Document document = new Document();

        try
        {    
            StringWriter SW = new StringWriter();
            SW.Write(HTMLContent);
            MyPage tmpPage = new MyPage();
            HtmlForm form = new HtmlForm();
            form.Controls.Add(new LiteralControl(""));
            tmpPage.Controls.Add(form);
            HtmlTextWriter htmlWriter = new HtmlTextWriter(SW);
            form.Controls[0].RenderControl(htmlWriter);
            string htmlContent = SW.ToString();
            PdfWriter.GetInstance(document, new FileStream(OutputFilePath, FileMode.Create));
            
            HTMLWorker worker = new HTMLWorker(document);
            document.Open();
            using (TextReader sReader = new StringReader(htmlContent))
            {
                FontFactory.Register(@"[Your font file location].ttf", "OpenSans-Regular");
                string fontpath = @"[Your font file location].ttf";
                //"simsun.ttf" file was downloaded from web and placed in the folder
                BaseFont bf = BaseFont.CreateFont(fontpath,  BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                //create new font based on BaseFont
                Font fontContent = new Font(bf, 11);
                Font fontHeader = new Font(bf, 12);
                // step 4.2: create a style sheet and set the encoding to Identity-H
                iTextSharp.text.html.simpleparser.StyleSheet ST = new iTextSharp.text.html.simpleparser.StyleSheet();
                ST.LoadTagStyle("body", "encoding", "Identity-H");
                worker.SetStyleSheet(ST);
                worker.StartDocument();
                worker.Parse(sReader);
                worker.EndDocument();
                worker.Close();
                document.Close();
            }
            IsSuccess = true;
        }
        catch (Exception eX)
        {
            IsSuccess = false;
            ErrorIfAny = eX.Message;
        }
        finally
        {
            try
            {
                if (document != null && document.IsOpen())
                {
                    document.Close();
                }
            }
            catch { }
        }
        return IsSuccess;
    }
    protected void btnGenerate_Click(object sender, EventArgs e)
    {

        string HTML_Configuration = txtHTML.Text.Trim();

        // Write the PDF
        string ErrorIfAny = string.Empty;
        string DownloadPath = Server.MapPath("") + @"\Downloads\";
        string PDFFileName = "PDF_" + DateTime.Now.ToString("ddMMMyy_HHmmss") + ".pdf";
        string PDF_NetworkGuide_ServerPath = DownloadPath + PDFFileName;
        if (!CreatePDF(HTML_Configuration, PDF_NetworkGuide_ServerPath, out ErrorIfAny))
        {
            lblMsg.Text = ErrorIfAny;
        }
        lblMsg.Text = "PDF is successfully generated @ " + PDF_NetworkGuide_ServerPath;
    }
}